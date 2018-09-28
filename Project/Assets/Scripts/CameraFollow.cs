using UnityEngine;

public class CameraFollow : MonoBehaviour
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
        transform.position = target.position + followOffset;
    }

    private void FixedUpdate()
    {
        Vector3 newPos = target.position + followOffset;
        transform.position = Vector3.Lerp(transform.position, newPos, camEasing);
    }
}
