using System.Linq;

public class Room
{
    public string floor = "dirt";
    public string floor2 = "tile";
    public string wall = "stone";
    public string wall2 = "wood";

    public float floorHeight = 0F;
    public float floor2Height = 0F;
    public float wallHeight = 1F;
    public float wall2Height = 1F;

    public string layout = "";

    public char GetRoomId(RoomsFile rooms)
    {
        return rooms.rooms.Where(kvp => kvp.Value == this).Single().Key;
    }
}
