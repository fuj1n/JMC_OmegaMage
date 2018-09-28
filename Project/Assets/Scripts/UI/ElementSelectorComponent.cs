using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ElementSelectorComponent : MonoBehaviour, IPointerClickHandler
{
    public ElementType element;

    private Image fillBar;

    private void Awake()
    {
        fillBar = transform.GetChild(0).GetComponent<Image>();
    }

    private void Update()
    {
        bool isUnlocked = Mage.instance.IsUnlockedElement(element);

        // Done it this way to prevent constant calls to SetActive which are expensive
        if (fillBar.gameObject.activeInHierarchy != isUnlocked)
            fillBar.gameObject.SetActive(isUnlocked);
        if (!isUnlocked)
            return;

        fillBar.fillAmount = Mage.instance.GetElementCharge(element);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Mage.instance.SelectElement(element);
    }
}
