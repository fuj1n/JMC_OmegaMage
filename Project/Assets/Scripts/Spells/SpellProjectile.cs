using UnityEngine;

public class SpellProjectile : SpellBase
{
    public float damage = 5F;
    public float speed = 5F;

    public float maxAliveTime = 0F;

    public bool hasKnockback = false;
    [ConditionalHide(true, ConditionalSourceField = "hasKnockback")]
    public float knockbackDistance = 1F;
    [ConditionalHide(true, ConditionalSourceField = "hasKnockback")]
    public float knockbackDuration = 0.5F;

    private Transform target;
    private float aliveTime;

    private void Update()
    {
        if (!target)
        {
            Destroy(gameObject);
            return;
        }

        if (maxAliveTime > 0F && aliveTime >= maxAliveTime)
            Destroy(gameObject);
        aliveTime += Time.deltaTime;

        transform.LookAt(target.position);
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnTriggerEnter(collision.collider);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == target)
        {
            IEnemy recepient = target.GetComponent<IEnemy>();
            if (recepient != null)
                recepient.TakeDamage(damage, element, false);
            if (hasKnockback)
                recepient.SetKnockback((recepient.transform.position - transform.position).normalized, knockbackDistance, knockbackDuration);

            Destroy(gameObject);
        }
    }

    public override bool Cast(ISpellParams parameters)
    {
        SpellTargetParams projectileParameters = (SpellTargetParams)parameters;
        Instantiate(gameObject, projectileParameters.source + Vector3.back * .6F, gameObject.transform.rotation, Mage.instance.spellAnchor).GetComponent<SpellProjectile>().target = projectileParameters.destination;

        return true;
    }

    public override SpellTargetType GetTargetType() => SpellTargetType.ENEMY;
}

public struct SpellTargetParams : ISpellParams
{
    public Vector3 source;
    public Transform destination;
}
