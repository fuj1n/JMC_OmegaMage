using UnityEngine;

public abstract class SpellSelfBase : SpellBase
{
    public float effectLength = 5F;

    public bool fadeAnimation = true;
    [ConditionalHide(true, ConditionalSourceField = "fadeAnimation")]
    public float fadeTime = 4F;

    private float startTime;

    public override bool Cast(ISpellParams parameters)
    {
        Instantiate(gameObject, Mage.instance.transform, false);

        return true;
    }

    protected virtual void Start()
    {
        startTime = Time.time;
    }

    protected virtual void Update()
    {
        float elapsed = Mathf.Clamp01((Time.time - startTime) / effectLength);

        if (fadeAnimation)
        {
            float fadeAt = 1 - (fadeTime / effectLength);
            if (elapsed > fadeAt)
            {
                Fade(elapsed, fadeAt);
            }
        }

        if (elapsed >= 1F)
            Destroy(gameObject);
    }

    protected virtual void Fade(float elapsed, float fadeAt)
    {
        float fadePercent = Mathf.Clamp01((elapsed - fadeAt) / (1 - fadeAt));
        Vector3 position = transform.position;
        position.z = fadePercent * 2;
        transform.position = position;
    }

    public override SpellTargetType GetTargetType() => SpellTargetType.SELF;
}

public struct SpellSelfParams : ISpellParams
{
    public static SpellSelfParams self = new SpellSelfParams();
}