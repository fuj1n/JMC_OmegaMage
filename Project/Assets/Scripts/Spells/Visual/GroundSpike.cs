using DG.Tweening;
using UnityEngine;

public class GroundSpike : MonoBehaviour
{
    public float pokeTime = .5F;
    public Vector3 extend;

    private Vector3 retract;
    private SpellGround spell;
    private new Collider collider;

    private void Awake()
    {
        spell = GetComponentInParent<SpellGround>();
        collider = spell.GetComponent<Collider>();
        collider.enabled = false;

        retract = transform.localPosition;
    }

    private void Start()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOLocalMove(extend, pokeTime));
        seq.AppendCallback(() => collider.enabled = true);
        seq.AppendInterval((spell.duration - pokeTime) * .5F);
        seq.Append(transform.DOLocalMove(retract, spell.duration - pokeTime - ((spell.duration - pokeTime) * .5F)));
    }
}
