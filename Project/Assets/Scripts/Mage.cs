using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Mage : MonoBehaviour
{
    public static Mage instance;
    public const bool DEBUG = true;

    /// <summary>
    /// Time it takes to register a tap
    /// </summary>
    public float mouseTapTime = 0.1F;

    /// <summary>
    /// Distance in pixels for mouse drag
    /// </summary>
    public float mouseDragDistance = 5;

    public GameObject tapIndicator;

    /// <summary>
    /// How much of the screen should be used
    /// </summary>
    public float activeScreenWidth = 1;

    /// <summary>
    /// The movement speed of the mage
    /// </summary>
    public float speed = 2F;

    [Header("Elements")]
    public GameObject[] elementPrefabs;
    public float elementRotationDistance = 0.5F;
    public float elementRotationSpeed = 0.5F;
    public int maxNumSelectedElements = 1;

    // public bool _______________; // ???

    private MousePhase mousePhase = MousePhase.IDLE;
    private List<MouseInfo> mouseInfoBuffer = new List<MouseInfo>();

    private List<Element> selectedElements = new List<Element>();

    public MouseInfo lastMouseInfo
    {
        get
        {
            if (mouseInfoBuffer.Count == 0) return null;
            return mouseInfoBuffer[mouseInfoBuffer.Count - 1];
        }
    }

    private bool walking = false;
    private Vector3 walkTarget;

    private Transform character;
    private new Rigidbody rigidbody;

    private void Awake()
    {
        instance = this;
        mousePhase = MousePhase.IDLE;

        character = transform.Find("CharacterTrans");
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Input handling
        bool b0Down = Input.GetMouseButtonDown(0);
        bool b0Up = Input.GetMouseButtonUp(0);

        bool inActiveArea = Input.mousePosition.x / Screen.width < activeScreenWidth;

        switch (mousePhase)
        {
            case MousePhase.IDLE:
                if (inActiveArea && b0Down)
                {
                    mouseInfoBuffer.Clear();
                    AddMouseInfo();

                    if (mouseInfoBuffer[0].hit)
                    {
                        MouseDown();
                        mousePhase = MousePhase.DOWN;
                    }
                }
                break;
            case MousePhase.DOWN:
                MouseInfo lastMouseInfo = AddMouseInfo();
                if (b0Up)
                {
                    MouseTap();
                    mousePhase = MousePhase.IDLE;
                }
                else if (Time.time - mouseInfoBuffer[0].time > mouseTapTime)
                {
                    float dragDist = (lastMouseInfo.screenPos - mouseInfoBuffer[0].screenPos).magnitude;
                    if (dragDist >= mouseDragDistance || selectedElements.Count == 0)
                        mousePhase = MousePhase.DRAG;
                }
                break;
            case MousePhase.DRAG:
                AddMouseInfo();
                if (b0Up)
                {
                    MouseDragUp();
                    mousePhase = MousePhase.IDLE;
                }
                else
                {
                    MouseDrag();
                }
                break;
        }

        OrbitSelectedElements();
    }

    private void FixedUpdate()
    {
        if (walking)
        {
            rigidbody.velocity = (walkTarget - transform.position).normalized * speed;

            // If we're very close to the target, stop moving
            if ((walkTarget - transform.position).magnitude < speed * Time.fixedDeltaTime)
            {
                transform.position = walkTarget;
                StopWalking();
            }
        }
        else
        {
            rigidbody.velocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Tile tile = collision.gameObject.GetComponent<Tile>();

        if (tile && tile.height > 0F)
        {
            StopWalking();
        }
    }

    private MouseInfo AddMouseInfo()
    {
        MouseInfo info = new MouseInfo();
        info.screenPos = Input.mousePosition;
        info.position = Utils.mouseLoc;
        info.ray = Utils.mouseRay;

        info.time = Time.time;
        info.Raycast();


        if (mouseInfoBuffer.Count == 0 || info.time != mouseInfoBuffer[mouseInfoBuffer.Count - 1].time)
            mouseInfoBuffer.Add(info);

        return info;
    }

    private void MouseDown()
    {
        this.ConditionalLog(DEBUG, "Mage.MouseDown()");
    }

    private void MouseTap()
    {
        this.ConditionalLog(DEBUG, "Mage.MouseTap()");

        WalkTo(lastMouseInfo.position);
        ShowTap(lastMouseInfo.position);
    }

    private void MouseDrag()
    {
        this.ConditionalLog(DEBUG, "Mage.MouseDrag()");

        WalkTo(lastMouseInfo.position);
    }

    private void MouseDragUp()
    {
        this.ConditionalLog(DEBUG, "Mage.MouseDragUp()");

        StopWalking();
    }

    private void OrbitSelectedElements()
    {
        if (selectedElements.Count == 0)
            return;

        float tau = Mathf.PI * 2;
        float rotPerElement = tau / selectedElements.Count;
        float theta0 = elementRotationSpeed * Time.time * tau;

        for (int i = 0; i < selectedElements.Count; i++)
        {
            // Determine rotation angle
            float theta = theta0 + i * rotPerElement;
            Element element = selectedElements[i];
            Vector3 vec = new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), 0) * elementRotationDistance;
            vec.z = -0.5F;
            element.transform.localPosition = vec;
        }
    }

    /// <summary>
    /// Tells the mage to walk towards and <see cref="Face(Vector3)"/> <paramref name="target"/>
    /// </summary>
    public void WalkTo(Vector3 target)
    {
        walkTarget = target;
        walkTarget.z = 0;
        walking = true;
        Face(walkTarget);
    }

    /// <summary>
    /// Faces the mage towards <paramref name="pos"/>
    /// </summary>
    public void Face(Vector3 pos)
    {
        Vector3 delta = pos - transform.position;
        // Get rotation around Z that points X axis towards pos
        float rot = Mathf.Rad2Deg * Mathf.Atan2(delta.y, delta.x);
        character.rotation = Quaternion.Euler(0, 0, rot);
    }

    /// <summary>
    /// Tells the mage to stop walking
    /// </summary>
    public void StopWalking()
    {
        walking = false;
        rigidbody.velocity = Vector3.zero;
    }

    /// <summary>
    /// Spawns the tap effect at the specificed position
    /// </summary>
    /// <param name="pos"></param>
    public void ShowTap(Vector3 pos)
    {
        Instantiate(tapIndicator, pos, Quaternion.identity);
    }

    /// <summary>
    /// Adds an <paramref name="element"/> if less than <see cref="maxNumSelectedElements"/> is selected
    /// </summary>
    public void SelectElement(ElementType element)
    {
        this.ConditionalLog(DEBUG, "Select element: {0}", element);

        if (selectedElements.Select(e => e.type).Contains(element))
            return;

        if (element == ElementType.NONE)
        {
            ClearElements();
            return;
        }

        if (maxNumSelectedElements == 1)
            ClearElements();

        if (selectedElements.Count >= maxNumSelectedElements)
            return;

        GameObject go = Instantiate(elementPrefabs[(int)element], transform);
        Element el = go.GetComponent<Element>();

        selectedElements.Add(el);
    }

    /// <summary>
    /// Clears the selected elements and destroys all orbiters
    /// </summary>
    public void ClearElements()
    {
        selectedElements.ForEach(o => Destroy(o.gameObject));
        selectedElements.Clear();
    }
}

/// <summary>
/// Used to track mouse interaction phase
/// </summary>
public enum MousePhase
{
    IDLE,
    DOWN,
    DRAG
}

public enum ElementType
{
    EARTH,
    WATER,
    AIR,
    FIRE,
    AETHER,
    NONE
}

/// <summary>
/// Stores information about the mouse in each frame of interaction
/// </summary>
[System.Serializable]
public class MouseInfo
{
    public Vector3 position;
    public Vector3 screenPos;
    public Ray ray;
    public float time;

    public RaycastHit hitInfo;
    public bool hit;

    /// <returns>True if the mouse ray hits anything</returns>
    public RaycastHit Raycast()
    {
        hit = Physics.Raycast(ray, out hitInfo);
        return hitInfo;
    }

    /// <param name="mask">The layer mask for this raycast</param>
    /// <returns>True if the mouse ray hits anything</returns>
    public RaycastHit Raycast(int mask)
    {
        hit = Physics.Raycast(ray, out hitInfo, mask);
        return hitInfo;
    }
}
