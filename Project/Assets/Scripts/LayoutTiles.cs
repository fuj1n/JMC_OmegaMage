using System.Linq;
using UnityEngine;

/// <summary>
/// This class allows us to define various textures for tiles
/// </summary>
[System.Serializable]
public class TileTexture
{
    public string name;
    public Texture2D texture;
}

public class LayoutTiles : MonoBehaviour
{
    public static LayoutTiles instance;

    public const int ROOM_SIZE = 100;

    public TextAsset roomsFile; // The rooms.xml file
    public int roomNumber = 0; // Current room number, because who stores numbers as strings?
    public GameObject tilePrefab; // Prefab for all tiles
    public TileTexture[] tileTextures; // A list of named textures for tiles

    //public bool _____________; // ???

    private PT_XMLReader roomsXMLR;
    private PT_XMLHashList roomsXML;
    private Tile[,] tiles;
    private Transform tileAnchor;

    private void Awake()
    {
        instance = this;

        // Make a group for all tiles
        GameObject anchor = new GameObject("tileAnchor");
        tileAnchor = anchor.transform;

        // Read the XML file
        roomsXMLR = new PT_XMLReader(); // Create an XML reader
        roomsXMLR.Parse(roomsFile.text); // Parse the rooms XML file
        roomsXML = roomsXMLR.xml["xml"][0]["room"];

        // Build the nth room
        BuildRoom(roomNumber);
    }

    /// <summary>
    /// Get the texture by name
    /// </summary>
    /// <param name="name">The name of the texture</param>
    /// <returns>Requested texture or null</returns>
    public Texture2D GetTileTexture(string name)
    {
        return tileTextures.Where(x => x.name == name).Select(x => x.texture).FirstOrDefault();
    }

    /// <summary>
    /// Build a room for an XML room entry
    /// </summary>
    /// <param name="room">The room entry from XML file</param>
    public void BuildRoom(PT_XMLHashtable room)
    {
        // Get the texture names for the floors and walls from <room> attributes
        string floorTexture = room.att("floor");
        string wallTexture = room.att("wall");

        // Split the room into rows of tiles based on line feeds in the xml file
        string[] roomRows = room.text.Split('\n');
        for (int i = 0; i < roomRows.Length; i++)
        {
            roomRows[i] = roomRows[i].Trim('\t');
        }

        // Clear the tiles array
        tiles = new Tile[ROOM_SIZE, ROOM_SIZE];

        float maxY = roomRows.Length - 1;

        for (int y = 0; y < roomRows.Length; y++)
        {
            for (int x = 0; x < roomRows[y].Length; x++)
            {
                int height = 0;
                string tileTexture = floorTexture;
                string type, rawType;

                // Get the character representing the tile
                type = rawType = roomRows[y][x].ToString();
                switch (rawType)
                {
                    case " ":
                    case "_":
                        continue;
                    case ".": // default floor
                        tileTexture = floorTexture;
                        break;
                    case "|": // default wall
                        height = 1;
                        tileTexture = wallTexture;
                        break;
                    default:
                        // interpret as floor
                        type = ".";
                        break;
                }

                // Create tile
                GameObject go = Instantiate(tilePrefab);
                Tile tile = go.GetComponent<Tile>();
                tile.transform.SetParent(tileAnchor);

                tile.position = new Vector3(x, maxY - y, 0);
                tiles[x, y] = tile;

                // Set the tile attributes
                tile.type = type;
                tile.height = height;
                tile.texture = tileTexture;

                // If the type is still rawType, continue to next iteration
                if (rawType == type) continue;

                switch (rawType)
                {
                    case "X": // Mage starting position
                        Mage.instance.pos = tile.pos;
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Grabs room object and calls <see cref="BuildRoom(PT_XMLHashtable)"/>
    /// </summary>
    /// <param name="roomNumber">The room number</param>
    public void BuildRoom(int roomNumber)
    {
        // Use the hexadecimal tostring converter to defeat tutorial's reason for storing it as string
        string roomId = roomNumber.ToString("X");

        PT_XMLHashtable room = roomsXML.Cast<PT_XMLHashtable>().Where(x => x.att("num") == roomId).FirstOrDefault();

        if (room == default(PT_XMLHashtable))
        {
            Utils.tr("ERROR", "LayoutTiles.BuildRoom(int)", "Room not found: " + roomId);
            return;
        }

        BuildRoom(room);
    }
}
