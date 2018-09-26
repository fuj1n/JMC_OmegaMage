using UnityEngine;

public class ElementInventoryButton : MonoBehaviour
{
    public ElementType type;

    private void OnMouseUpAsButton()
    {
        Mage.instance.SelectElement(type);
    }
}
