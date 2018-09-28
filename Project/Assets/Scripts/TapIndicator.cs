using DG.Tweening;
using UnityEngine;

public class TapIndicator : MonoBehaviour
{
    public float lifeTime = 0.4f;
    public float[] scales;
    public Color[] colors;

    private Material renderMaterial;

    private void Awake()
    {
        transform.localScale = Vector3.zero;

        renderMaterial = GetComponent<Renderer>().material;
        GetComponent<Renderer>().material = renderMaterial;
    }

    private void Start()
    {
        Sequence scaleSequence = DOTween.Sequence();
        Sequence colorSequence = DOTween.Sequence().OnComplete(() => Destroy(gameObject));

        for (int i = 0; i < scales.Length; i++)
        {
            scaleSequence.Append(transform.DOScale(scales[i], lifeTime / scales.Length));
        }

        for (int i = 0; i < colors.Length; i++)
        {
            colorSequence.Append(renderMaterial.DOColor(colors[i], lifeTime / colors.Length));
        }
    }

    private void Update()
    {
        Debug.Log(renderMaterial.color.a);
    }
}
