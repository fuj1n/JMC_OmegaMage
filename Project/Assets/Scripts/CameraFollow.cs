using UnityEngine;

public class CameraFollow : PT_MonoBehaviour
{
    public static CameraFollow instance;

    public Transform target;
    public float camEasing = 0.1F;
    public Vector3 followOffset = new Vector3(0, 0, -2);

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // Set starting position so we don't ease at the very start
        pos = target.position + followOffset;
    }

    private void FixedUpdate()
    {
        Vector3 newPos = target.position + followOffset;
        pos = Vector3.Lerp(pos, newPos, camEasing);
    }
}
