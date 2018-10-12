using UnityEngine;

public class EnemySpellProjectile : MonoBehaviour, IEnemy
{
    public float damage = 5F;
    public float speed = 5F;

    public float maxAliveTime = 0F;

    private Vector3 target;
    private float aliveTime;

    private void Start()
    {
        target = Mage.instance.transform.position;
    }

    private void Update()
    {
        if (Vector3.Distance(target, transform.position) <= 0.2F)
        {
            Destroy(gameObject);
        }

        if (maxAliveTime > 0F && aliveTime >= maxAliveTime)
            Destroy(gameObject);
        aliveTime += Time.deltaTime;

        transform.LookAt(target);
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject go = Utils.FindTaggedParent(other.gameObject);
        if (go && go.CompareTag("Mage"))
        {
            Destroy(gameObject);
        }
    }

    public float GetDamage() => damage;

    public void DoAI()
    {
        //NOOP
    }

    public void TakeDamage(float amount, ElementType element, bool dot)
    {
        // NOOP
    }

    public void Die()
    {
        // NOOP
    }

    public void SetKnockback(Vector3 knockbackDirection, float knockbackDistance, float knockbackDuration)
    {
        // NOOP
    }

    public void AddImmobilizedAgent(Transform agent)
    {
        //NOOP
    }
}