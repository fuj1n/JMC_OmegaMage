using DG.Tweening;
using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mage : MonoBehaviour
{
    public static Mage instance;
    public const bool DEBUG = false;

    public float masterVolume = 0.5F;

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
    public bool unlockAll = false;

    public GameObject[] elementPrefabs;
    public float elementRotationDistance = 0.5F;
    public float elementRotationSpeed = 0.5F;
    public int maxNumSelectedElements = 1;

    public Color[] elementColors = { };
    public int[] elementRecoverPerSecond = { };

    public float lineMinDelta = 0.1F;
    public float lineMaxDelta = 0.5F;
    public float lineMaxLength = 8F;

    public GameObject[] spells = { };
    private Dictionary<ElementType, Dictionary<SpellTargetType, ISpell>> spellCache = new Dictionary<ElementType, Dictionary<SpellTargetType, ISpell>>();

    [Header("Health")]
    public float maxHealth = 4F;
    public float knockbackDistance = 1F;
    public float knockbackDuration = 0.5F;
    public float invincibilityDuration = 0.5F;

    public int invincibilityBlinkCount = 3;

    private float health = 1F;

    [HideInInspector]
    public Transform spellAnchor;

    private float totalLineLength;
    private List<Vector3> linePoints = new List<Vector3>();
    protected LineRenderer lineRender;
    protected float lineZ = -0.1F; // Z depth

    private MousePhase mousePhase = MousePhase.IDLE;
    private List<MouseInfo> mouseInfoBuffer = new List<MouseInfo>();

    private string actionStartTag; // Mage, Ground or Enemy
    private GameObject actionStartObject;
    private List<Element> selectedElements = new List<Element>();

    private bool walking = false;
    private Vector3 walkTarget;

    private Transform character;
    private new Rigidbody rigidbody;
    private Material coreMaterial;

    private Sequence invincibilitySequence;

    private Vector3 knockbackDirection;
    private float knockbackTime;

    private HashSet<ElementType> unlockedElements = new HashSet<ElementType>() { ElementType.NONE };
    private Dictionary<ElementType, int> elementCharge = new Dictionary<ElementType, int>();
    private Dictionary<ElementType, int> elementMaxCharge = new Dictionary<ElementType, int>();

    private Transform isInvincibleController;
    private Transform thornsController;
    private float thornsMultiplier;
    private ElementType thornsType;

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

        // Unlocks all the elements
        if (unlockAll)
            System.Enum.GetValues(typeof(ElementType)).Cast<ElementType>().ToList().ForEach(e => UnlockElement(e));

        // Caches all the spells for quick access without the need to look them up every time
        foreach (ISpell spell in spells.Select(s => s.GetComponent<ISpell>()))
        {
            ElementType element = spell.GetElement();
            if (!spellCache.ContainsKey(element))
                spellCache.Add(element, new Dictionary<SpellTargetType, ISpell>());

            spellCache[element][spell.GetTargetType()] = spell;
        }
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

        foreach (ElementType element in System.Enum.GetValues(typeof(ElementType)))
        {
            RecoverElementCharge(element, Mathf.RoundToInt(elementRecoverPerSecond[(int)element] * Time.deltaTime));
        }
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

        RaycastHit[] hits = Physics.BoxCastAll(transform.position, Vector3.one * .25F, Vector3.back);
        float highest = 0F;

        foreach (RaycastHit hit in hits)
        {
            Tile t = hit.collider.GetComponent<Tile>();

            if (t && t.height <= .25F)
            {
                highest = Mathf.Max(highest, t.height);
            }
        }

        Vector3 pos = transform.position;
        pos.z = -(highest + .1F);
        transform.position = pos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject source = collision.gameObject;

        Tile tile = source.GetComponent<Tile>();

        if (tile && tile.height > .25F)
            StopWalking();
    }

    private void OnCollisionStay(Collision collision)
    {
        OnTriggerStay(collision.collider);
    }

    private void OnTriggerStay(Collider other)
    {
        IEnemy enemy = other.GetComponent<IEnemy>();
        if (enemy != null)
            CollisionDamage(enemy);
    }

    #region Health
    private void CollisionDamage(IEnemy enemy)
    {
        if (isInvincibleController || invincibilitySequence.IsPlaying())
            return;

        StopWalking();
        ClearInput();

        knockbackTime = knockbackDuration;
        knockbackDirection = (transform.position - enemy.transform.position).normalized;

        float damage = enemy.GetDamage();
        if (thornsController)
        {
            enemy.TakeDamage(damage * thornsMultiplier, thornsType, false);
            enemy.SetKnockback(knockbackDirection * -1, knockbackDistance * thornsMultiplier, knockbackDuration);
            damage -= damage * thornsMultiplier;
        }

        health -= damage / maxHealth;


        if (health <= 0F)
        {
            Die(); // 死ね
            return;
        }

        invincibilitySequence.Restart();
    }

    public void Heal(float amount)
    {
        health = Mathf.Clamp01(health + amount / maxHealth);
    }

    public float GetHealth()
    {
        return health;
    }

    public void SetInvincible(Transform controller)
    {
        isInvincibleController = controller;
    }

    public void SetThorns(float multiplier, ElementType type, Transform controller)
    {
        thornsMultiplier = multiplier;
        thornsType = type;
        thornsController = controller;
    }

    private void Die()
    {
        DOTween.KillAll();
        SceneManager.LoadScene("GameOver");
    }
    #endregion

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
        {
            actionStartObject = taggedParent;
            actionStartTag = taggedParent.tag;
        }
    }

    private void MouseTap()
    {
        this.ConditionalLog(DEBUG, "Mage.MouseTap()");

        switch (actionStartTag)
        {
            case "Mage":
                if (GetSelectedElement() != ElementType.NONE)
                {
                    CastSpell(SpellTargetType.SELF, SpellSelfParams.self);
                    break;
                }
                goto case "Ground";
            case "Ground":
                WalkTo(mouseInfoBuffer.LastOrDefault().position);
                ShowTap(mouseInfoBuffer.LastOrDefault().position);
                break;
            case "Enemy":
                CastSpell(SpellTargetType.ENEMY, new SpellTargetParams() { source = transform.position, destination = actionStartObject.transform });
                break;
        }
    }

    private void MouseDrag()
    {
        this.ConditionalLog(DEBUG, "Mage.MouseDrag()");

        if (actionStartTag != "Ground" && GetSelectedElement() != ElementType.NONE)
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
            Vector3[] positions = new Vector3[lineRender.positionCount];
            lineRender.GetPositions(positions);
            CastSpell(SpellTargetType.GROUND, new SpellGroundParams() { positions = positions });
            ClearLineRender();
        }
    }

    [NotNull]
    public Dictionary<SpellTargetType, ISpell> FindSpells(ElementType element)
    {
        if (!spellCache.ContainsKey(element))
            return new Dictionary<SpellTargetType, ISpell>();

        return spellCache[element];
    }

    public ISpell FindSpell(ElementType element, SpellTargetType type)
    {
        if (!spellCache.ContainsKey(element) || !spellCache[element].ContainsKey(type))
            return null;

        return spellCache[element][type];
    }

    private void CastSpell(SpellTargetType type, ISpellParams parameters)
    {
        ElementType element = GetSelectedElement();

        ISpell spell = FindSpell(element, type);

        if (spell != null && GetElementCharge(element) >= spell.GetCost())
        {
            if (spell.Cast(parameters))
            {
                elementCharge[element] = GetElementCharge(element) - spell.GetCost();
                ClearElements();
            }
        }
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
        int element = (int)GetSelectedElement();

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

    #region Elements
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

        if (!IsUnlockedElement(element) || !FindSpells(element).Any(s => GetElementCharge(element) >= s.Value.GetCost()))
            return;

        if (maxNumSelectedElements == 1)
            ClearElements();

        if (selectedElements.Count >= maxNumSelectedElements)
            return;

        GameObject go = Instantiate(elementPrefabs[(int)element], transform);
        Element el = go.GetComponent<Element>();

        selectedElements.Add(el);
    }

    /// <summary>
    /// Gets the selected element
    /// </summary>
    public ElementType GetSelectedElement()
    {
        if (selectedElements.Count == 0)
            return ElementType.NONE;

        return selectedElements.Single().type;
    }

    /// <summary>
    /// Clears the selected elements and destroys all orbiters
    /// </summary>
    public void ClearElements()
    {
        selectedElements.ForEach(o => Destroy(o.gameObject));
        selectedElements.Clear();
    }

    /// <summary>
    /// Gets whether the given <paramref name="element"/> is unlocked
    /// </summary>
    /// <returns>True if the element is unlocked</returns>
    public bool IsUnlockedElement(ElementType element)
    {
        return unlockedElements.Contains(element);
    }

    /// <summary>
    /// Unlocks the given <paramref name="element"/>
    /// </summary>
    public void UnlockElement(ElementType element)
    {
        unlockedElements.Add(element);
        elementCharge[element] = GetElementMaxCharge(element);
    }

    /// <summary>
    /// Gets the charge of the given <paramref name="element"/>
    /// </summary>
    /// <returns>The charge percentage of the given element</returns>
    public float GetElementChargeAsPercent(ElementType element)
    {
        return 1F * GetElementCharge(element) / GetElementMaxCharge(element);
    }

    /// <summary>
    /// Gets the charge of the given <paramref name="element"/>
    /// </summary>
    /// <returns>The charge value of the given element</returns>
    public int GetElementCharge(ElementType element)
    {
        if (!elementCharge.ContainsKey(element))
            elementCharge[element] = GetElementMaxCharge(element);

        return elementCharge[element];
    }

    /// <summary>
    /// Gets the maximum chage of the given <paramref name="element"/>
    /// </summary>
    /// <returns>The maximum chage</returns>
    public int GetElementMaxCharge(ElementType element)
    {
        if (!elementMaxCharge.ContainsKey(element))
            elementMaxCharge[element] = 1000000;

        return elementMaxCharge[element];
    }

    /// <summary>
    /// Recover <paramref name="charge"/> for the given <paramref name="element"/> clamped at the max elemental charge
    /// </summary>
    public void RecoverElementCharge(ElementType element, int charge)
    {
        elementCharge[element] = Mathf.Clamp(GetElementCharge(element) + charge, 0, GetElementMaxCharge(element));
    }
    #endregion

    private void OnValidate()
    {
        for (int i = 0; i < spells.Length; i++)
        {
            if (spells[i] && spells[i].GetComponent<ISpell>() == null)
            {
                spells[i] = null;
                Debug.LogError("Cannot assign spell that does not implement ISpell");
            }
        }

        if (elementRecoverPerSecond.Length != System.Enum.GetValues(typeof(ElementType)).Length)
            System.Array.Resize(ref elementRecoverPerSecond, System.Enum.GetValues(typeof(ElementType)).Length);
        if (elementColors.Length != System.Enum.GetValues(typeof(ElementType)).Length)
            System.Array.Resize(ref elementColors, System.Enum.GetValues(typeof(ElementType)).Length);
    }

    public void MapChange()
    {
        foreach (Transform t in spellAnchor)
        {
            Destroy(t.gameObject);
        }
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
    NONE
}

public enum SpellTargetType
{
    SELF,
    GROUND,
    ENEMY
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
