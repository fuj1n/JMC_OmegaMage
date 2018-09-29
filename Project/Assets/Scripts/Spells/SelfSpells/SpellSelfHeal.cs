public class SpellSelfHeal : SpellSelfBase
{
    public float healAmount;

    public override bool Cast(ISpellParams parameters)
    {
        if (Mage.instance.GetHealth() >= 1F)
            return false;

        Mage.instance.Heal(healAmount);

        return base.Cast(parameters);
    }
}
