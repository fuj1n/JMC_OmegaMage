using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Console = Colorful.Console;

namespace WorldEdit
{
    public partial class WorldEditor : Form
    {
        const int gridSize = 48;

        public static WorldFile World
        {
            get
            {
                if (_world == null)
                {
                    _world = JsonConvert.DeserializeObject<WorldFile>(File.ReadAllText("definition/world.json"));
                    _world.LoadTextures("definition/textures");
                }

                return _world;
            }
        }
        private static WorldFile _world;

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

            // Ensure the world gets loaded here
            WorldFile temp = World;

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
            btnRename.Click += (o, e) =>
            {
                using (RoomSelectorBox sel = new RoomSelectorBox())
                {
                    sel.text.Text = currentRoomId.ToString();
                    if (sel.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(sel.text.Text) && !rooms.ContainsKey(sel.text.Text[0]))
                    {
                        currentRoomId = sel.text.Text[0];
                    }
                }
            };
            btnDelete.Click += (o, e) => DeleteRoom();
            btnSave.Click += (o, e) => Save();
            btnProperties.Click += (o, e) =>
            {
                using (RoomDataEditor rd = new RoomDataEditor())
                {
                    if (!roomsData.ContainsKey(currentRoomId))
                        roomsData[currentRoomId] = new Room();

                    rd.RoomId = currentRoomId;

                    rd.floor.Text = roomsData[currentRoomId].floor;
                    rd.floor2.Text = roomsData[currentRoomId].floor2;
                    rd.wall.Text = roomsData[currentRoomId].wall;
                    rd.wall2.Text = roomsData[currentRoomId].wall2;

                    rd.floorHeight.Value = (decimal)roomsData[currentRoomId].floorHeight;
                    rd.floor2Height.Value = (decimal)roomsData[currentRoomId].floor2Height;
                    rd.wallHeight.Value = (decimal)roomsData[currentRoomId].wallHeight;
                    rd.wall2Height.Value = (decimal)roomsData[currentRoomId].wall2Height;

                    if (rd.ShowDialog() == DialogResult.OK)
                    {
                        if (!roomsData.ContainsKey(currentRoomId))
                            roomsData[currentRoomId] = new Room();
                        if (!string.IsNullOrWhiteSpace(rd.floor.Text))
                            roomsData[currentRoomId].floor = rd.floor.Text;
                        if (!string.IsNullOrWhiteSpace(rd.wall.Text))
                            roomsData[currentRoomId].wall = rd.wall.Text;
                        if (!string.IsNullOrWhiteSpace(rd.floor2.Text))
                            roomsData[currentRoomId].floor2 = rd.floor2.Text;
                        if (!string.IsNullOrWhiteSpace(rd.wall2.Text))
                            roomsData[currentRoomId].wall2 = rd.wall2.Text;

                        roomsData[currentRoomId].floorHeight = (float)rd.floorHeight.Value;
                        roomsData[currentRoomId].floor2Height = (float)rd.floor2Height.Value;
                        roomsData[currentRoomId].wallHeight = (float)rd.wallHeight.Value;
                        roomsData[currentRoomId].wall2Height = (float)rd.wall2Height.Value;

                        SoftUpdateWorld();
                    }
                }
            };

            btnWorldProperties.Click += (o, e) =>
            {
                using (WorldPropertiesEditor wpe = new WorldPropertiesEditor())
                {
                    wpe.CustomPortals = customPortals;
                    wpe.startingRoom.Text = startingRoom.ToString();

                    if (wpe.ShowDialog() == DialogResult.OK)
                    {
                        if (!string.IsNullOrWhiteSpace(wpe.startingRoom.Text))
                            startingRoom = wpe.startingRoom.Text[0];

                        customPortals = wpe.CustomPortals;
                    }
                }
            };

            btnLoadImage.Click += (o, e) =>
            {
                if (openImageDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Workaround for bug in GDI+
                        using (Bitmap image = new Bitmap(openImageDialog.FileName))
                        {
                            SetSize(image.Width, image.Height, false);

                            for (int y = 0; y < image.Height; y++)
                            {
                                for (int x = 0; x < image.Width; x++)
                                {
                                    int pixel = image.GetPixel(x, y).ToArgb();
                                    if (World.imageCodesConverted.ContainsKey(pixel))
                                        currentRoom[y][x] = World.imageCodesConverted[pixel];
                                    else
                                        currentRoom[y][x] = ' ';
                                }
                            }

                            UpdateWorld();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLineFormatted("Cannot read \"{0}\" + (" + ex.GetType().Name + ")", Color.Green, Color.DarkRed, openImageDialog.FileName);
                    }
                }
            };

            rooms = new Dictionary<char, List<List<char>>>();
            currentRoom = new List<List<char>>();

            foreach (KeyValuePair<char, string> pair in World.keys)
            {
                PaletteItem pi = new PaletteItem
                {
                    Parent = palette,
                    Image = World.GetTexture(pair.Value),
                    Text = pair.Key.ToString()
                };
                if (World.labels.ContainsKey(pair.Key))
                    pi.Text += " " + World.labels[pair.Key];
                pi.selectCallbackParam = pair.Key;
                pi.selectCallback = SelectPalette;
            }

            PaletteItem portal = new PaletteItem
            {
                Parent = palette,
                Image = World.GetTexture(World.portal),
                Text = "(Portal)",
                selectCallbackParam = "portal",
                selectCallback = SelectPalette
            };

            UpdateWorld();
        }

        public WorldEditor(string path) : this()
        {
            try
            {
                RoomsFile rf = JsonConvert.DeserializeObject<RoomsFile>(File.ReadAllText(path));

                customPortals = rf.customPortals;
                startingRoom = rf.startingRoom;
                roomsData = new Dictionary<char, Room>(rf.rooms);
                rooms.Clear();

                foreach (KeyValuePair<char, Room> room in roomsData)
                {
                    List<List<char>> roomsTable = new List<List<char>>();

                    foreach (string row in room.Value.layout.Trim('\n').Split('\n').Select(r => r.Trim('\t', '\r')))
                    {
                        roomsTable.Add(row.ToList());
                    }

                    rooms[room.Key] = roomsTable;
                }

                currentRoomId = '\0';
                SelectRoom(startingRoom);
            }
            catch (Exception e)
            {
                Console.WriteLineFormatted("Cannot read \"{0}\" + (" + e.GetType().Name + ")", Color.Green, Color.DarkRed, path);
            }
        }

        private void Save()
        {
            try
            {
                if (saveWorldDialog.ShowDialog() == DialogResult.OK)
                {
                    SelectRoom('\0');

                    RoomsFile roomsFile = new RoomsFile();
                    roomsData = roomsData.Where(d => rooms.ContainsKey(d.Key)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

                    roomsFile.customPortals = customPortals;
                    roomsFile.startingRoom = startingRoom;

                    int maxSize = 0;

                    foreach (KeyValuePair<char, List<List<char>>> room in rooms)
                    {
                        if (!roomsData.ContainsKey(room.Key))
                            roomsData[room.Key] = new Room();

                        // Sanitize the room layout to be uniform width and height
                        int highestColumnCount = 0;
                        room.Value.ForEach(row => highestColumnCount = Math.Max(highestColumnCount, row.Count));
                        room.Value.ForEach(row =>
                        {
                            while (row.Count < highestColumnCount)
                                row.Add(' ');
                        });

                        // Find the biggest room dimension
                        maxSize = Math.Max(maxSize, Math.Max(room.Value.Count, highestColumnCount));

                        roomsData[room.Key].layout = "\n";
                        room.Value.ForEach(row => roomsData[room.Key].layout += string.Join("", row) + "\n");
                        roomsData[room.Key].layout = roomsData[room.Key].layout.TrimEnd('\n', '\r');
                    }

                    roomsFile.roomSize = maxSize;
                    roomsFile.rooms = new SortedDictionary<char, Room>(roomsData);

                    File.WriteAllText(saveWorldDialog.FileName, JsonConvert.SerializeObject(roomsFile, Formatting.Indented).Replace("\\n", "\n"));

                    SelectRoom(startingRoom);
                }
            }
            catch (Exception e)
            {
                Console.WriteLineFormatted("Cannot write \"{0}\" + (" + e.GetType().Name + ")", Color.Green, Color.DarkRed, "file");
            }
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
            if (c != '\0' && !rooms.ContainsKey(c))
            {
                CreateRoom(c);
                return;
            }

            if (currentRoomId != '\0')
                rooms[currentRoomId] = currentRoom;
            currentRoomId = c;

            if (currentRoomId == '\0')
                currentRoom = new List<List<char>>();
            else
            {
                currentRoom = rooms[currentRoomId];
                rooms.Remove(currentRoomId);
            }

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

        private void SetSize(int newX, int newY, bool rebuild = true)
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
            if (rebuild)
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
                pb.Image = World.GetTexture(GetTileTexture(currentRoom[worldTable.GetRow(pb)][worldTable.GetColumn(pb)]));
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

                    PictureBox pb = new PictureBox
                    {
                        Parent = worldTable,
                        Dock = DockStyle.Fill,
                        BackgroundImage = World.GetTexture(World.background),
                        Image = World.GetTexture(GetTileTexture(c)),
                        SizeMode = PictureBoxSizeMode.StretchImage
                    };

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
            if (World.keys.ContainsKey(tile))
                texture = World.keys[tile];
            else if (rooms.ContainsKey(tile) || currentRoomId == tile)
                texture = World.portal;

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
                    case "wall2":
                        Text = roomsData[currentRoomId].wall2;
                        break;
                    case "floor2":
                        tex = roomsData[currentRoomId].floor2;
                        break;
                }

                if (!string.IsNullOrWhiteSpace(tex) && World.tags.ContainsKey(tex))
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
            pb.Image = World.GetTexture(GetTileTexture(selection));
        }

        private void CloseMenu(object sender, System.EventArgs e)
        {
            Close();
        }

        private void PopulateRoomSelector()
        {
            btnSelect.DropDownItems.Clear();

            foreach (char room in rooms.Select(x => x.Key).OrderBy(k => k))
            {
                ToolStripMenuItem i = new ToolStripMenuItem
                {
                    Text = room.ToString()
                };
                i.Click += (o, e) => SelectRoom(room);

                btnSelect.DropDownItems.Add(i);
            }

            btnSelect.Enabled = btnSelect.DropDownItems.Count != 0;
        }
    }
}
