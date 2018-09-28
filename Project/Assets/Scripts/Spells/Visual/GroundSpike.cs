using DG.Tweening;
using UnityEngine;

public class GroundSpike : MonoBehaviour
{
    public float pokeTime = .5F;
    public Vector3 extend;

    private Vector3 retract;
    private SpellGround groundSpell;
    private SpellAttach attachSpell;
    private new Collider collider;

    private void Awake()
    {
        groundSpell = GetComponentInParent<SpellGround>();
        attachSpell = GetComponentInParent<SpellAttach>();
        collider = GetComponentInParent<Collider>();
        if (collider)
            collider.enabled = false;

        retract = transform.localPosition;
    }

    private void Start()
    {
        float duration = groundSpell ? groundSpell.duration : attachSpell.duration;
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOLocalMove(extend, pokeTime));
        seq.AppendCallback(() => { if (collider) collider.enabled = true; });
        seq.AppendInterval((duration - pokeTime) * .5F);
        seq.Append(transform.DOLocalMove(retract, duration - pokeTime - ((duration - pokeTime) * .5F)));
    }
}
