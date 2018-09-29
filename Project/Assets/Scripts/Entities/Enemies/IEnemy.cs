using UnityEngine;

public interface IEnemy : IEntity
{
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

    /// <summary>
    /// Sets the enemy to be knocked back
    /// </summary>
    /// <param name="knockbackDirection">The direction to knock the enemy</param>
    /// <param name="knockbackDistance">The distance to knock the enemy</param>
    /// <param name="knockbackDuration">How long to knock the enemy for</param>
    void SetKnockback(Vector3 knockbackDirection, float knockbackDistance, float knockbackDuration);

    /// <summary>
    /// Adds an <paramref name="agent"/> preventing this object from moving
    /// </summary>
    void AddImmobilizedAgent(Transform agent);
}
