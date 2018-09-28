public abstract class SpellSelfBase : SpellBase
{
    public override string GetTargetType() => "Self";
}

public struct SpellSelfParams : ISpellParams
{
    public static SpellSelfParams self = new SpellSelfParams();
}