using UnityEngine;

public class SpellAttach : SpellBase
{
    public float damagePerSecond = 5F;

    public float duration = 4F;
    public float durationVariance = 0.5F;

    public bool fadeAnimation = true;
    [ConditionalHide(true, ConditionalSourceField = "fadeAnimation")]
    public float fadeTime = 1F;

    public bool immobilizeEnemy = false;

    private float startTime;

    private Transform target;

    private void Start()
    {
        startTime = Time.time;
        duration = Random.Range(duration - durationVariance, duration + durationVariance);
    }

    private void Update()
    {
        if (!target)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = target.position;

        float elapsed = Mathf.Clamp01((Time.time - startTime) / duration);

        if (fadeAnimation)
        {
            float fadeAt = 1 - (fadeTime / duration);
            if (elapsed > fadeAt)
            {
                float fadePercent = Mathf.Clamp01((elapsed - fadeAt) / (1 - fadeAt));
                Vector3 position = transform.position;
                position.z = fadePercent * 2;
                transform.position = position;
            }
        }

        if (elapsed >= 1F)
            Destroy(gameObject);
    }

    private void OnCollisionStay(Collision collision)
    {
        OnTriggerStay(collision.collider);
    }

    private void OnTriggerStay(Collider other)
    {
        IEnemy recepient = other.GetComponent<IEnemy>();
        if (recepient != null)
        {
            if (damagePerSecond != 0F)
                recepient.TakeDamage(damagePerSecond, element, true);
        }
    }

    public override void Cast(ISpellParams parameters)
    {
        SpellTargetParams targetParams = (SpellTargetParams)parameters;

        Transform t = Instantiate(gameObject).transform;
        t.GetComponent<SpellAttach>().target = targetParams.destination;

        if (immobilizeEnemy)
        {
            IEnemy enemy = targetParams.destination.GetComponent<IEnemy>();
            if (enemy != null)
                enemy.AddImmobilizedAgent(t);
        }
    }

    public override string GetTargetType() => "Enemy";
}
