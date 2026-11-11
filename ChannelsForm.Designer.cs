namespace zgkk_sp_simulator
{
    partial class ChannelsForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        private void InitializeComponent()
        {
            this.grid = new System.Windows.Forms.DataGridView();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRawMin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRawMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInvert = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            //
            // grid
            //
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.AllowUserToResizeRows = false;
            this.grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colFrom,
            this.colTo,
            this.colRawMin,
            this.colRawMax,
            this.colInvert});
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(0, 0);
            this.grid.Name = "grid";
            this.grid.RowHeadersVisible = false;
            this.grid.Size = new System.Drawing.Size(640, 200);
            this.grid.TabIndex = 0;
            this.grid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellValueChanged);
            this.grid.CurrentCellDirtyStateChanged += new System.EventHandler(this.grid_CurrentCellDirtyStateChanged);
            //
            // colName
            //
            this.colName.FillWeight = 130F;
            this.colName.HeaderText = "数据";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            //
            // colFrom
            //
            this.colFrom.HeaderText = "量程下限";
            this.colFrom.Name = "colFrom";
            //
            // colTo
            //
            this.colTo.HeaderText = "量程上限";
            this.colTo.Name = "colTo";
            //
            // colRawMin
            //
            this.colRawMin.HeaderText = "串口下限";
            this.colRawMin.Name = "colRawMin";
            //
            // colRawMax
            //
            this.colRawMax.HeaderText = "串口上限";
            this.colRawMax.Name = "colRawMax";
            //
            // colInvert
            //
            this.colInvert.FillWeight = 50F;
            this.colInvert.HeaderText = "翻转";
            this.colInvert.Name = "colInvert";
            //
            // ChannelsForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 200);
            this.Controls.Add(this.grid);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChannelsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "通道设置（量程和串口范围要和上位机的传感器设置一致）";
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFrom;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRawMin;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRawMax;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colInvert;
    }
}
