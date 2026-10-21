using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;
using TextBox = System.Windows.Forms.TextBox;

namespace zgkk_sp_simulator
{
    public partial class Form1 : Form
    {
        static SerialPort serialPort;
        // Command byte -> reply; the "收到" boxes are hex, matching the protocol table
        Dictionary<int, int> cmdMap = new Dictionary<int, int>();

        // 数采板协议见 zgkk/docs/Files/基于STM32和AD7606的高精度实时数采器设计说明V3.1.docx
        // Replies come from the UI table, except relays: their state lives in relayBits.
        const int CMD_BUBBLE_COUNT = 0x09;    // 光电计数，回 1 字节，表1第9条
        const int CMD_BUBBLE_RESET = 0x10;    // 光电计数清零，回 1 字节，表1第10条
        const int CMD_RELAY_READ = 0x11;      // 读继电器，回 0x11 + 状态位，表1第11条、例1
        const int CMD_RELAY_WRITE = 0x12;     // 写继电器 0x12 xx，回 0x12 + 写后状态，表1第12条、例2
        const int FRAME_GAP_MS = 100;         // 0x12 后等数据字节的上限；9600 波特下一个字节约 1 ms

        readonly string[] _cliArgs;

        // bit n = relay n+1, 1 = on; written only by 0x12 xx, read by 0x11
        volatile int relayBits;
        Label[] relayLamps = new Label[8];

        public Form1() : this(new string[0]) { }

        public Form1(string[] args)
        {
            _cliArgs = args ?? new string[0];
            InitializeComponent();
            CreateRelayLamps();
            Load += Form1_Load;
        }

        // One display-only row of eight lamps beside the 0x11 row's hex box (textBox3).
        void CreateRelayLamps()
        {
            for (int i = 0; i < relayLamps.Length; i++)
            {
                Label lamp = new Label
                {
                    Text = (i + 1).ToString(),
                    TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                    Font = new System.Drawing.Font("宋体", 16F),
                    Location = new System.Drawing.Point(676 + (7 - i) * 38, textBox3.Top),  // bit 7 left, bit 0 right, like the hex value
                    Size = new System.Drawing.Size(36, 46),
                    BorderStyle = BorderStyle.FixedSingle,
                };
                relayLamps[i] = lamp;
                Controls.Add(lamp);
            }
            ShowRelayBits();
        }

        void ShowRelayBits()
        {
            int bits = relayBits;
            for (int i = 0; i < relayLamps.Length; i++)
                relayLamps[i].BackColor = (bits & (1 << i)) != 0 ? System.Drawing.Color.LimeGreen : System.Drawing.SystemColors.Control;
            textBox3.Text = bits.ToString("X2");
        }

        // 命令行给了什么就照着填、给了串口号就自动点连接。
        // 自动化测试不再需要驱动这个窗口的下拉框——它是 DropDownList，CB_SETCURSEL 会被
        // WinForms 托管层改回 -1，往它的文本里写也没用（open_Click 读的是 portname.Text）。
        private void Form1_Load(object sender, EventArgs e)
        {
            string port = null;
            foreach (string a in _cliArgs)
            {
                if (a.StartsWith("COM", StringComparison.OrdinalIgnoreCase)) port = a.ToUpperInvariant();
            }
            if (port == null) return;

            int idx = portname.Items.IndexOf(port);
            if (idx < 0) { portname.Items.Add(port); idx = portname.Items.Count - 1; }
            portname.SelectedIndex = idx;
            Console.WriteLine("命令行指定串口 " + port + "，自动连接");
            open_Click(this, EventArgs.Empty);
        }

        void LoadData()
        {
            cmdMap.Clear();
            List<TextBox> inText = new List<TextBox>() { i1, i2, i3, i4, i5, i6, i7, bubble };
            List<TextBox> rText = new List<TextBox>() { r1, r2, r3, r4, r5, r6, r7, bubbler };
            for (int i = 0; i < inText.Count; i++)
            {
                if (!string.IsNullOrEmpty(inText.ElementAt(i).Text) &&
                    !string.IsNullOrEmpty(rText.ElementAt(i).Text))
                {
                    int.TryParse(rText.ElementAt(i).Text, out int rr);
                    if (!int.TryParse(inText.ElementAt(i).Text, NumberStyles.HexNumber, null, out int c)) continue;
                    cmdMap.Add(c, rr);
                }
            }
        }

        private void open_Click(object sender, EventArgs e)
        {
            if (serialPort != null)
            {
                if (serialPort.IsOpen)
                {
                    serialPort.DataReceived -= SerialPort_DataReceived;

                    serialPort.Close();
                }
                else { serialPort.Open(); Console.WriteLine("串口已打开，等待输入..."); SetPortState(true); return; }
            }
            // 设置串口参数
            string portName = portname.Text; // 你可以根据需要更改串口号
            int baudRate = 9600;  // 波特率
            Parity parity = Parity.None;  // 校验
            int dataBits = 8;  // 数据位
            StopBits stopBits = StopBits.One;  // 停止位

            // 创建串口对象并设置参数
            serialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
            // 打开串口
            try
            {
                serialPort.Open();
                Console.WriteLine("串口已打开，等待输入...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"无法打开串口: {ex.Message}");
                // UnauthorizedAccessException = port held by another process
                SetPortState(ex is UnauthorizedAccessException);
                return;
            }
            SetPortState(true);

            // 订阅串口数据接收事件
            serialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
        }

        private void close_Click(object sender, EventArgs e)
        {
            // 关闭串口
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.DataReceived -= SerialPort_DataReceived;

                serialPort.Close();
            }
            Console.WriteLine("串口已关闭。");
            SetPortState(false);
        }

        // Probe the selected port (on selection change and every portPoll tick):
        // ours and open, or held by another process -> red.
        private void portname_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = portname.Text;
            if (serialPort != null && serialPort.IsOpen && serialPort.PortName == name) { SetPortState(true); return; }
            try
            {
                using (SerialPort probe = new SerialPort(name)) { probe.Open(); }
                SetPortState(false);
            }
            catch (UnauthorizedAccessException) { SetPortState(true); }
            catch (Exception) { SetPortState(false); }  // port does not exist: nothing occupies it
        }

        // Red = port open or occupied, green = closed/free.
        private void SetPortState(bool busy)
        {
            portState.BackColor = busy ? System.Drawing.Color.Red : System.Drawing.Color.LimeGreen;
        }

        // 处理串口数据接收事件。
        // The first byte is the command; only 0x12 carries a second byte (docx 表1第12条).
        // Loop until the buffer is drained; one event may carry several commands.
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            try
            {
                while (sp.IsOpen && sp.BytesToRead > 0)
                {
                    int b0 = sp.ReadByte();
                    if (b0 < 0) return;
                    LoadData();
                    bool twoByte = b0 == CMD_RELAY_WRITE;
                    int b1 = 0;
                    if (twoByte)
                    {
                        // A lone 0x12 must not swallow the next frame's first byte
                        sp.ReadTimeout = FRAME_GAP_MS;
                        try { b1 = sp.ReadByte(); }
                        catch (TimeoutException)
                        {
                            Console.WriteLine("rx 0x12 without its data byte -> no reply");
                            continue;
                        }
                        if (b1 < 0) return;
                    }
                    byte[] buffer = HandleInput(b0, b1);
                    sp.Write(buffer, 0, buffer.Length);
                    string rx = twoByte ? $"0x{b0:X2} 0x{b1:X2}" : $"0x{b0:X2}";
                    Console.WriteLine($"rx {rx} -> tx {BitConverter.ToString(buffer)}");
                }
            }
            catch (TimeoutException) { }
            catch (InvalidOperationException) { }  // port closed mid-read
        }

        // 根据输入数据返回不同的响应
        private byte[] HandleInput(int input, int arg)
        {
            if (input == CMD_RELAY_READ)
            {
                return new byte[] { CMD_RELAY_READ, (byte)relayBits };
            }
            if (input == CMD_RELAY_WRITE)
            {
                relayBits = arg;
                this.BeginInvoke(new Action(ShowRelayBits));
                return new byte[] { CMD_RELAY_WRITE, (byte)arg };
            }
            byte[] bytes = new byte[2] { 0, 0 };
            // 根据 inputCommand 查询字典
            if (cmdMap.ContainsKey(input))
            {
                int response = cmdMap[input];
                bytes[0] = (byte)(response >> 8);
                bytes[1] = (byte)response;
                Console.WriteLine("Command found! Response: " + response);
            }
            else if (input == CMD_BUBBLE_RESET)
            {
                this.BeginInvoke(new Action(() =>
                {
                    bubbler.Text = "0";
                }));
            }
            else
            {
                Console.WriteLine("Command not found.");
            }
            // The photo-counter commands answer a single byte
            if (input == CMD_BUBBLE_COUNT || input == CMD_BUBBLE_RESET) return new byte[] { bytes[1] };
            return bytes;
        }

        void changeNum(TextBox tb, int offset, bool isAdd)
        {
            int rr = int.Parse(tb.Text) + (isAdd ? offset : (-offset));
            tb.Text = (rr < 0 ? 0 : rr).ToString();
        }
        private void r100p1_Click(object sender, EventArgs e)
        {
            changeNum(r1, int.Parse(unit_b.Text), true);
        }

        private void r100p2_Click(object sender, EventArgs e)
        {
            changeNum(r2, int.Parse(unit_b.Text), true);
        }

        private void r100p3_Click(object sender, EventArgs e)
        {
            changeNum(r3, int.Parse(unit_b.Text), true);
        }

        private void r100p4_Click(object sender, EventArgs e)
        {
            changeNum(r4, int.Parse(unit_b.Text), true);
        }

        private void r100p5_Click(object sender, EventArgs e)
        {
            changeNum(r5, int.Parse(unit_b.Text), true);
        }

        private void r100p6_Click(object sender, EventArgs e)
        {
            changeNum(r6, int.Parse(unit_b.Text), true);
        }

        private void r100p7_Click(object sender, EventArgs e)
        {
            changeNum(r7, int.Parse(unit_b.Text), true);
        }

        private void r10p1_Click(object sender, EventArgs e)
        {
            changeNum(r1, int.Parse(unit_s.Text), true);
        }

        private void r10p2_Click(object sender, EventArgs e)
        {
            changeNum(r2, int.Parse(unit_s.Text), true);
        }

        private void r10p3_Click(object sender, EventArgs e)
        {
            changeNum(r3, int.Parse(unit_s.Text), true);
        }

        private void r10p4_Click(object sender, EventArgs e)
        {
            changeNum(r4, int.Parse(unit_s.Text), true);
        }

        private void r10p5_Click(object sender, EventArgs e)
        {
            changeNum(r5, int.Parse(unit_s.Text), true);
        }

        private void r10p6_Click(object sender, EventArgs e)
        {
            changeNum(r6, int.Parse(unit_s.Text), true);
        }

        private void r10p7_Click(object sender, EventArgs e)
        {
            changeNum(r7, int.Parse(unit_s.Text), true);
        }

        private void r100n1_Click(object sender, EventArgs e)
        {
            changeNum(r1, int.Parse(unit_b.Text), false);
        }

        private void r100n2_Click(object sender, EventArgs e)
        {
            changeNum(r2, int.Parse(unit_b.Text), false);
        }

        private void r100n3_Click(object sender, EventArgs e)
        {
            changeNum(r3, int.Parse(unit_b.Text), false);
        }

        private void r100n4_Click(object sender, EventArgs e)
        {
            changeNum(r4, int.Parse(unit_b.Text), false);
        }

        private void r100n5_Click(object sender, EventArgs e)
        {
            changeNum(r5, int.Parse(unit_b.Text), false);
        }

        private void r100n6_Click(object sender, EventArgs e)
        {
            changeNum(r6, int.Parse(unit_b.Text), false);
        }

        private void r100n7_Click(object sender, EventArgs e)
        {
            changeNum(r7, int.Parse(unit_b.Text), false);
        }

        private void r10n1_Click(object sender, EventArgs e)
        {
            changeNum(r1, int.Parse(unit_s.Text), false);
        }

        private void r10n2_Click(object sender, EventArgs e)
        {
            changeNum(r2, int.Parse(unit_s.Text), false);
        }

        private void r10n3_Click(object sender, EventArgs e)
        {
            changeNum(r3, int.Parse(unit_s.Text), false);
        }

        private void r10n4_Click(object sender, EventArgs e)
        {
            changeNum(r4, int.Parse(unit_s.Text), false);
        }

        private void r10n5_Click(object sender, EventArgs e)
        {
            changeNum(r5, int.Parse(unit_s.Text), false);
        }

        private void r10n6_Click(object sender, EventArgs e)
        {
            changeNum(r6, int.Parse(unit_s.Text), false);
        }

        private void r10n7_Click(object sender, EventArgs e)
        {
            changeNum(r7, int.Parse(unit_s.Text), false);
        }

        private void c1_Click(object sender, EventArgs e)
        {
            r1.Text = "0";
        }

        private void c2_Click(object sender, EventArgs e)
        {
            r2.Text = "0";
        }

        private void c3_Click(object sender, EventArgs e)
        {
            r3.Text = "0";
        }

        private void c4_Click(object sender, EventArgs e)
        {
            r4.Text = "0";
        }

        private void c5_Click(object sender, EventArgs e)
        {
            r5.Text = "0";
        }

        private void c6_Click(object sender, EventArgs e)
        {
            r6.Text = "0";
        }

        private void c7_Click(object sender, EventArgs e)
        {
            r7.Text = "0";
        }

        private void r100p8_Click(object sender, EventArgs e)
        {
            changeNum(bubbler, int.Parse(unit_b.Text), true);
        }

        private void r100n8_Click(object sender, EventArgs e)
        {
            changeNum(bubbler, int.Parse(unit_b.Text), false);
        }

        private void r10p8_Click(object sender, EventArgs e)
        {
            changeNum(bubbler, int.Parse(unit_s.Text), true);
        }

        private void r10n8_Click(object sender, EventArgs e)
        {
            changeNum(bubbler, int.Parse(unit_s.Text), false);
        }
    }
}
