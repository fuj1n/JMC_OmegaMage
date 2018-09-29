public interface ISpell
{
    bool Cast(ISpellParams parameters);
    ElementType GetElement();
    int GetCost();
    SpellTargetType GetTargetType();

    string GetSpellDescription();
}

public interface ISpellParams { }
