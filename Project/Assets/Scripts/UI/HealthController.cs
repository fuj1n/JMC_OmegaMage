using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public TextMeshProUGUI text;
    private Image fill;

    private void Awake()
    {
        fill = GetComponent<Image>();
    }

    private void Update()
    {
        fill.fillAmount = Mage.instance.GetHealth();
        text.text = System.Math.Round(Mage.instance.GetHealth() * Mage.instance.maxHealth, 2) + "/" + Mage.instance.maxHealth;
    }

}
