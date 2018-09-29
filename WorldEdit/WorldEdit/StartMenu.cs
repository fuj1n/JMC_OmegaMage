using System;
using System.Windows.Forms;

namespace WorldEdit
{
    public partial class StartMenu : Form
    {
        public StartMenu()
        {
            InitializeComponent();
        }

        private void NewWorld(object sender, EventArgs e)
        {
            new WorldEditor().ShowDialog();
        }

        private void LoadWorld(object sender, EventArgs e)
        {
            DialogResult result = openWorldDialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                new WorldEditor(openWorldDialog.FileName).ShowDialog(this);
            }
        }
    }
}
