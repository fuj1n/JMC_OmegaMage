using UnityEngine;

public class Tile : MonoBehaviour
{
    public char type;

    private string texture_val;
    private int height_val = 0;
    private Vector3 position_val;

    private new Renderer renderer;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
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
            Texture2D t2D = LayoutTiles.instance.GetTileTexture(texture_val);
            if (t2D == null)
            {
                Utils.tr("ERROR", "Tile.type{set}=", value, "No matching Texture2D in LayoutTiles.S.tileTextures!");
            }
            else
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
