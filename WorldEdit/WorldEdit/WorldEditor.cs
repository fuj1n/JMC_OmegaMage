using System.Windows.Forms;

namespace WorldEdit
{
    public partial class WorldEditor : Form
    {
        private RoomsFile roomsFile;

        private char[,] currentRoom;

        public WorldEditor()
        {
            InitializeComponent();

            roomsFile = new RoomsFile();
            currentRoom = new char[roomsFile.roomSize, roomsFile.roomSize];
        }

        public WorldEditor(string path) : this()
        {
        }

        private void CloseMenu(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
