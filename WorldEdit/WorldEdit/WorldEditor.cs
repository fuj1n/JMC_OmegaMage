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

        private static WorldFile world;

        private Dictionary<char, List<List<char>>> rooms;
        private char currentRoomId = '0';
        private List<List<char>> currentRoom;

        private int roomWidth;
        private int roomHeight;

        private char selection = '\0';

        private Dictionary<char, string> customPortals = new Dictionary<char, string>();
        private char startingRoom = '0';
        private Dictionary<char, Room> roomsData = new Dictionary<char, Room>();

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

            btnNudgeUp.Click += (o, e) => Nudge(ArrowDirection.Up);
            btnNudgeDown.Click += (o, e) => Nudge(ArrowDirection.Down);
            btnNudgeLeft.Click += (o, e) => Nudge(ArrowDirection.Left);
            btnNudgeRight.Click += (o, e) => Nudge(ArrowDirection.Right);

            btnNew.Click += (o, e) =>
            {
                using (RoomSelectorBox sel = new RoomSelectorBox())
                {
                    if (sel.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(sel.text.Text))
                    {
                        CreateRoom(sel.text.Text[0]);
                    }
                }
            };
            btnDelete.Click += (o, e) => DeleteRoom();

            btnProperties.Click += (o, e) =>
            {
                using (RoomDataEditor rd = new RoomDataEditor())
                {
                    if (!roomsData.ContainsKey(currentRoomId))
                        roomsData[currentRoomId] = new Room();

                    rd.RoomId = currentRoomId;

                    rd.floor.Text = roomsData[currentRoomId].floor;
                    rd.wall.Text = roomsData[currentRoomId].wall;

                    if (rd.ShowDialog() == DialogResult.OK)
                    {
                        if (!roomsData.ContainsKey(currentRoomId))
                            roomsData[currentRoomId] = new Room();
                        if (!string.IsNullOrWhiteSpace(rd.floor.Text))
                            roomsData[currentRoomId].floor = rd.floor.Text;
                        if (!string.IsNullOrWhiteSpace(rd.wall.Text))
                            roomsData[currentRoomId].wall = rd.wall.Text;

                        SoftUpdateWorld();
                    }
                }
            };

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
            //try
            //{
            RoomsFile rf = JsonConvert.DeserializeObject<RoomsFile>(File.ReadAllText(path));

            customPortals = rf.customPortals;
            startingRoom = rf.startingRoom;
            roomsData = rf.rooms;
            rooms.Clear();

            foreach (KeyValuePair<char, Room> room in roomsData)
            {
                List<List<char>> roomsTable = new List<List<char>>();

                foreach (string row in room.Value.layout.Split('\n').Select(r => r.Trim('\t', '\r')))
                {
                    roomsTable.Add(row.ToList());
                }

                rooms[room.Key] = roomsTable;
            }

            DeleteRoom();
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLineFormatted("Cannot read \"{0}\" + (" + e.GetType().Name + ")", Color.Green, Color.DarkRed, path);
            //}
        }

        private void Save()
        {

        }

        private bool SelectPalette(object o)
        {
            ResetPalette();
            string s = o.ToString();

            if ("portal".Equals(s))
            {
                using (RoomSelectorBox pb = new RoomSelectorBox())
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

        private void CreateRoom(char c)
        {
            if (currentRoomId == c)
                return;
            if (!rooms.ContainsKey(c))
                rooms.Add(c, new List<List<char>>());

            SelectRoom(c);
        }

        private void SelectRoom(char c)
        {
            if (!rooms.ContainsKey(c))
                return;

            if (currentRoomId != '\0')
                rooms[currentRoomId] = currentRoom;
            currentRoomId = c;
            currentRoom = rooms[currentRoomId];
            rooms.Remove(currentRoomId);

            int highestColumnCount = 0;
            currentRoom.ForEach(row => highestColumnCount = Math.Max(highestColumnCount, row.Count));

            roomHeight = currentRoom.Count;
            roomWidth = highestColumnCount;

            currentRoom.ForEach(row =>
            {
                while (row.Count < highestColumnCount)
                    row.Add(' ');
            });

            UpdateWorld();
        }

        private void DeleteRoom()
        {
            if (roomsData.ContainsKey(currentRoomId))
                roomsData.Remove(currentRoomId);

            currentRoomId = '\0';
            if (rooms.Count == 0)
                CreateRoom(startingRoom);
            else if (rooms.ContainsKey(startingRoom))
                SelectRoom(startingRoom);
            else
                SelectRoom(rooms.First().Key);
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

        private void Nudge(ArrowDirection direction)
        {
            switch (direction)
            {
                case ArrowDirection.Up:
                    if (roomHeight == 0)
                        return;

                    currentRoom.RemoveAt(0);
                    currentRoom.Add(Enumerable.Repeat(' ', roomWidth).ToList());
                    break;
                case ArrowDirection.Down:
                    if (roomHeight == 0)
                        return;

                    currentRoom.RemoveAt(currentRoom.Count - 1);
                    currentRoom.Insert(0, Enumerable.Repeat(' ', roomWidth).ToList());
                    break;
                case ArrowDirection.Left:
                    foreach (List<char> row in currentRoom)
                    {
                        row.RemoveAt(0);
                        row.Add(' ');
                    }
                    break;
                case ArrowDirection.Right:
                    foreach (List<char> row in currentRoom)
                    {
                        row.RemoveAt(row.Count - 1);
                        row.Insert(0, ' ');
                    }
                    break;
            }

            SoftUpdateWorld();
        }

        private void SoftUpdateWorld()
        {
            foreach (PictureBox pb in worldTable.Controls.OfType<PictureBox>())
            {
                pb.Image = world.GetTexture(GetTileTexture(currentRoom[worldTable.GetRow(pb)][worldTable.GetColumn(pb)]));
            }
        }

        private void UpdateWorld()
        {
            SuspendLayout();

            labelGridSize.Text = "Grid Size: " + roomWidth + " x " + roomHeight + "\nCurrent Room: " + currentRoomId;

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

                    PictureBox pb = new PictureBox();
                    pb.Parent = worldTable;
                    pb.Dock = DockStyle.Fill;
                    pb.BackgroundImage = world.GetTexture(world.background);
                    pb.Image = world.GetTexture(GetTileTexture(c));
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;

                    worldTable.SetRow(pb, y);
                    worldTable.SetColumn(pb, x);

                    pb.Click += SetCell;
                }
            }

            ResumeLayout();

            PopulateRoomSelector();
        }

        private string GetTileTexture(char tile)
        {
            string texture = "missingno";
            if (world.keys.ContainsKey(tile))
                texture = world.keys[tile];
            else if (rooms.ContainsKey(tile) || currentRoomId == tile)
                texture = world.portal;

            if (texture.StartsWith("$") && !texture.Equals("$blank") && roomsData.ContainsKey(currentRoomId))
            {
                string tex = "";
                switch (texture.Substring(1))
                {
                    case "wall":
                        tex = roomsData[currentRoomId].wall;
                        break;
                    case "floor":
                        tex = roomsData[currentRoomId].floor;
                        break;
                }

                if (!string.IsNullOrWhiteSpace(tex) && world.tags.ContainsKey(tex))
                {
                    texture = tex;
                }
            }

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

        private void PopulateRoomSelector()
        {
            btnSelect.DropDownItems.Clear();

            foreach (char room in rooms.Select(x => x.Key))
            {
                ToolStripMenuItem i = new ToolStripMenuItem();

                i.Text = room.ToString();
                i.Click += (o, e) => SelectRoom(room);

                btnSelect.DropDownItems.Add(i);
            }

            btnSelect.Enabled = btnSelect.DropDownItems.Count != 0;
        }
    }
}
