using UnityEngine;

public class Tile : MonoBehaviour
{
    public char type;

    public string startTexture = "";
    public bool ditherTexture = true;

    private string texture_val;
    private int height_val = 0;
    private Vector3 position_val;

    private new Renderer renderer;
    private Material defaultMaterial;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        defaultMaterial = renderer.material;
    }

    private void Start()
    {
        // Done on first update so that we are 
        if (!string.IsNullOrWhiteSpace(startTexture))
            texture = startTexture;
    }

    /// <summary>
    /// Height moves the tile up or down, walls have height of 1
    /// </summary>
    public int height
    {
        get
        {
            return height_val;
        }
        set
        {
            height_val = value;
            AdjustHeight();
        }
    }

    /// <summary>
    /// Sets the texture of the tile based on a string
    /// </summary>
    public string texture
    {
        get
        {
            return texture_val;
        }
        set
        {
            texture_val = value;
            name = "TilePrefab_" + texture_val; // Sets teh name of this gameobject

            Material customMaterial = LayoutTiles.instance.GetTileCustomMaterial(texture_val);
            renderer.material = customMaterial ? customMaterial : defaultMaterial;

            Texture2D t2D = LayoutTiles.instance.GetTileTexture(texture_val, ditherTexture);
            if (!t2D && !customMaterial)
                Utils.tr("ERROR", "Tile.type{set}=", value, "No matching Texture2D in LayoutTiles.tileTextures!");
            else if (t2D)
            {
                renderer.material.mainTexture = t2D;
            }
        }
    }

    public Vector3 position
    {
        get
        {
            return position_val;
        }
        set
        {
            position_val = value;
            AdjustHeight();
        }
    }

    public void AdjustHeight()
    {
        // Moves the block up or down based on the height
        Vector3 vectorOffset = Vector3.back * (height - 0.5F);

        // The -0.5f shifts the Tile down 0.5 units so that it's top surface is at
        // z = 0 when pos.z = 0 and height = 0
        transform.position = position + vectorOffset;
    }
}
