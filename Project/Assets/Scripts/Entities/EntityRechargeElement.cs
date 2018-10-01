using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EntityRechargeElement : MonoBehaviour, IEntity
{
    [Header("Functional")]
    public int recoverCharge;
    public float lifeTime = 10F;
    public ElementType[] droppableElements = { };

    [Header("Effect")]
    public Vector3 rotateSpeed;
    public Vector3 randomVelocityMin;
    public Vector3 randomVelocityMax;

    private ElementType element;
    private Transform effect;
    private new Rigidbody rigidbody;

    private float aliveTime = 0F;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

        // Select a random unlocked element
        droppableElements = droppableElements.Where(e => Mage.instance.IsUnlockedElement(e)).ToArray();
        if (droppableElements.Length == 0)
        {
            Destroy(gameObject);
            return;
        }
        element = droppableElements[Random.Range(0, droppableElements.Length)];

        effect = transform.GetChild(0);
        effect.GetComponent<SpriteRenderer>().sprite = Mage.instance.elementSprites[(int)element];

        // Removed light cause too many caused world lighting to derp up
        //effect.GetComponent<Light>().color = Mage.instance.elementColors[(int)element];

        // Random velocity
        rigidbody.velocity = new Vector3(
            Random.Range(randomVelocityMin.x, randomVelocityMax.x),
            Random.Range(randomVelocityMin.y, randomVelocityMax.y),
            Random.Range(randomVelocityMin.z, randomVelocityMax.z));

        // Make sure we're not in the ground as this will mess with physics
        RaycastHit[] hits = Physics.BoxCastAll(transform.position, Vector3.one * .4F, Vector3.back);
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

    private void Start()
    {
        // Set us to the world anchor so we will be destroyed when the rooms switch
        // Done in start cause WorldAnchor is guaranteed to be initialized by first frame, but not zeroth
        transform.SetParent(LayoutTiles.instance.WorldAnchor);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mage"))
        {
            Mage.instance.RecoverElementCharge(element, recoverCharge);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        effect.localRotation *= Quaternion.Euler(rotateSpeed * Time.deltaTime);

        aliveTime += Time.deltaTime;
        if (aliveTime >= lifeTime)
            Destroy(gameObject);
    }
}
