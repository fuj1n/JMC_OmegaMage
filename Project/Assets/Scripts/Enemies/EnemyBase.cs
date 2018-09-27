using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, IEnemy
{
    public float damage;
    public float maxHealth;

    private float health = 1F;

    private Dictionary<ElementType, float> damageSources = new Dictionary<ElementType, float>();
    private Tween damageTween;

    public abstract void DoAI();

    private void FixedUpdate()
    {
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

    public float GetDamage()
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

    public void TakeDamage(float amount, ElementType element, bool dot)
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

    public void Die()
    {
        Destroy(gameObject);
    }
}
