using System.Collections;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public char destinationRoom;
    [HideInInspector]
    public bool justArrived = false;

    private void OnCollisionEnter(Collision collision)
    {
        OnTriggerEnter(collision.collider);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (justArrived)
            return;

        GameObject go = other.gameObject;
        GameObject goP = Utils.FindTaggedParent(go);
        if (goP) go = goP;

        if (go.tag != "Mage") return;

        OnPortalEntered();
    }

    protected virtual void OnPortalEntered()
    {
        StartCoroutine(TeleportPlayer());
    }

    private IEnumerator TeleportPlayer()
    {
        yield return null;

        LayoutTiles.instance.BuildRoom(destinationRoom);
        StopAllCoroutines();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Mage")
            justArrived = false;
    }
}
