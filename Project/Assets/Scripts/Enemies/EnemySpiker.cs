using UnityEngine;

[EnemyFactory('^', 'v', '<', '>')]
public class EnemySpiker : EnemyBase
{
    public float speed = 5F;

    private Vector3 moveDirection = Vector3.up;
    private new Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject go = Utils.FindTaggedParent(other.gameObject);
        if (!go)
            return;

        if (go.tag == "Ground")
        {
            float dot = Vector3.Dot(moveDirection, go.transform.position - transform.position);
            if (dot > 0)
                moveDirection *= -1;
        }
    }

    public override void DoAI()
    {
        rigidbody.velocity = moveDirection * speed;
    }

    public override bool CanTakeDamage(ElementType element)
    {
        return false;
    }

    [EnemyFactoryCallback]
    public void OnFactoryGenerated(char c)
    {
        switch (c)
        {
            case '^':
                moveDirection = Vector3.up;
                break;
            case 'v':
                moveDirection = Vector3.down;
                break;
            case '<':
                moveDirection = Vector3.left;
                break;
            case '>':
                moveDirection = Vector3.right;
                break;
        }
    }
}
