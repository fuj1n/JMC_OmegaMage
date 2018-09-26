using System.Collections.Generic;
using UnityEngine;

public class TapIndicator : PT_Mover
{
    public float lifeTime = 0.4f;
    public float[] scales;
    public Color[] colors;

    private void Awake()
    {
        scale = Vector3.zero;
    }

    private void Start()
    {
        List<PT_Loc> locs = new List<PT_Loc>();

        Vector3 pos = transform.position;
        pos.z = -0.1f;
        //transform.position = pos;

        for (int i = 0; i < scales.Length; i++)
        {
            PT_Loc loc = new PT_Loc();
            loc.scale = Vector3.one * scales[i];
            loc.pos = pos;
            loc.color = colors[i];
            locs.Add(loc);
        }

        callback = () => Destroy(gameObject);

        PT_StartMove(locs, lifeTime);
    }
}
