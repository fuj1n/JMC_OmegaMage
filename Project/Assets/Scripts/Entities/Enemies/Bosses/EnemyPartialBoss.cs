using System.Collections;
using UnityEngine;

public abstract class EnemyPartialBoss : EnemyBase
{
    [Header("Death")]
    public GameObject deathExplosion;
    public int explosionCount = 5;
    public float delayBetweenExplosions = 1F;

    [HideInInspector]
    public bool isDead = false;

    public override void Die()
    {
        isDead = true;
        StartCoroutine(Explode());
    }


    private IEnumerator Explode()
    {
        yield return 0;

        Collider[] cols = GetComponentsInChildren<Collider>();
        foreach (Collider c in cols)
            Destroy(c);

        if (deathExplosion && explosionCount > 0)
            for (int i = 0; i < explosionCount; i++)
            {
                Instantiate(deathExplosion, transform, false);
                yield return new WaitForSeconds(delayBetweenExplosions);
            }
        OnDeathAnimationEnd();
    }

    public virtual void OnDeathAnimationEnd()
    {
        base.Die();
    }
}
