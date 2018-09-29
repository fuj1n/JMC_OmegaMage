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
        fill.fillAmount = Mathf.Lerp(fill.fillAmount, Mage.instance.GetHealth(), Time.deltaTime * 4);
        text.text = System.Math.Round(Mage.instance.GetHealth() * Mage.instance.maxHealth, 2) + "/" + Mage.instance.maxHealth;
    }

}
