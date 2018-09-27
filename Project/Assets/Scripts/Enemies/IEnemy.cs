using UnityEngine;

public interface IEnemy
{
    GameObject gameObject { get; }
    Transform transform { get; }

    float GetDamage();
    void DoAI();

    /// <summary>
    /// Deals damage to the entity
    /// </summary>
    /// <param name="amount">The amount of damage to deal</param>
    /// <param name="element">The source of the damage</param>
    /// <param name="dot">Whether the amount should be multiplied by dt</param>
    void TakeDamage(float amount, ElementType element, bool dot);
    /// <summary>
    /// Kills the entity
    /// </summary>
    void Die();
}
