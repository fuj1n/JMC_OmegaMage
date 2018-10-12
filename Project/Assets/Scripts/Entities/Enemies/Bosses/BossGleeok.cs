using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[EntityFactory('Z')]
public class BossGleeok : BossBase
{
    [Header("Gleeok")]
    public int headsCount = 3;
    public GameObject headTemplate;

    private List<GameObject> heads = new List<GameObject>();

    public override void DoAI()
    {
        if (isDead)
            return;

        transform.LookAt(transform.position);

        // Filter out heads that no longer exist
        heads = heads.Where(h => h).ToList();

        if (heads.Count <= 0)
            Die();
    }

    [EntityFactoryCallback]
    public void OnSpawn()
    {
        maxHealth = float.PositiveInfinity;

        heads.Clear();
        for (int i = 0; i < headsCount; i++)
            heads.Add(Instantiate(headTemplate, transform));
    }

    public override void OnDeathAnimationEnd()
    {
        base.OnDeathAnimationEnd();

        SceneManager.LoadScene("GameWin");
    }

    public override bool CanTakeDamage(ElementType element)
    {
        // Can't take damage
        return false;
    }

    public override void SetKnockback(Vector3 knockbackDirection, float knockbackDistance, float knockbackDuration)
    {
        // No knockback
    }

    public override void AddImmobilizedAgent(Transform agent)
    {
        // No immobilize
    }
}
