using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;

namespace zgkk_sp_simulator
{
    // Physical range, serial range and inversion of each model quantity; edits apply to the channels at once
    public partial class ChannelsForm : Form
    {
        bool reverting;

        public ChannelsForm() : this(new List<Channel>()) { }

        public ChannelsForm(List<Channel> channels)
        {
            InitializeComponent();
            foreach (Channel c in channels)
            {
                int i = grid.Rows.Add(c.Name, Num(c.From), Num(c.To), c.RawMin.ToString(), c.RawMax.ToString(), c.Invert);
                grid.Rows[i].Tag = c;
            }
        }

        static string Num(double v) { return v.ToString(CultureInfo.InvariantCulture); }

        // Commit checkbox clicks immediately instead of on leaving the cell
        private void grid_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (grid.IsCurrentCellDirty && grid.CurrentCell is DataGridViewCheckBoxCell) grid.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void grid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (reverting || e.RowIndex < 0) return;
            DataGridViewCell cell = grid.Rows[e.RowIndex].Cells[e.ColumnIndex];
            Channel c = (Channel)grid.Rows[e.RowIndex].Tag;
            object v = cell.Value;
            string t = Convert.ToString(v, CultureInfo.InvariantCulture)?.Trim() ?? "";
            bool ok = true;
            switch (e.ColumnIndex)
            {
                case 1: ok = double.TryParse(t, NumberStyles.Float, CultureInfo.InvariantCulture, out double f) && f != c.To; if (ok) c.From = f; break;
                case 2: ok = double.TryParse(t, NumberStyles.Float, CultureInfo.InvariantCulture, out double to) && to != c.From; if (ok) c.To = to; break;
                case 3: ok = int.TryParse(t, out int rmin) && rmin >= 0 && rmin < c.RawMax; if (ok) c.RawMin = rmin; break;
                case 4: ok = int.TryParse(t, out int rmax) && rmax > c.RawMin && rmax <= 0xFFFF; if (ok) c.RawMax = rmax; break;
                case 5: c.Invert = v is bool b && b; break;
            }
            if (ok) return;
            MessageBox.Show($"「{t}」不是有效值，已恢复。", Text);
            string[] back = { null, Num(c.From), Num(c.To), c.RawMin.ToString(), c.RawMax.ToString() };
            BeginInvoke(new Action(() => { reverting = true; cell.Value = back[e.ColumnIndex]; reverting = false; }));
        }
    }
}
