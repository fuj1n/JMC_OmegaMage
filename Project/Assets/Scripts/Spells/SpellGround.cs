using UnityEngine;

public class SpellGround : SpellBase
{
    public float duration = 4F;
    public float durationVariance = 0.5F;

    public bool fadeAnimation = true;
    [ConditionalHide(true, ConditionalSourceField = "fadeAnimation")]
    public float fadeTime = 1F;

    public bool hasKnockback = false;
    [ConditionalHide(true, ConditionalSourceField = "hasKnockback")]
    public float knockbackDistance = 1F;
    [ConditionalHide(true, ConditionalSourceField = "hasKnockback")]
    public float knockbackDuration = 0.5F;

    public float damagePerSecond = 10F;

    private float startTime;

    private void Start()
    {
        startTime = Time.time;
        duration = Random.Range(duration - durationVariance, duration + durationVariance);
    }

    private void Update()
    {
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
            recepient.TakeDamage(damagePerSecond, element, true);
            if (hasKnockback)
            {
                recepient.SetKnockback((recepient.transform.position - transform.position).normalized, knockbackDistance, knockbackDuration);
            }
        }
    }

    public override void Cast(ISpellParams parameters)
    {
        SpellGroundParams groundParams = (SpellGroundParams)parameters;

        foreach (Vector3 point in groundParams.positions)
        {
            Instantiate(gameObject, point, gameObject.transform.rotation, Mage.instance.spellAnchor);
        }
    }

    public override string GetTargetType() => "Ground";
}

public struct SpellGroundParams : ISpellParams
{
    public Vector3[] positions;
}