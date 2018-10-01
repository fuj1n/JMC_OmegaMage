using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, IEnemy
{
    public float damage;
    public float maxHealth;

    [Header("Drops")]
    public bool doDrops = false;
    [ConditionalHide(true, ConditionalSourceField = "doDrops")]
    public GameObject drop;
    [ConditionalHide(true, ConditionalSourceField = "doDrops")]
    public int minDrops = 5;
    [ConditionalHide(true, ConditionalSourceField = "doDrops")]
    public int maxDrops = 30;

    protected Rigidbody Rigidbody
    {
        get
        {
            if (rigidbody_value == null)
                rigidbody_value = GetComponent<Rigidbody>();

            return rigidbody_value;
        }
    }
    private Rigidbody rigidbody_value;
    private float health = 1F;

    private Dictionary<ElementType, float> damageSources = new Dictionary<ElementType, float>();
    private Tween damageTween;

    private Vector3 knockbackDirection;
    private float knockbackDistance;
    private float knockbackDuration;

    private float knockbackTime;

    private List<Transform> immobilizationAgents = new List<Transform>();

    public abstract void DoAI();

    private void FixedUpdate()
    {
        if (knockbackTime > 0F)
        {
            Rigidbody.velocity = knockbackDirection * (knockbackDistance / knockbackDuration);
            knockbackTime -= Time.fixedDeltaTime;

            return;
        }

        if (immobilizationAgents.Count != 0)
            immobilizationAgents = immobilizationAgents.Where(a => a).Distinct().ToList();

        if (immobilizationAgents.Count == 0)
            DoAI();

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

    private void LateUpdate()
    {
        float damage = 0F;
        foreach (KeyValuePair<ElementType, float> source in damageSources)
        {
            damage += source.Value;
        }

        health -= damage / maxHealth;
        health = Mathf.Clamp01(health);

        damageSources.Clear();

        if (health <= 0F)
            Die();

        if (damage > 0F)
            if (damageTween == null || !damageTween.IsPlaying())
            {
                GetCharacterTransform().localScale = new Vector3(.8F, .8F, .8F);
                damageTween = GetCharacterTransform().DOScale(1F, 0.25F);
            }
    }

    public virtual float GetDamage()
    {
        return damage;
    }

    public virtual Transform GetCharacterTransform()
    {
        return transform;
    }

    public virtual float GetScaledDamage(ElementType element, float damage)
    {
        return damage;
    }

    public virtual bool CanTakeDamage(ElementType element)
    {
        return true;
    }

    public virtual void TakeDamage(float amount, ElementType element, bool dot)
    {
        if (!CanTakeDamage(element))
            return;

        if (dot)
            amount *= Time.deltaTime;

        amount = GetScaledDamage(element, amount);

        if (amount <= 0F)
            return;


        damageSources[element] = Mathf.Max(damageSources.SingleOrDefault(x => x.Key == element).Value, amount);
    }

    public virtual void Die()
    {
        if (doDrops)
            for (int i = 0; i < Random.Range(minDrops, maxDrops + 1); i++)
                Instantiate(drop, transform.position, drop.transform.rotation);

        Destroy(gameObject);
    }

    public virtual void SetKnockback(Vector3 knockbackDirection, float knockbackDistance, float knockbackDuration)
    {
        this.knockbackDirection = knockbackDirection;
        this.knockbackDistance = knockbackDistance;
        this.knockbackDuration = knockbackDuration;
        knockbackTime = knockbackDuration;
    }

    public virtual void AddImmobilizedAgent(Transform agent)
    {
        immobilizationAgents.Add(agent);
        OnImmobilized();
    }

    public virtual void OnImmobilized()
    {
        if (Rigidbody)
            Rigidbody.velocity = Vector3.zero;
    }
}
