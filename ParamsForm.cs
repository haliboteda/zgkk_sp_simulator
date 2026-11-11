using System.Windows.Forms;

namespace zgkk_sp_simulator
{
    // Valve parameters of the selected bench; edits go straight into the model
    public partial class ParamsForm : Form
    {
        public ParamsForm() : this(null, "") { }

        public ParamsForm(object view, string benchName)
        {
            InitializeComponent();
            grid.SelectedObject = view;
            Text = "参数 - " + benchName;
        }
    }
}
