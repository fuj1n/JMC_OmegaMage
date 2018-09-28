using UnityEngine;

public class SpellProjectile : SpellBase
{
    public float damage = 5F;
    public float speed = 5F;

    public float maxAliveTime = 0F;

    private Transform target;
    private float aliveTime;

    private void Update()
    {
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
            IEnemy enemy = target.GetComponent<IEnemy>();
            if (enemy != null)
                enemy.TakeDamage(damage, element, false);

            Destroy(gameObject);
        }
    }

    public override void Cast(ISpellParams parameters)
    {
        SpellTargetParams projectileParameters = (SpellTargetParams)parameters;
        Instantiate(gameObject, projectileParameters.source + Vector3.back * .6F, gameObject.transform.rotation, Mage.instance.spellAnchor).GetComponent<SpellProjectile>().target = projectileParameters.destination;
    }

    public override string GetTargetType() => "Enemy";
}

public struct SpellTargetParams : ISpellParams
{
    public Vector3 source;
    public Transform destination;
}
