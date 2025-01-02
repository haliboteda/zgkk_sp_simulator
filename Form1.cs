using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace zgkk_sp_simulator
{
    public partial class Form1 : Form
    {
        static SerialPort serialPort;
        Dictionary<string, int> cmdMap = new Dictionary<string, int>();
        public Form1()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            cmdMap.Clear();
            List<TextBox> inText = new List<TextBox>() { i1, i2, i3,i4,i5,i6,i7 };
            List<TextBox> rText = new List<TextBox>() { r1, r2, r3,r4,r5,r6,r7 };
            for (int i = 0; i < inText.Count; i++)
            {
                if (!string.IsNullOrEmpty(inText.ElementAt(i).Text) &&
                    !string.IsNullOrEmpty(rText.ElementAt(i).Text))
                {
                    int.TryParse(rText.ElementAt(i).Text, out int rr);
                    cmdMap.Add(inText.ElementAt(i).Text, rr);
                }
            }
        }

        private void open_Click(object sender, EventArgs e)
        {
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
                return;
            }

            // 订阅串口数据接收事件
            serialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
        }

        private void close_Click(object sender, EventArgs e)
        {
            // 关闭串口
            if (serialPort.IsOpen)
            {
                serialPort.DataReceived -= SerialPort_DataReceived;

                serialPort.Close();
            }
            Console.WriteLine("串口已关闭。");
        }

        // 处理串口数据接收事件
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;

            byte[] buff = new byte[2];
            sp.Read(buff, 0, 2);
            if(BitConverter.IsLittleEndian)
                Array.Reverse(buff);

            // 根据输入的不同，返回不同的响应
            byte[] buffer = HandleInput(BitConverter.ToInt16(buff, 0).ToString());

            // 返回响应给串口
            serialPort.Write(buffer, 0, buffer.Length);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(buffer);
            Console.WriteLine($"返回数据: {BitConverter.ToInt16(buffer, 0)}");
        }

        // 根据输入数据返回不同的响应
        private byte[] HandleInput(string input)
        {
            LoadData();
            byte[] bytes = new byte[2] { 0, 0};
            // 根据 inputCommand 查询字典
            if (cmdMap.ContainsKey(input))
            {
                int response = cmdMap[input];
                bytes[0] = (byte)(response >> 8);
                bytes[1] = (byte)response;
                Console.WriteLine("Command found! Response: " + response);
            }
            else
            {
                Console.WriteLine("Command not found.");
            }
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
    }
}
