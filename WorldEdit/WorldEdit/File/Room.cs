using System.Linq;

public class Room
{
    public string floor;
    public string wall;
    public string layout;

    public char GetRoomId(RoomsFile rooms)
    {
        return rooms.rooms.Where(kvp => kvp.Value == this).Single().Key;
    }
}
