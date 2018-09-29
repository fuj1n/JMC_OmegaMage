using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, IEnemy
{
    public float damage;
    public float maxHealth;

    protected new Rigidbody rigidbody
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
            rigidbody.velocity = knockbackDirection * (knockbackDistance / knockbackDuration);
            knockbackTime -= Time.fixedDeltaTime;

            return;
        }

        if (immobilizationAgents.Count != 0)
            immobilizationAgents = immobilizationAgents.Where(a => a).Distinct().ToList();

        if (immobilizationAgents.Count == 0)
            DoAI();
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
        if (rigidbody)
            rigidbody.velocity = Vector3.zero;
    }
}
