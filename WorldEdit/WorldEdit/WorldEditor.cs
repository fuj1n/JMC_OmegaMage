using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WorldEdit
{
    public partial class WorldEditor : Form
    {
        const int gridSize = 32;

        private RoomsFile roomsFile;
        private static WorldFile world;

        private Dictionary<char, List<List<char>>> rooms;
        private char selectedRoom;
        private List<List<char>> currentRoom;

        private int roomWidth;
        private int roomHeight;

        private char selection = '\0';

        public WorldEditor()
        {
            InitializeComponent();

            if (world == null)
            {
                world = JsonConvert.DeserializeObject<WorldFile>(File.ReadAllText("definition/world.json"));
                world.LoadTextures("definition/textures");
            }

            addRowBtn.Click += (o, e) => AddRow();
            addColBtn.Click += (o, e) => AddColumn();
            remRowBtn.Click += (o, e) => RemoveRow();
            remColBtn.Click += (o, e) => RemoveColumn();
            setSizeBtn.Click += (o, e) =>
            {
                using (WorldSizeSetter dialog = new WorldSizeSetter())
                {
                    dialog.width.Value = roomWidth;
                    dialog.height.Value = roomHeight;

                    if (dialog.ShowDialog(this) == DialogResult.OK)
                        SetSize((int)dialog.width.Value, (int)dialog.height.Value);
                }
            };

            roomsFile = new RoomsFile();
            selectedRoom = roomsFile.startingRoom;
            rooms = new Dictionary<char, List<List<char>>>();
            currentRoom = new List<List<char>>();

            foreach (KeyValuePair<char, string> pair in world.keys)
            {
                PaletteItem pi = new PaletteItem
                {
                    Parent = palette,
                    Image = world.GetTexture(pair.Value),
                    Text = pair.Key.ToString()
                };
                if (world.labels.ContainsKey(pair.Key))
                    pi.Text += " " + world.labels[pair.Key];
                pi.selectCallbackParam = pair.Key;
                pi.selectCallback = SelectPalette;
            }

            PaletteItem portal = new PaletteItem
            {
                Parent = palette,
                Image = world.GetTexture(world.portal),
                Text = "(Portal)",
                selectCallbackParam = "portal",
                selectCallback = SelectPalette
            };

            UpdateWorld();
        }

        public WorldEditor(string path) : this()
        {
        }

        private bool SelectPalette(object o)
        {
            ResetPalette();
            string s = o.ToString();

            if ("portal".Equals(s))
            {
                using (PortalSelectorBox pb = new PortalSelectorBox())
                {
                    if (pb.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(pb.text.Text))
                        s = pb.text.Text;
                    else
                    {
                        ResetPalette();
                        return false;
                    }
                }
            }

            selection = s[0];

            return true;
        }

        private void ResetPalette()
        {
            foreach (PaletteItem i in palette.Controls.OfType<PaletteItem>())
            {
                i.ResetSelection();
            }

            selection = '\0';
        }

        private void SelectRoom(char c)
        {
            if (!rooms.ContainsKey(c))
                return;

            rooms[selectedRoom] = currentRoom;
            selectedRoom = c;
            currentRoom = rooms[selectedRoom];

            UpdateWorld();
        }

        private void AddRow()
        {
            roomHeight++;
            currentRoom.Add(Enumerable.Repeat(' ', roomWidth).ToList());

            UpdateWorld();
        }

        private void AddColumn()
        {
            roomWidth++;
            currentRoom.ForEach(x => x.Add(' '));

            UpdateWorld();
        }

        private void RemoveRow()
        {
            if (roomHeight == 0)
                return;

            roomHeight--;
            currentRoom.RemoveAt(currentRoom.Count - 1);

            UpdateWorld();
        }

        private void RemoveColumn()
        {
            if (roomWidth == 0)
                return;

            roomWidth--;
            currentRoom.ForEach(x => x.RemoveAt(x.Count - 1));

            UpdateWorld();
        }

        private void SetSize(int newX, int newY)
        {
            List<List<char>> newSize = new List<List<char>>();

            for (int i = 0; i < newY; i++)
            {
                newSize.Add(Enumerable.Repeat(' ', newX).ToList());
            }

            for (int y = 0; y < newY && y < roomHeight; y++)
            {
                for (int x = 0; x < newX && x < roomWidth; x++)
                {
                    newSize[y][x] = currentRoom[y][x];
                }
            }

            roomWidth = newX;
            roomHeight = newY;

            currentRoom = newSize;
            UpdateWorld();
        }

        private void UpdateWorld()
        {
            SuspendLayout();

            foreach (Control c in worldTable.Controls)
            {
                c.Dispose();
            }

            worldTable.Controls.Clear();

            worldTable.RowCount = roomHeight;
            worldTable.ColumnCount = roomWidth;

            worldTable.RowStyles.Clear();
            worldTable.ColumnStyles.Clear();

            for (int y = 0; y < roomHeight; y++)
            {
                worldTable.RowStyles.Add(new RowStyle(SizeType.Absolute, gridSize));
                for (int x = 0; x < roomWidth; x++)
                {
                    worldTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, gridSize));
                    char c = currentRoom[y][x];

                    string texture = GetTileTexture(c);

                    PictureBox pb = new PictureBox();
                    pb.Parent = worldTable;
                    pb.Dock = DockStyle.Fill;
                    pb.BackgroundImage = world.GetTexture(world.background);
                    pb.Image = world.GetTexture(texture);
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;

                    worldTable.SetRow(pb, y);
                    worldTable.SetColumn(pb, x);

                    pb.Click += SetCell;
                }
            }

            ResumeLayout();
        }

        private string GetTileTexture(char tile)
        {
            string texture;

            if (world.keys.ContainsKey(tile))
                texture = world.keys[tile];
            else
                texture = "missingno";

            return texture;
        }

        private void SetCell(object s, EventArgs @event)
        {
            if (!(s is PictureBox))
                return;

            PictureBox sender = (PictureBox)s;

            SetCell(worldTable.GetColumn(sender), worldTable.GetRow(sender), sender);
        }

        private void SetCell(int x, int y, PictureBox pb)
        {
            if (selection == '\0')
                return;

            currentRoom[y][x] = selection;
            pb.Image = world.GetTexture(GetTileTexture(selection));
        }

        private void CloseMenu(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
