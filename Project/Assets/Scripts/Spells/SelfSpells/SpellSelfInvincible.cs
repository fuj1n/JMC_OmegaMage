public class SpellSelfInvincible : SpellSelfBase
{
    private void Awake()
    {
        Mage.instance.SetInvincible(transform);
    }
}
