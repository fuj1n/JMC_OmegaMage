using System.Collections.Generic;

public class RoomsFile
{
    public int roomSize = 100;
    public char startingRoom = '0';
    public Dictionary<char, string> customPortals = new Dictionary<char, string>();
    public SortedDictionary<char, Room> rooms = new SortedDictionary<char, Room>();
}
