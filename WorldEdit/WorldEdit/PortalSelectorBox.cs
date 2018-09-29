using System.Windows.Forms;

namespace WorldEdit
{
    public partial class PortalSelectorBox : Form
    {
        public PortalSelectorBox()
        {
            InitializeComponent();
            text.KeyPress += (s, e) => { if (e.KeyChar == (char)Keys.Return) { selectbutton.PerformClick(); e.Handled = true; } };
        }
    }
}
