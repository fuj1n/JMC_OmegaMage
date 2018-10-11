using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExpandBox : MonoBehaviour, IPointerClickHandler
{
    [Header("State")]
    public bool isExpanded = true;
    public float speed = 1F;

    [Header("Positions")]
    public Vector2 expanded = Vector2.zero;
    public Vector2 retracted = Vector2.zero;
    public float expandedArrowAngle = 180F;
    public float retractedArrowAngle = 0F;

    private Tween internalTween;
    private new RectTransform transform;
    private RectTransform arrow;

    private void Awake()
    {
        UpdateState();
        transform = GetComponent<RectTransform>();
        arrow = transform.Find("Arrow").GetComponent<RectTransform>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (internalTween != null && internalTween.IsPlaying())
            return;

        isExpanded = !isExpanded;
        UpdateState();
    }

    private void UpdateState()
    {
        Vector2 target = isExpanded ? expanded : retracted;
        internalTween = transform.DOAnchorPos(target, speed);

        if (arrow)
        {
            float angle = isExpanded ? expandedArrowAngle : retractedArrowAngle;
            arrow.DORotate(Vector3.forward * angle, speed);
        }
    }
}
