using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyBug : MonoBehaviour
{
    public float speed = 0.5F;
    public float maxHealth;


    // public bool ____________________; // ??? Why do these exist?

    private float health = 1F;
    private Vector3 walkTarget;
    private bool walking;
    private Transform character;

    private new Rigidbody rigidbody;
    private Dictionary<ElementType, float> damageSources = new Dictionary<ElementType, float>();

    private Tween damageTween;

    private void Awake()
    {
        character = transform.Find("CharacterTrans");

        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        WalkTo(Mage.instance.transform.position);
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
                character.localScale = new Vector3(.8F, .8F, .8F);
                damageTween = character.DOScale(1F, 0.25F);
            }

    }

    #region Walking
    /// <summary>
    /// Tells the bug to walk towards and <see cref="Face(Vector3)"/> <paramref name="target"/>
    /// </summary>
    public void WalkTo(Vector3 target)
    {
        walkTarget = target;
        walkTarget.z = 0;
        walking = true;
        Face(walkTarget);
    }

    /// <summary>
    /// Faces the bug towards <paramref name="pos"/>
    /// </summary>
    public void Face(Vector3 pos)
    {
        Vector3 delta = pos - transform.position;
        // Get rotation around Z that points X axis towards pos
        float rot = Mathf.Rad2Deg * Mathf.Atan2(delta.y, delta.x);
        character.rotation = Quaternion.Euler(0, 0, rot);
    }

    /// <summary>
    /// Tells the bug to stop walking
    /// </summary>
    public void StopWalking()
    {
        walking = false;
        rigidbody.velocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        if (walking)
        {
            rigidbody.velocity = (walkTarget - transform.position).normalized * speed;

            // If we're very close to the target, stop moving
            if ((walkTarget - transform.position).magnitude < speed * Time.fixedDeltaTime)
            {
                transform.position = walkTarget;
                StopWalking();
            }
        }
        else
        {
            rigidbody.velocity = Vector3.zero;
        }
    }
    #endregion

    /// <summary>
    /// Deals damage to the entity
    /// </summary>
    /// <param name="amount">The amount of damage to deal</param>
    /// <param name="element">The source of the damage</param>
    /// <param name="dot">Whether the amount should be multiplied by dt</param>
    public void Damage(float amount, ElementType element, bool dot)
    {
        if (dot)
            amount *= Time.deltaTime;

        switch (element)
        {
            case ElementType.FIRE:
                damageSources[element] = Mathf.Max(damageSources.SingleOrDefault(x => x.Key == element).Value, amount);
                break;
            case ElementType.AIR:
                break;
            default:
                damageSources[element] += amount;
                break;
        }
    }

    /// <summary>
    /// Kills the entity
    /// </summary>
    public void Die()
    {
        Destroy(gameObject);
    }
}
