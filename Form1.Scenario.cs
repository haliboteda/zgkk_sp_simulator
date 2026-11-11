using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows.Forms;

namespace zgkk_sp_simulator
{
    [DataContract]
    public class SimSettings
    {
        [DataMember] public Bench Bench = Bench.SafetyOnline;
        [DataMember] public bool Auto;
        [DataMember] public bool BreathNegative;
        [DataMember] public ValveParams P = new ValveParams();
        [DataMember] public List<Channel> Channels = Channel.Defaults();
        [DataMember] public int[] RowSources;   // 来源 index per row
    }

    // Scenario simulation: each row's 来源 picks a model quantity; once started, those rows answer from the valve model
    public partial class Form1
    {
        static readonly string SettingsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.json");
        // Order of the 来源 combo items after 固定
        static readonly Quantity[] Sources = { Quantity.PressureMPa, Quantity.Hydraulic, Quantity.Lift, Quantity.Sound, Quantity.Temp };

        readonly object sync = new object();
        readonly ValveModel model = new ValveModel();
        List<Channel> channels;
        readonly Stopwatch clock = new Stopwatch();
        volatile bool running;
        Drive manualDrive = Drive.Hold;

        TextBox[] opBoxes, valBoxes;
        ComboBox[] srcBoxes;
        readonly int[] rowOffset = new int[7];   // raw counts the +/- buttons add to a model row
        readonly int[] rowOp = new int[7];        // row opcodes, mirrored for the serial thread
        // Fixed-table text of rows the model has taken over, restored on 结束
        readonly Dictionary<TextBox, string> savedVal = new Dictionary<TextBox, string>();

        void InitScenario()
        {
            opBoxes = new[] { i1, i2, i3, i4, i5, i6, i7 };
            valBoxes = new[] { r1, r2, r3, r4, r5, r6, r7 };
            srcBoxes = new[] { src1, src2, src3, src4, src5, src6, src7 };
            model.Event += m => Log(m, false);
            LoadSettings();
            RefreshBench();
            clock.Start();
        }

        // ---------- settings ----------

        void LoadSettings()
        {
            SimSettings s = null;
            try
            {
                if (File.Exists(SettingsPath))
                    using (FileStream f = File.OpenRead(SettingsPath))
                        s = (SimSettings)new DataContractJsonSerializer(typeof(SimSettings)).ReadObject(f);
            }
            catch (Exception ex) { Console.WriteLine("settings.json unreadable, using defaults: " + ex.Message); }
            s = s ?? new SimSettings();
            model.P = s.P ?? new ValveParams();
            model.Bench = s.Bench;
            model.BreathNegative = s.BreathNegative;
            channels = s.Channels ?? new List<Channel>();
            // A file from an older build may lack channels added later
            foreach (Channel d in Channel.Defaults())
                if (!channels.Any(c => c.Q == d.Q)) channels.Add(d);
            channels = channels.OrderBy(c => c.Q).ToList();

            cbBench.SelectedIndex = (int)model.Bench;
            cbBreathDir.SelectedIndex = model.BreathNegative ? 1 : 0;
            (s.Auto ? rbAuto : rbManual).Checked = true;
            for (int k = 0; k < srcBoxes.Length; k++)
                srcBoxes[k].SelectedIndex = s.RowSources != null && k < s.RowSources.Length ? s.RowSources[k] : 0;
        }

        void SaveSettings()
        {
            try
            {
                SimSettings s = new SimSettings
                {
                    Bench = model.Bench,
                    Auto = rbAuto.Checked,
                    BreathNegative = model.BreathNegative,
                    P = model.P,
                    Channels = channels,
                    RowSources = srcBoxes.Select(b => b.SelectedIndex).ToArray(),
                };
                using (FileStream f = File.Create(SettingsPath))
                    new DataContractJsonSerializer(typeof(SimSettings)).WriteObject(f, s);
            }
            catch (Exception ex) { Console.WriteLine("settings.json not saved: " + ex.Message); }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }

        // ---------- rows ----------

        Channel ChannelOf(int source)
        {
            if (source <= 0) return null;
            Quantity q = Sources[source - 1];
            if (q == Quantity.PressureMPa && model.Bench == Bench.Breath) q = Quantity.PressurePa;
            return channels.First(c => c.Q == q);
        }

        // The model channel answering row k right now, or null when the row replies its fixed value
        Channel ModelChannelOf(int k)
        {
            return running ? ChannelOf(srcBoxes[k].SelectedIndex) : null;
        }

        int RowRaw(int k, Channel c)
        {
            int raw;
            lock (sync) raw = c.ToRaw(c.Value(model));
            return Math.Max(0, Math.Min(0xFFFF, raw + rowOffset[k]));
        }

        // Serial thread: the first model row with this opcode answers
        bool TryModelReply(int opcode, out int raw)
        {
            raw = 0;
            if (!running) return false;
            for (int k = 0; k < rowOp.Length; k++)
            {
                if (rowOp[k] != opcode) continue;
                Channel c = ModelChannelOf(k);
                if (c == null) continue;
                raw = RowRaw(k, c);
                return true;
            }
            return false;
        }

        // UI thread, every tick: mirror opcodes and show model rows' replies in their 返回 boxes
        void ShowModelRows()
        {
            for (int k = 0; k < opBoxes.Length; k++)
            {
                rowOp[k] = int.TryParse(opBoxes[k].Text, NumberStyles.HexNumber, null, out int op) ? op : -1;
                TextBox val = valBoxes[k];
                Channel c = ModelChannelOf(k);
                if (c != null)
                {
                    if (!savedVal.ContainsKey(val)) { savedVal[val] = val.Text; val.ReadOnly = true; }
                    val.Text = RowRaw(k, c).ToString();
                }
                else if (savedVal.TryGetValue(val, out string old))
                {
                    val.Text = old;
                    val.ReadOnly = false;
                    savedVal.Remove(val);
                }
            }
        }

        // ---------- controls ----------

        private void cbBench_SelectedIndexChanged(object sender, EventArgs e)
        {
            lock (sync) { model.Bench = (Bench)cbBench.SelectedIndex; model.Reset(); }
            RefreshBench();
        }

        private void cbBreathDir_SelectedIndexChanged(object sender, EventArgs e)
        {
            lock (sync) { model.BreathNegative = cbBreathDir.SelectedIndex == 1; model.Reset(); }
        }

        private void rbManual_CheckedChanged(object sender, EventArgs e)
        {
            manualDrive = Drive.Hold;
            ShowDrive();
        }

        void RefreshBench()
        {
            cbBreathDir.Visible = model.Bench == Bench.Breath;
            RefreshButtons();
        }

        void RefreshButtons()
        {
            cbBench.Enabled = cbBreathDir.Enabled = !running;
            btnStart.Enabled = !running;
            btnStop.Enabled = running;
            ShowDrive();
        }

        // Manual: the chosen state; auto: whatever the host's relays say. Neither lit = holding
        void ShowDrive()
        {
            Drive d = !running ? Drive.Hold : rbAuto.Checked ? model.DriveFromRelays(relayBits) : manualDrive;
            btnSupply.BackColor = d == Drive.Supply ? Color.LimeGreen : SystemColors.Control;
            btnVent.BackColor = d == Drive.Vent ? Color.Orange : SystemColors.Control;
            btnSupply.Cursor = btnVent.Cursor = running && rbManual.Checked ? Cursors.Hand : Cursors.No;
        }

        void Toggle(Drive d)
        {
            if (!running || !rbManual.Checked) return;
            manualDrive = manualDrive == d ? Drive.Hold : d;
            Log("人工 → " + (manualDrive == Drive.Supply ? "给压" : manualDrive == Drive.Vent ? "泄压" : "保压"), false);
            ShowDrive();
        }

        private void btnSupply_Click(object sender, EventArgs e) { Toggle(Drive.Supply); }

        private void btnVent_Click(object sender, EventArgs e) { Toggle(Drive.Vent); }

        private void btnStart_Click(object sender, EventArgs e)
        {
            lock (sync) model.Reset();
            Array.Clear(rowOffset, 0, rowOffset.Length);
            manualDrive = Drive.Hold;
            running = true;
            Log($"开始：{cbBench.Text}{(model.Bench == Bench.Breath ? " " + cbBreathDir.Text : "")}，{(rbAuto.Checked ? "自动" : "人工")}", false);
            if (!srcBoxes.Any(b => b.SelectedIndex > 0)) Log("没有一行的来源选了模型，回包仍是固定值", true);
            RefreshButtons();
            SaveSettings();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            running = false;
            Log("结束：回包改回固定值", false);
            RefreshButtons();
        }

        private void btnParams_Click(object sender, EventArgs e)
        {
            using (ParamsForm f = new ParamsForm(new BenchParamsView(model.P, model.Bench), cbBench.Text))
                f.ShowDialog(this);
        }

        private void btnChannels_Click(object sender, EventArgs e)
        {
            Quantity skip = model.Bench == Bench.Breath ? Quantity.PressureMPa : Quantity.PressurePa;
            using (ChannelsForm f = new ChannelsForm(channels.Where(c => c.Q != skip).ToList()))
                f.ShowDialog(this);
        }

        private void simTimer_Tick(object sender, EventArgs e)
        {
            double dt = clock.Elapsed.TotalSeconds;
            clock.Restart();
            if (running)
            {
                int bits = relayBits;
                lock (sync) model.Step(dt, rbAuto.Checked ? model.DriveFromRelays(bits) : manualDrive, bits);
            }
            ShowModelRows();
            ShowValues();
            ShowDrive();
        }

        // Physical values of the pressure, the offline hydraulic pressure and every quantity a row uses
        void ShowValues()
        {
            List<Channel> shown = new List<Channel> { ChannelOf(1) };
            if (model.Bench == Bench.SafetyOffline) shown.Add(ChannelOf(2));
            foreach (ComboBox b in srcBoxes)
            {
                Channel c = ChannelOf(b.SelectedIndex);
                if (c != null && !shown.Contains(c)) shown.Add(c);
            }
            StringBuilder sb = new StringBuilder();
            lock (sync)
                foreach (Channel c in shown)
                {
                    int sp = c.Name.LastIndexOf(' ');
                    sb.AppendLine($"{c.Name.Substring(0, sp)}  {c.Value(model).ToString(c.Format)} {c.Name.Substring(sp + 1)}");
                }
            lblValues.Text = sb.ToString();
            lblState.Text = !running ? "未开始（回固定值）" : model.Open ? "阀门 开启" : "阀门 关闭";
            lblState.ForeColor = running && model.Open ? Color.OrangeRed : Color.White;
        }

        // UI thread, after the host's 0x12 write
        void OnRelayWrite(int oldBits, int newBits)
        {
            if (!running) return;
            string w;
            lock (sync) { w = model.CheckRelayWrite(oldBits, newBits); model.ApplyRelayWrite(newBits); }
            ValveParams p = model.P;
            List<string> jobs = new List<string>();
            if (ValveParams.On(newBits, p.SupplyDy)) jobs.Add("给压");
            if (ValveParams.On(newBits, p.VentDy)) jobs.Add("泄压");
            if (model.Bench == Bench.SafetyOffline)
            {
                if (ValveParams.On(newBits, p.ClampDy)) jobs.Add("夹紧");
                if (ValveParams.On(newBits, p.ReturnDy)) jobs.Add("回油");
            }
            Log($"上位机写继电器 {newBits:X2}：{(jobs.Count == 0 ? "全断" : string.Join("、", jobs))}", false);
            if (w != null) Log("违反安全规则：" + w, true);
        }

        void Log(string msg, bool warn)
        {
            rtbLog.SelectionStart = rtbLog.TextLength;
            rtbLog.SelectionColor = warn ? Color.Red : rtbLog.ForeColor;
            rtbLog.AppendText(DateTime.Now.ToString("HH:mm:ss.f ") + msg + "\n");
            rtbLog.ScrollToCaret();
        }
    }
}
