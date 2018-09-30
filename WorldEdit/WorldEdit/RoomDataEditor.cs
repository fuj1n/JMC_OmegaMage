using System.Windows.Forms;

namespace WorldEdit
{
    public partial class RoomDataEditor : Form
    {
        public char RoomId
        {
            get
            {
                return roomId;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(templateText))
                    templateText = windowLabel.Text;

                roomId = value;
                windowLabel.Text = string.Format(templateText, roomId);
            }
        }

        private string templateText;
        private char roomId = '0';

        public RoomDataEditor()
        {
            InitializeComponent();
        }
    }
}
