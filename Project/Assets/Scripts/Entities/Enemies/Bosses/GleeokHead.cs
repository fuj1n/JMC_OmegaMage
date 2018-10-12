using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class GleeokHead : EnemyPartialBoss
{
    [Header("Head")]
    public float minDistance = .1F;
    public float maxDistance = 1.5F;

    public float targetDither = .5F;

    public float speed = 2F;

    public GameObject fireballTemplate;
    public Vector2 fireballTimeRange = new Vector2(.5F, 3F);

    private LineRenderer lineRender;
    private Transform track;

    private bool extending = true;
    private Vector3 moveTarget;

    private float nextFireball;

    private void Start()
    {
        lineRender = GetComponentInChildren<LineRenderer>();
        track = transform.parent;

        Transform anchor = track.Find("Anchor");
        if (anchor)
            track = anchor;

        nextFireball = Random.Range(fireballTimeRange.x, fireballTimeRange.y);

        UpdateMoveTarget();
    }

    public override void DoAI()
    {
        if (isDead)
            return;

        Vector3 dir = moveTarget - transform.position;
        dir = (dir / dir.magnitude);

        transform.position += dir * Time.fixedDeltaTime;

        float dist = Vector3.Distance(transform.position, track.position);
        if (Vector3.Distance(transform.position, moveTarget) <= minDistance || (extending && dist >= maxDistance) || (!extending && dist <= minDistance))
        {
            extending = !extending;
            UpdateMoveTarget();
        }

        lineRender.SetPosition(0, transform.position);
        lineRender.SetPosition(lineRender.positionCount - 1, track.position);

        if (!fireballTemplate)
            return;

        nextFireball -= Time.fixedDeltaTime;
        if (nextFireball <= 0F)
        {
            nextFireball = Random.Range(fireballTimeRange.x, fireballTimeRange.y);

            Instantiate(fireballTemplate, transform, false).transform.position = transform.position;
        }
    }

    private void UpdateMoveTarget()
    {
        if (extending)
            moveTarget = Mage.instance.transform.position;
        else
            moveTarget = track.position;

        moveTarget.x += Random.Range(-targetDither, targetDither);
        moveTarget.y += Random.Range(-targetDither, targetDither);
        moveTarget.z = transform.position.z;
    }

    public override void SetKnockback(Vector3 knockbackDirection, float knockbackDistance, float knockbackDuration)
    {
        // No knockback
    }
}
