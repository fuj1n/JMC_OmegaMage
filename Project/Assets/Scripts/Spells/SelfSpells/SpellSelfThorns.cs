using UnityEngine;

public class SpellSelfThorns : SpellSelfBase
{
    public float multiplier = .25F;

    private bool isPlaying = true;

    private void Awake()
    {
        Mage.instance.SetThorns(multiplier, element, transform);
    }

    protected override void Fade(float elapsed, float fadeAt)
    {
        if (isPlaying && effectLength * elapsed >= effectLength - 1F)
        {
            GetComponent<ParticleSystem>().Stop();
            isPlaying = false;
        }
    }
}
