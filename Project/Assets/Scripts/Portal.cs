using UnityEngine;

public class Portal : MonoBehaviour
{
    public int destinationRoom;
    [HideInInspector]
    public bool justArrived = false;

    private void OnTriggerEnter(Collider other)
    {
        if (justArrived)
            return;

        GameObject go = other.gameObject;
        GameObject goP = Utils.FindTaggedParent(go);
        if (goP) go = goP;

        if (go.tag != "Mage") return;

        LayoutTiles.instance.BuildRoom(destinationRoom);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Mage")
            justArrived = false;
    }
}
