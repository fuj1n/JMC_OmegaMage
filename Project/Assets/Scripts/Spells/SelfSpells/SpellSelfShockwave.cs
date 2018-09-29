using DG.Tweening;
using UnityEngine;

public class SpellSelfShockwave : SpellBase
{
    public float shockwaveDistance = 2F;
    public float shockwaveDuration = 1F;

    public float damage = 5F;
    public bool dot = false;

    public int shockwaveVectors = 12;

    public bool hasKnockback = false;
    [ConditionalHide(true, ConditionalSourceField = "hasKnockback")]
    public float knockbackDistance = 1F;
    [ConditionalHide(true, ConditionalSourceField = "hasKnockback")]
    public float knockbackDuration = 0.5F;

    private Vector3 target;

    private void Start()
    {
        transform.DOMove(target, shockwaveDuration).OnComplete(() => Destroy(gameObject));
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnTriggerEnter(collision.collider);
    }

    private void OnTriggerEnter(Collider other)
    {
        IEnemy recepient = other.gameObject.GetComponent<IEnemy>();
        if (recepient == null)
            return;
        if (recepient != null && damage > 0F)
            recepient.TakeDamage(damage, element, false);
        if (hasKnockback)
            recepient.SetKnockback((recepient.transform.position - transform.position).normalized, knockbackDistance, knockbackDuration);

        Destroy(gameObject);
    }

    private void OnCollisionStay(Collision collision)
    {
        OnTriggerStay(collision.collider);
    }

    private void OnTriggerStay(Collider other)
    {
        if (dot)
            OnTriggerEnter(other);
    }

    public override bool Cast(ISpellParams parameters)
    {
        for (int i = 0; i < shockwaveVectors; i++)
        {
            // Create objects targetting a circle around the player
            SpellSelfShockwave spell = Instantiate(gameObject, Mage.instance.transform.position, transform.rotation, Mage.instance.spellAnchor).GetComponent<SpellSelfShockwave>();
            spell.target = Mage.instance.transform.position + new Vector3(shockwaveDistance * Mathf.Cos(2 * Mathf.PI * i / shockwaveVectors), shockwaveDistance * Mathf.Sin(2 * Mathf.PI * i / shockwaveVectors), 0F);
        }

        return true;
    }

    public override SpellTargetType GetTargetType() => SpellTargetType.SELF;
}
