using System.Collections.Generic;
using System.Linq;

public class RoomsFile
{
    public int roomSize = 100;
    public char startingRoom = '0';
    public char[] portals = Enumerable.Range(0, 16).Select(x => x.ToString("X")[0]).ToArray();
    public Dictionary<char, string> customPortals = new Dictionary<char, string>() { { 'F', "boss" } };
    public Dictionary<char, Room> rooms = new Dictionary<char, Room>();
}
