using System.Collections.Generic;
using UnityEngine;

public abstract class BossBase : EnemyPartialBoss
{
    public GameObject lockedDoorTemplate;

    private Portal[] portals;
    private List<GameObject> lockedDoors = new List<GameObject>();

    [EntityFactoryCallback]
    public virtual void OnBaseSpawn()
    {
        portals = FindObjectsOfType<Portal>();

        foreach (Portal p in portals)
        {
            p.gameObject.SetActive(false);
            if (lockedDoorTemplate)
            {
                GameObject lockedDoor = Instantiate(lockedDoorTemplate, p.transform.position, p.transform.rotation);
                lockedDoors.Add(lockedDoor);
            }
        }
    }

    public override void OnDeathAnimationEnd()
    {
        lockedDoors.ForEach(d => Destroy(d));
        foreach (Portal p in portals)
            if (p)
                p.gameObject.SetActive(true);

        base.OnDeathAnimationEnd();
    }
}
