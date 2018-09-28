using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ElementInfoManager : MonoBehaviour
{
    public TextMeshProUGUI elementName;

    private TextMeshProUGUI[] descriptions;
    private TextMeshProUGUI[] costs;

    private ElementType lastElement;
    private SpellTargetType[] targetTypes;

    private void Awake()
    {
        TextMeshProUGUI targets = transform.Find("Targets").GetComponent<TextMeshProUGUI>();
        targets.text = "";

        targetTypes = System.Enum.GetValues(typeof(SpellTargetType)).Cast<SpellTargetType>().ToArray();

        foreach (SpellTargetType target in targetTypes)
        {
            targets.text += target.ToString().ToLower().FirstToUpper() + "\n\n";
        }

        descriptions = new TextMeshProUGUI[targetTypes.Length];
        costs = new TextMeshProUGUI[targetTypes.Length];

        Transform descriptionsGroup = transform.Find("Descriptions");
        Transform costsGroup = transform.Find("Costs");

        for (int i = 0; i < targetTypes.Length; i++)
        {
            descriptions[i] = descriptionsGroup.GetChild(i).GetComponent<TextMeshProUGUI>();
            costs[i] = costsGroup.GetChild(i).GetComponent<TextMeshProUGUI>();
        }
    }

    private void Update()
    {
        if (lastElement == Mage.instance.GetSelectedElement())
            return;

        lastElement = Mage.instance.GetSelectedElement();

        elementName.text = Mage.instance.GetSelectedElement().ToString().ToLower().FirstToUpper();

        Dictionary<SpellTargetType, ISpell> spells = Mage.instance.FindSpells(lastElement);

        for (int i = 0; i < targetTypes.Length; i++)
        {
            if (lastElement == ElementType.NONE && targetTypes[i] != SpellTargetType.ENEMY)
            {
                descriptions[i].text = "Moves the player";
                costs[i].text = "";
                continue;
            }

            if (!spells.ContainsKey(targetTypes[i]))
            {
                descriptions[i].text = "No Effect";
                costs[i].text = "";
                continue;
            }

            descriptions[i].text = spells[targetTypes[i]].GetSpellDescription();
            costs[i].text = spells[targetTypes[i]].GetCost().ToString();

            if (i == targetTypes.Length - 1)
                costs[i].text += "\n/" + Mage.instance.GetElementMaxCharge(lastElement);
        }
    }
}
