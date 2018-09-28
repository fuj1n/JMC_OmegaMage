using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mage : MonoBehaviour
{
    public static Mage instance;
    public const bool DEBUG = true;

    [Header("Input")]
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

    public Color[] elementColors;

    public float lineMinDelta = 0.1F;
    public float lineMaxDelta = 0.5F;
    public float lineMaxLength = 8F;

    public GameObject fireGroundSpell;

    [Header("Health")]
    public float maxHealth = 4F;
    public float knockbackDistance = 1F;
    public float knockbackDuration = 0.5F;
    public float invincibilityDuration = 0.5F;

    public int invincibilityBlinkCount = 3;

    private float health = 1F;

    protected Transform spellAnchor;

    private float totalLineLength;
    private List<Vector3> linePoints = new List<Vector3>();
    protected LineRenderer lineRender;
    protected float lineZ = -0.1F; // Z depth

    private MousePhase mousePhase = MousePhase.IDLE;
    private List<MouseInfo> mouseInfoBuffer = new List<MouseInfo>();

    private string actionStartTag; // Mage, Ground or Enemy
    private List<Element> selectedElements = new List<Element>();

    private bool walking = false;
    private Vector3 walkTarget;

    private Transform character;
    private new Rigidbody rigidbody;
    private Material coreMaterial;

    private Sequence invincibilitySequence;

    private Vector3 knockbackDirection;
    private float knockbackTime;

    private void Awake()
    {
        instance = this;
        mousePhase = MousePhase.IDLE;

        character = transform.Find("CharacterTrans");
        rigidbody = GetComponent<Rigidbody>();

        lineRender = GetComponentInChildren<LineRenderer>();
        lineRender.enabled = false;

        spellAnchor = new GameObject("Spell Anchor").transform;

        Transform view = character.Find("View_Character");
        coreMaterial = view.GetChild(0).GetComponent<Renderer>().material;
        foreach (Transform t in view)
        {
            t.GetComponent<Renderer>().material = coreMaterial;
        }
        coreMaterial.SetInt("_ZWrite", 1); // Enable ZWrite so our material doesn't act weird with transparency on

        invincibilitySequence = DOTween.Sequence();
        for (int i = 0; i < invincibilityBlinkCount; i++)
        {
            invincibilitySequence.Append(coreMaterial.DOFade(0F, invincibilityDuration / invincibilityBlinkCount / 2));
            invincibilitySequence.Append(coreMaterial.DOFade(1F, invincibilityDuration / invincibilityBlinkCount / 2));
        }
        invincibilitySequence.Pause().SetAutoKill(false);
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

                    if (mouseInfoBuffer.FirstOrDefault().hit)
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
                else if (Time.time - mouseInfoBuffer.FirstOrDefault().time > mouseTapTime)
                {
                    float dragDist = (lastMouseInfo.screenPos - mouseInfoBuffer.FirstOrDefault().screenPos).magnitude;
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
        if (knockbackTime > 0F)
        {
            rigidbody.velocity = knockbackDirection * (knockbackDistance / knockbackDuration);
            knockbackTime -= Time.fixedDeltaTime;

            return;
        }

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
        GameObject source = collision.gameObject;

        Tile tile = source.GetComponent<Tile>();

        if (tile && tile.height > 0F)
        {
            StopWalking();
        }

        IEnemy enemy = source.GetComponent<IEnemy>();

        if (enemy != null)
            CollisionDamage(enemy);
    }

    private void OnTriggerEnter(Collider other)
    {
        IEnemy enemy = other.GetComponent<IEnemy>();
        if (enemy != null)
            CollisionDamage(enemy);
    }

    private void CollisionDamage(IEnemy enemy)
    {
        if (invincibilitySequence.IsPlaying())
            return;

        StopWalking();
        ClearInput();

        health -= enemy.GetDamage() / maxHealth;
        if (health <= 0F)
        {
            Die(); // 死ね
            return;
        }

        knockbackTime = knockbackDuration;
        knockbackDirection = (transform.position - enemy.transform.position).normalized;

        invincibilitySequence.Restart();
    }

    private void Die()
    {
        DOTween.KillAll();
        SceneManager.LoadScene(0);
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

        GameObject clicked = mouseInfoBuffer.FirstOrDefault().hitInfo.collider.gameObject;
        GameObject taggedParent = Utils.FindTaggedParent(clicked);
        if (!taggedParent)
            actionStartTag = "";
        else
            actionStartTag = taggedParent.tag;
    }

    private void MouseTap()
    {
        this.ConditionalLog(DEBUG, "Mage.MouseTap()");

        switch (actionStartTag)
        {
            case "Mage":
                break;
            case "Ground":
                WalkTo(mouseInfoBuffer.LastOrDefault().position);
                ShowTap(mouseInfoBuffer.LastOrDefault().position);
                break;
        }
    }

    private void MouseDrag()
    {
        this.ConditionalLog(DEBUG, "Mage.MouseDrag()");

        if (actionStartTag != "Ground")
            return;

        if (selectedElements.Count == 0)
            WalkTo(mouseInfoBuffer.LastOrDefault().position);
        else
        {
            AddPointToLineRender(mouseInfoBuffer.LastOrDefault().position);
        }
    }

    private void MouseDragUp()
    {
        this.ConditionalLog(DEBUG, "Mage.MouseDragUp()");

        if (actionStartTag != "Ground") return;

        if (selectedElements.Count == 0)
            StopWalking();
        else
        {
            CastGroundSpell();
            ClearLineRender();
        }
    }

    private void CastGroundSpell()
    {
        if (selectedElements.Count == 0)
            return;

        switch (selectedElements.First().type)
        {
            case ElementType.FIRE:
                foreach (Vector3 point in linePoints)
                {
                    Instantiate(fireGroundSpell, point, Quaternion.identity, spellAnchor);
                }
                break;
                //TODO: elements
        }

        ClearElements();
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

    #region Line Render
    private void AddPointToLineRender(Vector3 point)
    {
        point.z = lineZ;

        if (linePoints.Count == 0)
        {
            linePoints.Add(point);
            totalLineLength = 0F;
            return;
        }

        if (totalLineLength > lineMaxLength)
            return;

        Vector3 pt0 = linePoints.Last();
        Vector3 direction = point - pt0;
        float delta = direction.magnitude;
        direction.Normalize();

        if (delta < lineMinDelta)
            return;

        totalLineLength += delta;

        if (delta > lineMaxDelta)
        {
            int numToAdd = Mathf.CeilToInt(delta / lineMaxDelta);
            float midDelta = delta / numToAdd;

            for (int i = 1; i < numToAdd; i++)
            {
                Vector3 pointMid = pt0 + (direction * midDelta * i);
                linePoints.Add(pointMid);
            }
        }

        linePoints.Add(point);
        UpdateLineRender();
    }

    private void UpdateLineRender()
    {
        int element = (int)selectedElements[0].type;

        lineRender.startColor = elementColors[element];
        lineRender.endColor = lineRender.startColor;

        lineRender.positionCount = linePoints.Count;
        lineRender.SetPositions(linePoints.ToArray());

        lineRender.enabled = true;
    }

    private void ClearLineRender()
    {
        lineRender.enabled = false;
        linePoints.Clear();
    }
    #endregion

    public void ClearInput()
    {
        mousePhase = MousePhase.IDLE;
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
