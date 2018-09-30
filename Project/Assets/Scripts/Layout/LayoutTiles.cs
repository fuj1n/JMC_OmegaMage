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
    public Texture2D[] ditherTex;
    public float ditherChance = 0.1F;

    public Material customMaterial;
}

public class LayoutTiles : MonoBehaviour
{
    public const int KEY_COUNT = 5;

    public static LayoutTiles instance;

    public TextAsset roomsFile; // The rooms JSON file
    public char roomId; // Current room ID
    public GameObject tilePrefab; // Prefab for all tiles
    public TileTexture[] tileTextures; // A list of named textures for tiles

    public RoomsFile roomsData;

    public GameObject portal;
    public TypeTemplatePair[] customPortals = { };

    public TypeTemplatePair[] entityTemplates = { };

    private Tile[,] tiles;
    private Transform tileAnchor;

    private bool firstRoom = true;

    private void Awake()
    {
        instance = this;

        // Make a group for all tiles
        GameObject anchor = new GameObject("tileAnchor");
        tileAnchor = anchor.transform;

        // Read the JSON file
        roomsData = JsonConvert.DeserializeObject<RoomsFile>(roomsFile.text);
        roomId = roomsData.startingRoom;
    }

    private void Start()
    {
        EffectDoor.instance.worldAnchor = tileAnchor.gameObject;

        // Build the room with given ID
        BuildRoom(roomId);
    }

    /// <summary>
    /// Get the texture by name
    /// </summary>
    /// <param name="name">The name of the texture</param>
    /// <returns>Requested texture or null</returns>
    public Texture2D GetTileTexture(string name, bool dither)
    {
        TileTexture tt = tileTextures.Where(x => x.name == name).FirstOrDefault();
        if (tt == null)
            return null;

        Texture2D texture = tt.texture;

        if (dither && tt.ditherTex != null && tt.ditherTex.Length > 0 && Random.value <= tt.ditherChance)
        {
            texture = tt.ditherTex[Random.Range(0, tt.ditherTex.Length)];
        }

        return texture;
    }

    /// <summary>
    /// Gets the material by name if the tile has a custom material
    /// </summary>
    /// <param name="name">The name of the texture</param>
    /// <returns>Requested material or null</returns>
    public Material GetTileCustomMaterial(string name)
    {
        TileTexture tt = tileTextures.Where(x => x.name == name).FirstOrDefault();
        if (tt == null)
            return null;

        return tt.customMaterial;
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

        Mage.instance.transform.position = Vector3.left * 5;
        Mage.instance.ClearInput();
        Mage.instance.MapChange();

        // Get the texture names for the floors and walls from <room> attributes
        string floorTexture = room.floor;
        string floor2Texture = room.floor2;
        string wallTexture = room.wall;
        string wall2Texture = room.wall2;

        float floorHeight = room.floorHeight;
        float floor2Height = room.floor2Height;
        float wallHeight = room.wallHeight;
        float wall2Height = room.wall2Height;

        // Split the room into rows of tiles based on line feeds in the json file
        string[] roomRows = room.layout.Trim('\n').Split('\n');
        for (int i = 0; i < roomRows.Length; i++)
        {
            roomRows[i] = roomRows[i].Trim('\t', '\r');
        }

        // Clear the tiles array
        tiles = new Tile[roomsData.roomSize, roomsData.roomSize];

        float maxY = roomRows.Length - 1;
        List<Portal> portals = new List<Portal>();
        HashSet<int> entityIds = new HashSet<int>();
        System.Random predictable = new System.Random(25565 + room.GetRoomId(roomsData));

        for (int y = 0; y < roomRows.Length; y++)
        {
            for (int x = 0; x < roomRows[y].Length; x++)
            {
                float height = 0;
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
                        height = floorHeight;
                        tileTexture = floorTexture;
                        break;
                    case '+': // second floor
                        height = floor2Height;
                        tileTexture = floor2Texture;
                        break;
                    case '-': // Invisible wall
                        height = 1F;
                        tileTexture = "invisible";
                        break;
                    case '|': // default wall
                        height = wallHeight;
                        tileTexture = wallTexture;
                        break;
                    case ':': // second wall
                        height = wall2Height;
                        tileTexture = wall2Texture;
                        break;
                    default:
                        // interpret as floor
                        height = floorHeight;
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
                            roomId = room.GetRoomId(roomsData);
                            firstRoom = false;
                        }
                        break;
                    default:
                        if (roomsData.rooms.ContainsKey(rawType))
                        {
                            GameObject portalTemplate = this.portal;
                            if (roomsData.customPortals.ContainsKey(rawType))
                            {
                                GameObject customPortal = customPortals.Where(p => p.type == roomsData.customPortals[rawType]).Select(p => p.template).SingleOrDefault();
                                if (!customPortal)
                                    Debug.LogError("Requested custom portal " + roomsData.customPortals[rawType] + " cannot be found.");
                                else
                                    portalTemplate = customPortal;
                            }

                            // Create portal
                            GameObject gop = Instantiate(portalTemplate, tile.position, this.portal.transform.rotation, tileAnchor);
                            Portal portal = gop.GetComponent<Portal>();
                            if (portal)
                            {
                                portal.destinationRoom = rawType;
                                portals.Add(portal);
                            }
                        }
                        else
                        {
                            int id;
                            do
                            {
                                id = predictable.Next(int.MinValue, int.MaxValue);
                            } while (entityIds.Contains(id));

                            IEntity entity = EntityFactory.CreateEntity(rawType, entityTemplates, id);
                            if (entity == null)
                                break;

                            entity.transform.position = tile.position;
                            entity.transform.SetParent(tileAnchor);
                        }
                        break;
                }
            }
        }

        //portals.Where(portal => portal.destinationRoom == roomId).ToList().ForEach(portal =>
        //{
        //    Mage.instance.StopWalking();
        //    Mage.instance.transform.position = portal.transform.position + Vector3.back * .1F;
        //    portal.justArrived = true;
        //    firstRoom = false;
        //});

        if (!firstRoom)
            foreach (Portal portal in portals)
            {
                if (portal.destinationRoom == roomId)
                {
                    Mage.instance.StopWalking();
                    Mage.instance.transform.position = portal.transform.position + Vector3.back * .1F;
                    portal.justArrived = true;
                }
            }

        firstRoom = false;
        roomId = room.GetRoomId(roomsData);
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
