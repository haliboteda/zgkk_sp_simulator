using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
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
    }

    // Scenario controls: bench, manual/auto, start/stop, valve parameters, channel table and log.
    // While running, enabled channels answer their opcodes from the valve model; otherwise the old table answers.
    public class ScenarioPanel : UserControl
    {
        static readonly string SettingsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.json");
        // Opcodes the protocol already uses for bubble count / reset and relays
        static readonly int[] Reserved = { 0x09, 0x10, 0x11, 0x12 };

        readonly object sync = new object();
        readonly ValveModel model = new ValveModel();
        List<Channel> channels;
        readonly Func<int> relayBits;
        readonly Stopwatch clock = new Stopwatch();
        readonly Timer timer = new Timer { Interval = 50 };

        volatile bool running;
        Drive manualDrive = Drive.Hold;

        ComboBox bench, breathDir;
        RadioButton manual, auto;
        Button start, stop, supply, hold, vent;
        Label state;
        DataGridView grid;
        PropertyGrid props;
        RichTextBox log;

        public event Action Ticked;

        public bool Running => running;

        public ScenarioPanel(Func<int> relayBits)
        {
            this.relayBits = relayBits;
            BackColor = Color.Black;
            ForeColor = Color.White;
            Font = new Font("微软雅黑", 10.5F);
            LoadSettings();
            BuildUi();
            model.Event += m => BeginInvokeSafe(() => Log(m, false));
            timer.Tick += (s, e) => StepModel();
            timer.Start();
            clock.Start();
        }

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
            autoAtLoad = s.Auto;
        }
        bool autoAtLoad;

        public void Save()
        {
            try
            {
                SimSettings s = new SimSettings { Bench = model.Bench, Auto = auto.Checked, BreathNegative = model.BreathNegative, P = model.P, Channels = channels };
                using (FileStream f = File.Create(SettingsPath))
                    new DataContractJsonSerializer(typeof(SimSettings)).WriteObject(f, s);
            }
            catch (Exception ex) { Console.WriteLine("settings.json not saved: " + ex.Message); }
        }

        // ---------- UI ----------

        void BuildUi()
        {
            TableLayoutPanel root = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 4, Padding = new Padding(4) };
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 150));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            Controls.Add(root);

            FlowLayoutPanel rowA = Flow();
            rowA.Controls.Add(Lbl("台子"));
            bench = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 120 };
            bench.Items.AddRange(new object[] { "呼吸阀", "安全阀在线", "安全阀离线" });
            bench.SelectedIndex = (int)model.Bench;
            bench.SelectedIndexChanged += (s, e) => { model.Bench = (Bench)bench.SelectedIndex; model.Reset(); RefreshBench(); };
            rowA.Controls.Add(bench);
            breathDir = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 70 };
            breathDir.Items.AddRange(new object[] { "正压", "负压" });
            breathDir.SelectedIndex = model.BreathNegative ? 1 : 0;
            breathDir.SelectedIndexChanged += (s, e) => { lock (sync) { model.BreathNegative = breathDir.SelectedIndex == 1; model.Reset(); } };
            rowA.Controls.Add(breathDir);
            manual = new RadioButton { Text = "人工", AutoSize = true, Checked = !autoAtLoad, Margin = new Padding(16, 6, 0, 0) };
            auto = new RadioButton { Text = "自动（跟上位机继电器）", AutoSize = true, Checked = autoAtLoad, Margin = new Padding(6, 6, 0, 0) };
            manual.CheckedChanged += (s, e) => RefreshButtons();
            rowA.Controls.Add(manual);
            rowA.Controls.Add(auto);
            root.Controls.Add(rowA);

            FlowLayoutPanel rowB = Flow();
            start = Btn("开始", (s, e) => Start());
            stop = Btn("结束", (s, e) => Stop());
            supply = Btn("给压", (s, e) => SetManual(Drive.Supply));
            hold = Btn("保压", (s, e) => SetManual(Drive.Hold));
            vent = Btn("泄压", (s, e) => SetManual(Drive.Vent));
            supply.Margin = new Padding(20, 3, 3, 3);
            state = new Label { AutoSize = true, Font = new Font("微软雅黑", 14F, FontStyle.Bold), Margin = new Padding(20, 4, 0, 0) };
            rowB.Controls.AddRange(new Control[] { start, stop, supply, hold, vent, state });
            root.Controls.Add(rowB);

            grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.CellSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ForeColor = Color.Black,
            };
            grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "通道", ReadOnly = true, FillWeight = 130 });
            grid.Columns.Add(new DataGridViewCheckBoxColumn { HeaderText = "启用", FillWeight = 45 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "操作码", FillWeight = 55 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "量程下限", FillWeight = 70 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "量程上限", FillWeight = 70 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "串口下限", FillWeight = 65 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "串口上限", FillWeight = 65 });
            grid.Columns.Add(new DataGridViewCheckBoxColumn { HeaderText = "翻转", FillWeight = 45 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "当前值", ReadOnly = true, FillWeight = 70 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "回包", ReadOnly = true, FillWeight = 60 });
            // Commit checkbox clicks immediately instead of on leaving the cell
            grid.CurrentCellDirtyStateChanged += (s, e) => { if (grid.IsCurrentCellDirty && grid.CurrentCell is DataGridViewCheckBoxCell) grid.CommitEdit(DataGridViewDataErrorContexts.Commit); };
            grid.CellValueChanged += Grid_CellValueChanged;
            root.Controls.Add(grid);

            TableLayoutPanel split = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, Margin = new Padding(0) };
            split.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            split.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            props = new PropertyGrid { Dock = DockStyle.Fill, SelectedObject = model.P, ToolbarVisible = false, HelpVisible = false, PropertySort = PropertySort.Categorized };
            split.Controls.Add(props, 0, 0);
            log = new RichTextBox { Dock = DockStyle.Fill, ReadOnly = true, BackColor = Color.FromArgb(20, 20, 20), ForeColor = Color.Gainsboro, Font = new Font("Consolas", 9.5F) };
            split.Controls.Add(log, 1, 0);
            root.Controls.Add(split);

            RefreshBench();
        }

        static FlowLayoutPanel Flow() { return new FlowLayoutPanel { AutoSize = true, Dock = DockStyle.Fill, WrapContents = false }; }
        static Label Lbl(string t) { return new Label { Text = t, AutoSize = true, Margin = new Padding(0, 6, 4, 0) }; }
        static Button Btn(string t, EventHandler h)
        {
            Button b = new Button { Text = t, Width = 64, Height = 30, ForeColor = Color.Black, BackColor = SystemColors.Control };
            b.Click += h;
            return b;
        }

        void RefreshBench()
        {
            breathDir.Visible = model.Bench == Bench.Breath;
            FillGrid();
            RefreshButtons();
        }

        void RefreshButtons()
        {
            bench.Enabled = !running;
            breathDir.Enabled = !running;
            start.Enabled = !running;
            stop.Enabled = running;
            foreach (Button b in new[] { supply, hold, vent })
            {
                b.Enabled = running && manual.Checked;
                b.BackColor = b.Enabled && b == DriveButton(manualDrive) ? Color.Gold : SystemColors.Control;
            }
        }

        Button DriveButton(Drive d) { return d == Drive.Supply ? supply : d == Drive.Vent ? vent : hold; }

        bool filling;
        void FillGrid()
        {
            filling = true;
            grid.Rows.Clear();
            foreach (Channel c in channels.Where(c => c.ShownOn(model.Bench)))
            {
                int i = grid.Rows.Add(c.Name, c.Optional ? c.Enabled : true, c.Opcode.ToString("X2"),
                    c.From.ToString(CultureInfo.InvariantCulture), c.To.ToString(CultureInfo.InvariantCulture),
                    c.RawMin.ToString(), c.RawMax.ToString(), c.Invert, "", "");
                grid.Rows[i].Tag = c;
                grid.Rows[i].Cells[1].ReadOnly = !c.Optional;
            }
            filling = false;
            UpdateGridValues();
        }

        void Grid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (filling || e.RowIndex < 0) return;
            Channel c = (Channel)grid.Rows[e.RowIndex].Tag;
            object v = grid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            string t = Convert.ToString(v, CultureInfo.InvariantCulture)?.Trim() ?? "";
            bool ok = true;
            switch (e.ColumnIndex)
            {
                case 1: c.Enabled = v is bool b1 && b1; break;
                case 2: ok = int.TryParse(t, NumberStyles.HexNumber, null, out int op) && op > 0 && op <= 0xFF; if (ok) c.Opcode = op; break;
                case 3: ok = double.TryParse(t, NumberStyles.Float, CultureInfo.InvariantCulture, out double f) && f != c.To; if (ok) c.From = f; break;
                case 4: ok = double.TryParse(t, NumberStyles.Float, CultureInfo.InvariantCulture, out double to) && to != c.From; if (ok) c.To = to; break;
                case 5: ok = int.TryParse(t, out int rmin) && rmin >= 0 && rmin < c.RawMax; if (ok) c.RawMin = rmin; break;
                case 6: ok = int.TryParse(t, out int rmax) && rmax > c.RawMin && rmax <= 0xFFFF; if (ok) c.RawMax = rmax; break;
                case 7: c.Invert = v is bool b7 && b7; break;
            }
            if (!ok)
            {
                Log($"{c.Name}：「{t}」不是有效值，已恢复", true);
                BeginInvokeSafe(FillGrid);
                return;
            }
            CheckOpcodes();
        }

        // Two active channels on one opcode, or one on a protocol opcode, would answer the host wrongly
        void CheckOpcodes()
        {
            List<Channel> act = channels.Where(c => c.ActiveOn(model.Bench)).ToList();
            foreach (var g in act.GroupBy(c => c.Opcode).Where(g => g.Count() > 1))
                Log($"操作码 {g.Key:X2} 被 {string.Join("、", g.Select(c => c.Name))} 同时占用，只有第一个会回包", true);
            foreach (Channel c in act.Where(c => Reserved.Contains(c.Opcode)))
                Log($"{c.Name} 的操作码 {c.Opcode:X2} 是气泡 / 继电器命令，不会按模型回包", true);
        }

        void UpdateGridValues()
        {
            foreach (DataGridViewRow r in grid.Rows)
            {
                Channel c = (Channel)r.Tag;
                double v;
                int raw;
                lock (sync) { v = c.Value(model); raw = c.ToRaw(v); }
                r.Cells[8].Value = v.ToString(c.Q == Quantity.PressureMPa || c.Q == Quantity.Hydraulic ? "0.000" : "0.0");
                r.Cells[9].Value = $"{raw >> 8:X2} {raw & 0xFF:X2}";
            }
            string s = !running ? "未开始（回旧表）" : model.Open ? "开启" : "关闭";
            if (model.Bench == Bench.SafetyOffline) s += $"  液压 {model.HydraulicMPa:0.00} MPa";
            state.Text = s;
            state.ForeColor = running && model.Open ? Color.OrangeRed : Color.White;
        }

        // ---------- run ----------

        void Start()
        {
            lock (sync) model.Reset();
            manualDrive = Drive.Hold;
            running = true;
            Log($"开始：{bench.Text}{(model.Bench == Bench.Breath ? " " + breathDir.Text : "")}，{(auto.Checked ? "自动" : "人工")}", false);
            CheckOpcodes();
            RefreshButtons();
            Save();
        }

        void Stop()
        {
            running = false;
            Log("结束：回包改回旧表", false);
            RefreshButtons();
        }

        void SetManual(Drive d)
        {
            manualDrive = d;
            Log("人工 → " + DriveButton(d).Text, false);
            RefreshButtons();
        }

        void StepModel()
        {
            double dt = clock.Elapsed.TotalSeconds;
            clock.Restart();
            if (running)
            {
                int bits = relayBits();
                lock (sync) model.Step(dt, auto.Checked ? model.DriveFromRelays(bits) : manualDrive, bits);
            }
            UpdateGridValues();
            Ticked?.Invoke();
        }

        // ---------- called by Form1 ----------

        // Serial thread: the model's reply for this opcode, if a running, active channel owns it
        public bool TryReply(int opcode, out int raw)
        {
            raw = 0;
            if (!running || Reserved.Contains(opcode)) return false;
            Channel c = ChannelFor(opcode);
            if (c == null) return false;
            lock (sync) raw = c.ToRaw(c.Value(model));
            return true;
        }

        public Channel ChannelFor(int opcode)
        {
            if (!running) return null;
            return channels.FirstOrDefault(c => c.ActiveOn(model.Bench) && c.Opcode == opcode);
        }

        public int RawOf(Channel c) { lock (sync) return c.ToRaw(c.Value(model)); }

        // UI thread, after the host's 0x12 write
        public void OnRelayWrite(int oldBits, int newBits)
        {
            if (!running) return;
            string w;
            lock (sync) { w = model.CheckRelayWrite(oldBits, newBits); model.ApplyRelayWrite(newBits); }
            ValveParams p = model.P;
            var jobs = new List<string>();
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
            log.SelectionStart = log.TextLength;
            log.SelectionColor = warn ? Color.Red : log.ForeColor;
            log.AppendText(DateTime.Now.ToString("HH:mm:ss.f ") + msg + "\n");
            log.ScrollToCaret();
        }

        void BeginInvokeSafe(Action a)
        {
            if (IsHandleCreated) BeginInvoke(a); else a();
        }
    }
}
