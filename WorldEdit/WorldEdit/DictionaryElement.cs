using System.Windows.Forms;

namespace WorldEdit
{
    public partial class DictionaryElement : UserControl
    {
        public delegate void OnClickRemove(DictionaryElement element);

        public string Key
        {
            get
            {
                return key.Text;
            }
            set
            {
                key.Text = value; ;
            }
        }

        public string Value
        {
            get
            {
                return val.Text;
            }
            set
            {
                val.Text = value;
            }
        }

        public OnClickRemove onClickRemove;

        public DictionaryElement()
        {
            InitializeComponent();

            btnRemove.Click += (o, e) => onClickRemove?.Invoke(this);
        }
    }
}
