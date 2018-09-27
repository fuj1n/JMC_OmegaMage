using Newtonsoft.Json;
using System.Collections.Generic;
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

    public TextAsset roomsFile; // The rooms JSON file
    public char roomId = '0'; // Current room ID
    public GameObject tilePrefab; // Prefab for all tiles
    public TileTexture[] tileTextures; // A list of named textures for tiles

    private RoomsFile roomsData;

    public GameObject portal;

    //public bool _____________; // ???

    public TypeTemplatePair[] enemyTemplates = { };

    private Tile[,] tiles;
    private Transform tileAnchor;

    private bool firstRoom = true;

    private void Start()
    {
        instance = this;

        // Make a group for all tiles
        GameObject anchor = new GameObject("tileAnchor");
        tileAnchor = anchor.transform;

        // Read the JSON file
        roomsData = JsonConvert.DeserializeObject<RoomsFile>(roomsFile.text);
        roomId = roomsData.startingRoom;

        // Build the room with given ID
        BuildRoom(roomId);
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
    /// Build a room for a room entry
    /// </summary>
    /// <param name="room">The room entry from JSON file</param>
    public void BuildRoom(Room room)
    {
        // Clean up
        foreach (Transform t in tileAnchor)
        {
            Destroy(t.gameObject);
        }

        Mage.instance.transform.position = Vector3.left * 1000;
        Mage.instance.ClearInput();

        // Get the texture names for the floors and walls from <room> attributes
        string floorTexture = room.floor;
        string wallTexture = room.wall;

        // Split the room into rows of tiles based on line feeds in the json file
        string[] roomRows = room.layout.Split('\n');
        for (int i = 0; i < roomRows.Length; i++)
        {
            roomRows[i] = roomRows[i].Trim('\t', '\r');
        }

        // Clear the tiles array
        tiles = new Tile[roomsData.roomSize, roomsData.roomSize];

        float maxY = roomRows.Length - 1;
        List<Portal> portals = new List<Portal>();

        for (int y = 0; y < roomRows.Length; y++)
        {
            for (int x = 0; x < roomRows[y].Length; x++)
            {
                int height = 0;
                string tileTexture = floorTexture;
                char type, rawType;

                // Get the character representing the tile
                type = rawType = roomRows[y][x];
                switch (rawType)
                {
                    case ' ':
                    case '_':
                        continue;
                    case '.': // default floor
                        tileTexture = floorTexture;
                        break;
                    case '|': // default wall
                        height = 1;
                        tileTexture = wallTexture;
                        break;
                    default:
                        // interpret as floor
                        type = '.';
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
                    case 'X': // Mage starting position
                        if (firstRoom)
                        {
                            // Raise the mage cause for some reason the tutorial places them in the ground
                            Mage.instance.transform.position = tile.transform.position + Vector3.back * .6F;
                            roomId = roomsData.rooms.Where(kvp => kvp.Value == room).Single().Key;
                            firstRoom = false;
                        }
                        break;
                    default:
                        if (roomsData.portals.Contains(rawType))
                        {
                            // Create portal
                            GameObject gop = Instantiate(this.portal, tile.position, Quaternion.identity, tileAnchor);
                            Portal portal = gop.GetComponent<Portal>();
                            portal.destinationRoom = rawType;
                            portals.Add(portal);
                        }
                        else
                        {
                            IEnemy enemy = EnemyFactory.CreateEnemy(rawType, enemyTemplates);
                            if (enemy == null)
                                break;

                            enemy.transform.position = tile.position;
                            enemy.transform.SetParent(tileAnchor);
                        }
                        break;

                }
            }
        }

        foreach (Portal portal in portals)
        {
            if (portal.destinationRoom == roomId)
            {
                Mage.instance.StopWalking();
                Mage.instance.transform.position = portal.transform.position + Vector3.back * .1F;
                portal.justArrived = true;
                firstRoom = false;
            }
        }

        roomId = roomsData.rooms.Where(kvp => kvp.Value == room).Single().Key;
    }

    /// <summary>
    /// Grabs <see cref="Room"/> object and calls <see cref="BuildRoom(Room)"/>
    /// </summary>
    /// <param name="roomId">The character identifier of the room</param>
    public void BuildRoom(char roomId)
    {
        if (!roomsData.rooms.ContainsKey(roomId))
        {
            Debug.LogError("Cannot find room with ID " + roomId);
            return;
        }

        BuildRoom(roomsData.rooms[roomId]);
    }
}
