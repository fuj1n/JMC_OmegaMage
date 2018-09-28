public interface ISpell
{
    void Cast(ISpellParams parameters);
    ElementType GetElement();
    int GetCost();
    string GetTargetType();
}

public interface ISpellParams { }
