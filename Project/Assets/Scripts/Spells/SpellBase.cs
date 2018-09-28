using UnityEngine;

public abstract class SpellBase : MonoBehaviour, ISpell
{
    public ElementType element;
    public int cost;

    public abstract void Cast(ISpellParams parameters);

    public int GetCost() => cost;
    public ElementType GetElement() => element;

    public abstract string GetTargetType();
}
