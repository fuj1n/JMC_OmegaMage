using UnityEngine;

public abstract class SpellBase : MonoBehaviour, ISpell
{
    public ElementType element;
    public int cost;

    public string description = "";

    public abstract void Cast(ISpellParams parameters);

    public int GetCost() => cost;
    public ElementType GetElement() => element;
    public string GetSpellDescription() => description;

    public abstract SpellTargetType GetTargetType();
}
