using System.Collections.Generic;
using System.Xml.Linq;

namespace WorldEdit
{
    public static class XMLAdapter
    {
        public static RoomsFile LoadRoomsFile(string xml)
        {
            XElement rooms = XElement.Parse(xml);

            SortedDictionary<char, Room> loadedRooms = new SortedDictionary<char, Room>();

            foreach (XElement room in rooms.Elements("room"))
            {
                Room loadedRoom = new Room();

                XAttribute roomId = room.Attribute("num");
                XAttribute floor = room.Attribute("floor");
                XAttribute wall = room.Attribute("wall");

                if (roomId == null || loadedRooms.ContainsKey(roomId.Value[0]))
                    continue;

                if (floor != null)
                    loadedRoom.floor = floor.Value;
                if (wall != null)
                    loadedRoom.wall = wall.Value;

                loadedRoom.layout = room.Value.Replace('_', ' ').Trim('\n', '\r', '\t');

                loadedRooms[roomId.Value[0]] = loadedRoom;
            }

            return new RoomsFile() { rooms = loadedRooms };
        }

        public static string WriteRoomsFile(RoomsFile roomsFile)
        {
            SortedDictionary<char, Room> loadedRooms = roomsFile.rooms;

            XElement global = new XElement("xml");

            foreach (KeyValuePair<char, Room> loadedRoom in loadedRooms)
            {
                XElement room = new XElement("room");

                room.SetAttributeValue("num", loadedRoom.Key.ToString());
                room.SetAttributeValue("floor", loadedRoom.Value.floor);
                room.SetAttributeValue("wall", loadedRoom.Value.wall);

                room.Value = loadedRoom.Value.layout.Replace("\n", "\n\t") + "\n";

                global.Add(room);
            }

            return global.ToString();
        }
    }
}
