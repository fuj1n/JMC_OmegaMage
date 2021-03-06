﻿using UnityEngine;

[EntityFactoryAttribute('b')]
public class EnemyBug : EnemyBase
{
    public float speed = 0.5F;

    private Vector3 walkTarget;
    private bool walking;
    private Transform character;

    private new Rigidbody rigidbody;

    private void Awake()
    {
        character = transform.Find("CharacterTrans");

        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        WalkTo(Mage.instance.transform.position);
    }

    #region Walking
    /// <summary>
    /// Tells the bug to walk towards and <see cref="Face(Vector3)"/> <paramref name="target"/>
    /// </summary>
    public void WalkTo(Vector3 target)
    {
        walkTarget = target;
        walkTarget.z = 0;
        walking = true;
        Face(walkTarget);
    }

    /// <summary>
    /// Faces the bug towards <paramref name="pos"/>
    /// </summary>
    public void Face(Vector3 pos)
    {
        Vector3 delta = pos - transform.position;
        // Get rotation around Z that points X axis towards pos
        float rot = Mathf.Rad2Deg * Mathf.Atan2(delta.y, delta.x);
        character.rotation = Quaternion.Euler(0, 0, rot);
    }

    /// <summary>
    /// Tells the bug to stop walking
    /// </summary>
    public void StopWalking()
    {
        walking = false;
        rigidbody.velocity = Vector3.zero;
    }

    public override void DoAI()
    {
        if (walking)
        {
            rigidbody.velocity = (walkTarget - transform.position).normalized * speed;

            // If we're very close to the target, stop moving
            if ((walkTarget - transform.position).magnitude < speed * Time.fixedDeltaTime)
            {
                transform.position = walkTarget;
                StopWalking();
            }
        }
        else
        {
            rigidbody.velocity = Vector3.zero;
        }
    }
    #endregion

    public override bool CanTakeDamage(ElementType element)
    {
        return element != ElementType.AIR;
    }
}
