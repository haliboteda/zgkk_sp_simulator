
namespace zgkk_sp_simulator
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.writetimeout = new System.Windows.Forms.ComboBox();
            this.readtimeout = new System.Windows.Forms.ComboBox();
            this.parity = new System.Windows.Forms.ComboBox();
            this.baudrate = new System.Windows.Forms.ComboBox();
            this.portname = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.i1 = new System.Windows.Forms.TextBox();
            this.r1 = new System.Windows.Forms.TextBox();
            this.i2 = new System.Windows.Forms.TextBox();
            this.r2 = new System.Windows.Forms.TextBox();
            this.i3 = new System.Windows.Forms.TextBox();
            this.r3 = new System.Windows.Forms.TextBox();
            this.open = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.Button();
            this.i6 = new System.Windows.Forms.TextBox();
            this.i5 = new System.Windows.Forms.TextBox();
            this.i4 = new System.Windows.Forms.TextBox();
            this.r6 = new System.Windows.Forms.TextBox();
            this.r5 = new System.Windows.Forms.TextBox();
            this.r4 = new System.Windows.Forms.TextBox();
            this.i7 = new System.Windows.Forms.TextBox();
            this.r7 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.r100p1 = new System.Windows.Forms.Button();
            this.r100n1 = new System.Windows.Forms.Button();
            this.r10n1 = new System.Windows.Forms.Button();
            this.r10p1 = new System.Windows.Forms.Button();
            this.r10n2 = new System.Windows.Forms.Button();
            this.r10p2 = new System.Windows.Forms.Button();
            this.r100n2 = new System.Windows.Forms.Button();
            this.r100p2 = new System.Windows.Forms.Button();
            this.r10n3 = new System.Windows.Forms.Button();
            this.r10p3 = new System.Windows.Forms.Button();
            this.r100n3 = new System.Windows.Forms.Button();
            this.r100p3 = new System.Windows.Forms.Button();
            this.r10n4 = new System.Windows.Forms.Button();
            this.r10p4 = new System.Windows.Forms.Button();
            this.r100n4 = new System.Windows.Forms.Button();
            this.r100p4 = new System.Windows.Forms.Button();
            this.r10n5 = new System.Windows.Forms.Button();
            this.r10p5 = new System.Windows.Forms.Button();
            this.r100n5 = new System.Windows.Forms.Button();
            this.r100p5 = new System.Windows.Forms.Button();
            this.r10n6 = new System.Windows.Forms.Button();
            this.r10p6 = new System.Windows.Forms.Button();
            this.r100n6 = new System.Windows.Forms.Button();
            this.r100p6 = new System.Windows.Forms.Button();
            this.r10n7 = new System.Windows.Forms.Button();
            this.r10p7 = new System.Windows.Forms.Button();
            this.r100n7 = new System.Windows.Forms.Button();
            this.r100p7 = new System.Windows.Forms.Button();
            this.unit_b = new System.Windows.Forms.TextBox();
            this.unit_s = new System.Windows.Forms.TextBox();
            this.c1 = new System.Windows.Forms.Button();
            this.c2 = new System.Windows.Forms.Button();
            this.c3 = new System.Windows.Forms.Button();
            this.c4 = new System.Windows.Forms.Button();
            this.c5 = new System.Windows.Forms.Button();
            this.c6 = new System.Windows.Forms.Button();
            this.c7 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // writetimeout
            // 
            this.writetimeout.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.writetimeout.FormattingEnabled = true;
            this.writetimeout.Items.AddRange(new object[] {
            "100",
            "200",
            "500",
            "1000",
            "2000",
            "5000"});
            this.writetimeout.Location = new System.Drawing.Point(188, 118);
            this.writetimeout.Name = "writetimeout";
            this.writetimeout.Size = new System.Drawing.Size(155, 49);
            this.writetimeout.TabIndex = 58;
            // 
            // readtimeout
            // 
            this.readtimeout.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.readtimeout.FormattingEnabled = true;
            this.readtimeout.Items.AddRange(new object[] {
            "100",
            "200",
            "500",
            "1000",
            "2000",
            "5000"});
            this.readtimeout.Location = new System.Drawing.Point(188, 229);
            this.readtimeout.Name = "readtimeout";
            this.readtimeout.Size = new System.Drawing.Size(155, 49);
            this.readtimeout.TabIndex = 57;
            // 
            // parity
            // 
            this.parity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.parity.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.parity.FormattingEnabled = true;
            this.parity.Items.AddRange(new object[] {
            "NONE",
            "ODD",
            "EVEN"});
            this.parity.Location = new System.Drawing.Point(188, 284);
            this.parity.Name = "parity";
            this.parity.Size = new System.Drawing.Size(155, 49);
            this.parity.TabIndex = 56;
            // 
            // baudrate
            // 
            this.baudrate.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.baudrate.FormattingEnabled = true;
            this.baudrate.Items.AddRange(new object[] {
            "300",
            "600",
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "43000",
            "56000",
            "57600",
            "115200"});
            this.baudrate.Location = new System.Drawing.Point(188, 174);
            this.baudrate.Name = "baudrate";
            this.baudrate.Size = new System.Drawing.Size(155, 49);
            this.baudrate.TabIndex = 55;
            this.baudrate.Text = "9600";
            // 
            // portname
            // 
            this.portname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.portname.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.portname.FormattingEnabled = true;
            this.portname.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9"});
            this.portname.Location = new System.Drawing.Point(188, 63);
            this.portname.Name = "portname";
            this.portname.Size = new System.Drawing.Size(155, 49);
            this.portname.TabIndex = 54;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Black;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(38, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(114, 41);
            this.label6.TabIndex = 53;
            this.label6.Text = "写超时";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Black;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(22, 229);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(146, 41);
            this.label5.TabIndex = 52;
            this.label5.Text = "读取超时";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Black;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(210, 41);
            this.label2.TabIndex = 51;
            this.label2.Text = "串口参数设置";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Black;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(38, 288);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(114, 41);
            this.label10.TabIndex = 49;
            this.label10.Text = "校验位";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Black;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(22, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 41);
            this.label3.TabIndex = 50;
            this.label3.Text = "串口名称";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Black;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(38, 178);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 41);
            this.label4.TabIndex = 48;
            this.label4.Text = "波特率";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(465, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 41);
            this.label1.TabIndex = 59;
            this.label1.Text = "收到";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Black;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(638, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 41);
            this.label7.TabIndex = 60;
            this.label7.Text = "返回";
            // 
            // i1
            // 
            this.i1.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.i1.Location = new System.Drawing.Point(428, 53);
            this.i1.MaxLength = 10;
            this.i1.Name = "i1";
            this.i1.Size = new System.Drawing.Size(157, 46);
            this.i1.TabIndex = 61;
            this.i1.Text = "2";
            this.i1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // r1
            // 
            this.r1.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.r1.Location = new System.Drawing.Point(597, 53);
            this.r1.MaxLength = 10;
            this.r1.Name = "r1";
            this.r1.Size = new System.Drawing.Size(157, 46);
            this.r1.TabIndex = 62;
            this.r1.Text = "2";
            this.r1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // i2
            // 
            this.i2.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.i2.Location = new System.Drawing.Point(428, 106);
            this.i2.MaxLength = 10;
            this.i2.Name = "i2";
            this.i2.Size = new System.Drawing.Size(157, 46);
            this.i2.TabIndex = 63;
            this.i2.Text = "4";
            this.i2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // r2
            // 
            this.r2.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.r2.Location = new System.Drawing.Point(597, 106);
            this.r2.MaxLength = 10;
            this.r2.Name = "r2";
            this.r2.Size = new System.Drawing.Size(157, 46);
            this.r2.TabIndex = 64;
            this.r2.Text = "2";
            this.r2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // i3
            // 
            this.i3.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.i3.Location = new System.Drawing.Point(428, 159);
            this.i3.MaxLength = 10;
            this.i3.Name = "i3";
            this.i3.Size = new System.Drawing.Size(157, 46);
            this.i3.TabIndex = 65;
            this.i3.Text = "5";
            this.i3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // r3
            // 
            this.r3.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.r3.Location = new System.Drawing.Point(597, 159);
            this.r3.MaxLength = 10;
            this.r3.Name = "r3";
            this.r3.Size = new System.Drawing.Size(157, 46);
            this.r3.TabIndex = 66;
            this.r3.Text = "2";
            this.r3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // open
            // 
            this.open.Location = new System.Drawing.Point(67, 357);
            this.open.Name = "open";
            this.open.Size = new System.Drawing.Size(98, 32);
            this.open.TabIndex = 67;
            this.open.Text = "连接";
            this.open.UseVisualStyleBackColor = true;
            this.open.Click += new System.EventHandler(this.open_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(171, 357);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(98, 32);
            this.close.TabIndex = 68;
            this.close.Text = "关闭";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // i6
            // 
            this.i6.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.i6.Location = new System.Drawing.Point(428, 318);
            this.i6.MaxLength = 10;
            this.i6.Name = "i6";
            this.i6.Size = new System.Drawing.Size(157, 46);
            this.i6.TabIndex = 71;
            this.i6.Text = "8";
            this.i6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // i5
            // 
            this.i5.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.i5.Location = new System.Drawing.Point(428, 265);
            this.i5.MaxLength = 10;
            this.i5.Name = "i5";
            this.i5.Size = new System.Drawing.Size(157, 46);
            this.i5.TabIndex = 70;
            this.i5.Text = "7";
            this.i5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // i4
            // 
            this.i4.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.i4.Location = new System.Drawing.Point(428, 212);
            this.i4.MaxLength = 10;
            this.i4.Name = "i4";
            this.i4.Size = new System.Drawing.Size(157, 46);
            this.i4.TabIndex = 69;
            this.i4.Text = "6";
            this.i4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // r6
            // 
            this.r6.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.r6.Location = new System.Drawing.Point(597, 318);
            this.r6.MaxLength = 10;
            this.r6.Name = "r6";
            this.r6.Size = new System.Drawing.Size(157, 46);
            this.r6.TabIndex = 74;
            this.r6.Text = "2";
            this.r6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // r5
            // 
            this.r5.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.r5.Location = new System.Drawing.Point(597, 265);
            this.r5.MaxLength = 10;
            this.r5.Name = "r5";
            this.r5.Size = new System.Drawing.Size(157, 46);
            this.r5.TabIndex = 73;
            this.r5.Text = "2";
            this.r5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // r4
            // 
            this.r4.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.r4.Location = new System.Drawing.Point(597, 212);
            this.r4.MaxLength = 10;
            this.r4.Name = "r4";
            this.r4.Size = new System.Drawing.Size(157, 46);
            this.r4.TabIndex = 72;
            this.r4.Text = "2";
            this.r4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // i7
            // 
            this.i7.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.i7.Location = new System.Drawing.Point(428, 372);
            this.i7.MaxLength = 10;
            this.i7.Name = "i7";
            this.i7.Size = new System.Drawing.Size(157, 46);
            this.i7.TabIndex = 75;
            this.i7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // r7
            // 
            this.r7.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.r7.Location = new System.Drawing.Point(597, 372);
            this.r7.MaxLength = 10;
            this.r7.Name = "r7";
            this.r7.Size = new System.Drawing.Size(157, 46);
            this.r7.TabIndex = 76;
            this.r7.Text = "2";
            this.r7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Black;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(894, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(0, 41);
            this.label9.TabIndex = 78;
            // 
            // r100p1
            // 
            this.r100p1.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.r100p1.Location = new System.Drawing.Point(799, 53);
            this.r100p1.Name = "r100p1";
            this.r100p1.Size = new System.Drawing.Size(35, 46);
            this.r100p1.TabIndex = 79;
            this.r100p1.Text = "+";
            this.r100p1.UseVisualStyleBackColor = true;
            this.r100p1.Click += new System.EventHandler(this.r100p1_Click);
            // 
            // r100n1
            // 
            this.r100n1.Font = new System.Drawing.Font("宋体", 22F);
            this.r100n1.Location = new System.Drawing.Point(840, 53);
            this.r100n1.Name = "r100n1";
            this.r100n1.Size = new System.Drawing.Size(35, 46);
            this.r100n1.TabIndex = 80;
            this.r100n1.Text = "-";
            this.r100n1.UseVisualStyleBackColor = true;
            this.r100n1.Click += new System.EventHandler(this.r100n1_Click);
            // 
            // r10n1
            // 
            this.r10n1.Font = new System.Drawing.Font("宋体", 22F);
            this.r10n1.Location = new System.Drawing.Point(926, 52);
            this.r10n1.Name = "r10n1";
            this.r10n1.Size = new System.Drawing.Size(35, 46);
            this.r10n1.TabIndex = 82;
            this.r10n1.Text = "-";
            this.r10n1.UseVisualStyleBackColor = true;
            this.r10n1.Click += new System.EventHandler(this.r10n1_Click);
            // 
            // r10p1
            // 
            this.r10p1.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.r10p1.Location = new System.Drawing.Point(885, 52);
            this.r10p1.Name = "r10p1";
            this.r10p1.Size = new System.Drawing.Size(35, 46);
            this.r10p1.TabIndex = 81;
            this.r10p1.Text = "+";
            this.r10p1.UseVisualStyleBackColor = true;
            this.r10p1.Click += new System.EventHandler(this.r10p1_Click);
            // 
            // r10n2
            // 
            this.r10n2.Font = new System.Drawing.Font("宋体", 22F);
            this.r10n2.Location = new System.Drawing.Point(926, 106);
            this.r10n2.Name = "r10n2";
            this.r10n2.Size = new System.Drawing.Size(35, 46);
            this.r10n2.TabIndex = 86;
            this.r10n2.Text = "-";
            this.r10n2.UseVisualStyleBackColor = true;
            this.r10n2.Click += new System.EventHandler(this.r10n2_Click);
            // 
            // r10p2
            // 
            this.r10p2.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.r10p2.Location = new System.Drawing.Point(885, 106);
            this.r10p2.Name = "r10p2";
            this.r10p2.Size = new System.Drawing.Size(35, 46);
            this.r10p2.TabIndex = 85;
            this.r10p2.Text = "+";
            this.r10p2.UseVisualStyleBackColor = true;
            this.r10p2.Click += new System.EventHandler(this.r10p2_Click);
            // 
            // r100n2
            // 
            this.r100n2.Font = new System.Drawing.Font("宋体", 22F);
            this.r100n2.Location = new System.Drawing.Point(840, 107);
            this.r100n2.Name = "r100n2";
            this.r100n2.Size = new System.Drawing.Size(35, 46);
            this.r100n2.TabIndex = 84;
            this.r100n2.Text = "-";
            this.r100n2.UseVisualStyleBackColor = true;
            this.r100n2.Click += new System.EventHandler(this.r100n2_Click);
            // 
            // r100p2
            // 
            this.r100p2.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.r100p2.Location = new System.Drawing.Point(799, 107);
            this.r100p2.Name = "r100p2";
            this.r100p2.Size = new System.Drawing.Size(35, 46);
            this.r100p2.TabIndex = 83;
            this.r100p2.Text = "+";
            this.r100p2.UseVisualStyleBackColor = true;
            this.r100p2.Click += new System.EventHandler(this.r100p2_Click);
            // 
            // r10n3
            // 
            this.r10n3.Font = new System.Drawing.Font("宋体", 22F);
            this.r10n3.Location = new System.Drawing.Point(926, 159);
            this.r10n3.Name = "r10n3";
            this.r10n3.Size = new System.Drawing.Size(35, 46);
            this.r10n3.TabIndex = 90;
            this.r10n3.Text = "-";
            this.r10n3.UseVisualStyleBackColor = true;
            this.r10n3.Click += new System.EventHandler(this.r10n3_Click);
            // 
            // r10p3
            // 
            this.r10p3.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.r10p3.Location = new System.Drawing.Point(885, 159);
            this.r10p3.Name = "r10p3";
            this.r10p3.Size = new System.Drawing.Size(35, 46);
            this.r10p3.TabIndex = 89;
            this.r10p3.Text = "+";
            this.r10p3.UseVisualStyleBackColor = true;
            this.r10p3.Click += new System.EventHandler(this.r10p3_Click);
            // 
            // r100n3
            // 
            this.r100n3.Font = new System.Drawing.Font("宋体", 22F);
            this.r100n3.Location = new System.Drawing.Point(840, 160);
            this.r100n3.Name = "r100n3";
            this.r100n3.Size = new System.Drawing.Size(35, 46);
            this.r100n3.TabIndex = 88;
            this.r100n3.Text = "-";
            this.r100n3.UseVisualStyleBackColor = true;
            this.r100n3.Click += new System.EventHandler(this.r100n3_Click);
            // 
            // r100p3
            // 
            this.r100p3.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.r100p3.Location = new System.Drawing.Point(799, 160);
            this.r100p3.Name = "r100p3";
            this.r100p3.Size = new System.Drawing.Size(35, 46);
            this.r100p3.TabIndex = 87;
            this.r100p3.Text = "+";
            this.r100p3.UseVisualStyleBackColor = true;
            this.r100p3.Click += new System.EventHandler(this.r100p3_Click);
            // 
            // r10n4
            // 
            this.r10n4.Font = new System.Drawing.Font("宋体", 22F);
            this.r10n4.Location = new System.Drawing.Point(926, 212);
            this.r10n4.Name = "r10n4";
            this.r10n4.Size = new System.Drawing.Size(35, 46);
            this.r10n4.TabIndex = 94;
            this.r10n4.Text = "-";
            this.r10n4.UseVisualStyleBackColor = true;
            this.r10n4.Click += new System.EventHandler(this.r10n4_Click);
            // 
            // r10p4
            // 
            this.r10p4.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.r10p4.Location = new System.Drawing.Point(885, 212);
            this.r10p4.Name = "r10p4";
            this.r10p4.Size = new System.Drawing.Size(35, 46);
            this.r10p4.TabIndex = 93;
            this.r10p4.Text = "+";
            this.r10p4.UseVisualStyleBackColor = true;
            this.r10p4.Click += new System.EventHandler(this.r10p4_Click);
            // 
            // r100n4
            // 
            this.r100n4.Font = new System.Drawing.Font("宋体", 22F);
            this.r100n4.Location = new System.Drawing.Point(840, 213);
            this.r100n4.Name = "r100n4";
            this.r100n4.Size = new System.Drawing.Size(35, 46);
            this.r100n4.TabIndex = 92;
            this.r100n4.Text = "-";
            this.r100n4.UseVisualStyleBackColor = true;
            this.r100n4.Click += new System.EventHandler(this.r100n4_Click);
            // 
            // r100p4
            // 
            this.r100p4.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.r100p4.Location = new System.Drawing.Point(799, 213);
            this.r100p4.Name = "r100p4";
            this.r100p4.Size = new System.Drawing.Size(35, 46);
            this.r100p4.TabIndex = 91;
            this.r100p4.Text = "+";
            this.r100p4.UseVisualStyleBackColor = true;
            this.r100p4.Click += new System.EventHandler(this.r100p4_Click);
            // 
            // r10n5
            // 
            this.r10n5.Font = new System.Drawing.Font("宋体", 22F);
            this.r10n5.Location = new System.Drawing.Point(926, 265);
            this.r10n5.Name = "r10n5";
            this.r10n5.Size = new System.Drawing.Size(35, 46);
            this.r10n5.TabIndex = 98;
            this.r10n5.Text = "-";
            this.r10n5.UseVisualStyleBackColor = true;
            this.r10n5.Click += new System.EventHandler(this.r10n5_Click);
            // 
            // r10p5
            // 
            this.r10p5.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.r10p5.Location = new System.Drawing.Point(885, 265);
            this.r10p5.Name = "r10p5";
            this.r10p5.Size = new System.Drawing.Size(35, 46);
            this.r10p5.TabIndex = 97;
            this.r10p5.Text = "+";
            this.r10p5.UseVisualStyleBackColor = true;
            this.r10p5.Click += new System.EventHandler(this.r10p5_Click);
            // 
            // r100n5
            // 
            this.r100n5.Font = new System.Drawing.Font("宋体", 22F);
            this.r100n5.Location = new System.Drawing.Point(840, 266);
            this.r100n5.Name = "r100n5";
            this.r100n5.Size = new System.Drawing.Size(35, 46);
            this.r100n5.TabIndex = 96;
            this.r100n5.Text = "-";
            this.r100n5.UseVisualStyleBackColor = true;
            this.r100n5.Click += new System.EventHandler(this.r100n5_Click);
            // 
            // r100p5
            // 
            this.r100p5.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.r100p5.Location = new System.Drawing.Point(799, 266);
            this.r100p5.Name = "r100p5";
            this.r100p5.Size = new System.Drawing.Size(35, 46);
            this.r100p5.TabIndex = 95;
            this.r100p5.Text = "+";
            this.r100p5.UseVisualStyleBackColor = true;
            this.r100p5.Click += new System.EventHandler(this.r100p5_Click);
            // 
            // r10n6
            // 
            this.r10n6.Font = new System.Drawing.Font("宋体", 22F);
            this.r10n6.Location = new System.Drawing.Point(926, 318);
            this.r10n6.Name = "r10n6";
            this.r10n6.Size = new System.Drawing.Size(35, 46);
            this.r10n6.TabIndex = 102;
            this.r10n6.Text = "-";
            this.r10n6.UseVisualStyleBackColor = true;
            this.r10n6.Click += new System.EventHandler(this.r10n6_Click);
            // 
            // r10p6
            // 
            this.r10p6.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.r10p6.Location = new System.Drawing.Point(885, 318);
            this.r10p6.Name = "r10p6";
            this.r10p6.Size = new System.Drawing.Size(35, 46);
            this.r10p6.TabIndex = 101;
            this.r10p6.Text = "+";
            this.r10p6.UseVisualStyleBackColor = true;
            this.r10p6.Click += new System.EventHandler(this.r10p6_Click);
            // 
            // r100n6
            // 
            this.r100n6.Font = new System.Drawing.Font("宋体", 22F);
            this.r100n6.Location = new System.Drawing.Point(840, 319);
            this.r100n6.Name = "r100n6";
            this.r100n6.Size = new System.Drawing.Size(35, 46);
            this.r100n6.TabIndex = 100;
            this.r100n6.Text = "-";
            this.r100n6.UseVisualStyleBackColor = true;
            this.r100n6.Click += new System.EventHandler(this.r100n6_Click);
            // 
            // r100p6
            // 
            this.r100p6.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.r100p6.Location = new System.Drawing.Point(799, 319);
            this.r100p6.Name = "r100p6";
            this.r100p6.Size = new System.Drawing.Size(35, 46);
            this.r100p6.TabIndex = 99;
            this.r100p6.Text = "+";
            this.r100p6.UseVisualStyleBackColor = true;
            this.r100p6.Click += new System.EventHandler(this.r100p6_Click);
            // 
            // r10n7
            // 
            this.r10n7.Font = new System.Drawing.Font("宋体", 22F);
            this.r10n7.Location = new System.Drawing.Point(926, 372);
            this.r10n7.Name = "r10n7";
            this.r10n7.Size = new System.Drawing.Size(35, 46);
            this.r10n7.TabIndex = 106;
            this.r10n7.Text = "-";
            this.r10n7.UseVisualStyleBackColor = true;
            this.r10n7.Click += new System.EventHandler(this.r10n7_Click);
            // 
            // r10p7
            // 
            this.r10p7.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.r10p7.Location = new System.Drawing.Point(885, 372);
            this.r10p7.Name = "r10p7";
            this.r10p7.Size = new System.Drawing.Size(35, 46);
            this.r10p7.TabIndex = 105;
            this.r10p7.Text = "+";
            this.r10p7.UseVisualStyleBackColor = true;
            this.r10p7.Click += new System.EventHandler(this.r10p7_Click);
            // 
            // r100n7
            // 
            this.r100n7.Font = new System.Drawing.Font("宋体", 22F);
            this.r100n7.Location = new System.Drawing.Point(840, 373);
            this.r100n7.Name = "r100n7";
            this.r100n7.Size = new System.Drawing.Size(35, 46);
            this.r100n7.TabIndex = 104;
            this.r100n7.Text = "-";
            this.r100n7.UseVisualStyleBackColor = true;
            this.r100n7.Click += new System.EventHandler(this.r100n7_Click);
            // 
            // r100p7
            // 
            this.r100p7.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.r100p7.Location = new System.Drawing.Point(799, 373);
            this.r100p7.Name = "r100p7";
            this.r100p7.Size = new System.Drawing.Size(35, 46);
            this.r100p7.TabIndex = 103;
            this.r100p7.Text = "+";
            this.r100p7.UseVisualStyleBackColor = true;
            this.r100p7.Click += new System.EventHandler(this.r100p7_Click);
            // 
            // unit_b
            // 
            this.unit_b.Font = new System.Drawing.Font("微软雅黑", 18F);
            this.unit_b.Location = new System.Drawing.Point(799, 9);
            this.unit_b.MaxLength = 10;
            this.unit_b.Name = "unit_b";
            this.unit_b.Size = new System.Drawing.Size(76, 39);
            this.unit_b.TabIndex = 107;
            this.unit_b.Text = "100";
            this.unit_b.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // unit_s
            // 
            this.unit_s.Font = new System.Drawing.Font("微软雅黑", 18F);
            this.unit_s.Location = new System.Drawing.Point(885, 9);
            this.unit_s.MaxLength = 10;
            this.unit_s.Name = "unit_s";
            this.unit_s.Size = new System.Drawing.Size(76, 39);
            this.unit_s.TabIndex = 108;
            this.unit_s.Text = "50";
            this.unit_s.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // c1
            // 
            this.c1.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.c1.Location = new System.Drawing.Point(760, 53);
            this.c1.Name = "c1";
            this.c1.Size = new System.Drawing.Size(35, 46);
            this.c1.TabIndex = 109;
            this.c1.Text = "0";
            this.c1.UseVisualStyleBackColor = true;
            this.c1.Click += new System.EventHandler(this.c1_Click);
            // 
            // c2
            // 
            this.c2.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.c2.Location = new System.Drawing.Point(760, 106);
            this.c2.Name = "c2";
            this.c2.Size = new System.Drawing.Size(35, 46);
            this.c2.TabIndex = 110;
            this.c2.Text = "0";
            this.c2.UseVisualStyleBackColor = true;
            this.c2.Click += new System.EventHandler(this.c2_Click);
            // 
            // c3
            // 
            this.c3.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.c3.Location = new System.Drawing.Point(760, 159);
            this.c3.Name = "c3";
            this.c3.Size = new System.Drawing.Size(35, 46);
            this.c3.TabIndex = 111;
            this.c3.Text = "0";
            this.c3.UseVisualStyleBackColor = true;
            this.c3.Click += new System.EventHandler(this.c3_Click);
            // 
            // c4
            // 
            this.c4.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.c4.Location = new System.Drawing.Point(760, 211);
            this.c4.Name = "c4";
            this.c4.Size = new System.Drawing.Size(35, 46);
            this.c4.TabIndex = 112;
            this.c4.Text = "0";
            this.c4.UseVisualStyleBackColor = true;
            this.c4.Click += new System.EventHandler(this.c4_Click);
            // 
            // c5
            // 
            this.c5.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.c5.Location = new System.Drawing.Point(760, 265);
            this.c5.Name = "c5";
            this.c5.Size = new System.Drawing.Size(35, 46);
            this.c5.TabIndex = 113;
            this.c5.Text = "0";
            this.c5.UseVisualStyleBackColor = true;
            this.c5.Click += new System.EventHandler(this.c5_Click);
            // 
            // c6
            // 
            this.c6.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.c6.Location = new System.Drawing.Point(760, 319);
            this.c6.Name = "c6";
            this.c6.Size = new System.Drawing.Size(35, 46);
            this.c6.TabIndex = 114;
            this.c6.Text = "0";
            this.c6.UseVisualStyleBackColor = true;
            this.c6.Click += new System.EventHandler(this.c6_Click);
            // 
            // c7
            // 
            this.c7.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.c7.Location = new System.Drawing.Point(760, 373);
            this.c7.Name = "c7";
            this.c7.Size = new System.Drawing.Size(35, 46);
            this.c7.TabIndex = 115;
            this.c7.Text = "0";
            this.c7.UseVisualStyleBackColor = true;
            this.c7.Click += new System.EventHandler(this.c7_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1050, 473);
            this.Controls.Add(this.c7);
            this.Controls.Add(this.c6);
            this.Controls.Add(this.c5);
            this.Controls.Add(this.c4);
            this.Controls.Add(this.c3);
            this.Controls.Add(this.c2);
            this.Controls.Add(this.c1);
            this.Controls.Add(this.unit_s);
            this.Controls.Add(this.unit_b);
            this.Controls.Add(this.r10n7);
            this.Controls.Add(this.r10p7);
            this.Controls.Add(this.r100n7);
            this.Controls.Add(this.r100p7);
            this.Controls.Add(this.r10n6);
            this.Controls.Add(this.r10p6);
            this.Controls.Add(this.r100n6);
            this.Controls.Add(this.r100p6);
            this.Controls.Add(this.r10n5);
            this.Controls.Add(this.r10p5);
            this.Controls.Add(this.r100n5);
            this.Controls.Add(this.r100p5);
            this.Controls.Add(this.r10n4);
            this.Controls.Add(this.r10p4);
            this.Controls.Add(this.r100n4);
            this.Controls.Add(this.r100p4);
            this.Controls.Add(this.r10n3);
            this.Controls.Add(this.r10p3);
            this.Controls.Add(this.r100n3);
            this.Controls.Add(this.r100p3);
            this.Controls.Add(this.r10n2);
            this.Controls.Add(this.r10p2);
            this.Controls.Add(this.r100n2);
            this.Controls.Add(this.r100p2);
            this.Controls.Add(this.r10n1);
            this.Controls.Add(this.r10p1);
            this.Controls.Add(this.r100n1);
            this.Controls.Add(this.r100p1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.r7);
            this.Controls.Add(this.i7);
            this.Controls.Add(this.r6);
            this.Controls.Add(this.r5);
            this.Controls.Add(this.r4);
            this.Controls.Add(this.i6);
            this.Controls.Add(this.i5);
            this.Controls.Add(this.i4);
            this.Controls.Add(this.close);
            this.Controls.Add(this.open);
            this.Controls.Add(this.r3);
            this.Controls.Add(this.i3);
            this.Controls.Add(this.r2);
            this.Controls.Add(this.i2);
            this.Controls.Add(this.r1);
            this.Controls.Add(this.i1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.writetimeout);
            this.Controls.Add(this.readtimeout);
            this.Controls.Add(this.parity);
            this.Controls.Add(this.baudrate);
            this.Controls.Add(this.portname);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox writetimeout;
        private System.Windows.Forms.ComboBox readtimeout;
        private System.Windows.Forms.ComboBox parity;
        private System.Windows.Forms.ComboBox baudrate;
        private System.Windows.Forms.ComboBox portname;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox i1;
        private System.Windows.Forms.TextBox r1;
        private System.Windows.Forms.TextBox i2;
        private System.Windows.Forms.TextBox r2;
        private System.Windows.Forms.TextBox i3;
        private System.Windows.Forms.TextBox r3;
        private System.Windows.Forms.Button open;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.TextBox i6;
        private System.Windows.Forms.TextBox i5;
        private System.Windows.Forms.TextBox i4;
        private System.Windows.Forms.TextBox r6;
        private System.Windows.Forms.TextBox r5;
        private System.Windows.Forms.TextBox r4;
        private System.Windows.Forms.TextBox i7;
        private System.Windows.Forms.TextBox r7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button r100p1;
        private System.Windows.Forms.Button r100n1;
        private System.Windows.Forms.Button r10n1;
        private System.Windows.Forms.Button r10p1;
        private System.Windows.Forms.Button r10n2;
        private System.Windows.Forms.Button r10p2;
        private System.Windows.Forms.Button r100n2;
        private System.Windows.Forms.Button r100p2;
        private System.Windows.Forms.Button r10n3;
        private System.Windows.Forms.Button r10p3;
        private System.Windows.Forms.Button r100n3;
        private System.Windows.Forms.Button r100p3;
        private System.Windows.Forms.Button r10n4;
        private System.Windows.Forms.Button r10p4;
        private System.Windows.Forms.Button r100n4;
        private System.Windows.Forms.Button r100p4;
        private System.Windows.Forms.Button r10n5;
        private System.Windows.Forms.Button r10p5;
        private System.Windows.Forms.Button r100n5;
        private System.Windows.Forms.Button r100p5;
        private System.Windows.Forms.Button r10n6;
        private System.Windows.Forms.Button r10p6;
        private System.Windows.Forms.Button r100n6;
        private System.Windows.Forms.Button r100p6;
        private System.Windows.Forms.Button r10n7;
        private System.Windows.Forms.Button r10p7;
        private System.Windows.Forms.Button r100n7;
        private System.Windows.Forms.Button r100p7;
        private System.Windows.Forms.TextBox unit_b;
        private System.Windows.Forms.TextBox unit_s;
        private System.Windows.Forms.Button c1;
        private System.Windows.Forms.Button c2;
        private System.Windows.Forms.Button c3;
        private System.Windows.Forms.Button c4;
        private System.Windows.Forms.Button c5;
        private System.Windows.Forms.Button c6;
        private System.Windows.Forms.Button c7;
    }
}

