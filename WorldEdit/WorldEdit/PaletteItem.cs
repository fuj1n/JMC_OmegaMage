using System.Drawing;
using System.Windows.Forms;

namespace WorldEdit
{
    public partial class PaletteItem : UserControl
    {
        public delegate bool PaletteSelectedCallback(object param);

        public Image Image
        {
            get
            {
                return picture.Image;
            }
            set
            {
                picture.Image = value;
            }
        }

        public new string Text
        {
            get
            {
                return label.Text;
            }
            set
            {
                label.Text = value;
            }
        }

        public PaletteSelectedCallback selectCallback;
        public object selectCallbackParam;

        public PaletteItem()
        {
            InitializeComponent();

            selectCallbackParam = this;

            picture.Click += (s, e) => SelectControl();
            label.Click += (s, e) => SelectControl();
        }

        public void SelectControl()
        {
            if (selectCallback != null && selectCallback.Invoke(selectCallbackParam))
                BackColor = SystemColors.Highlight;
        }

        public void ResetSelection()
        {
            BackColor = Color.FromArgb(48, 48, 48);
        }
    }
}
