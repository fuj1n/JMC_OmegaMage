public abstract class SpellSelfBase : SpellBase
{
    public override SpellTargetType GetTargetType() => SpellTargetType.SELF;
}

public struct SpellSelfParams : ISpellParams
{
    public static SpellSelfParams self = new SpellSelfParams();
}