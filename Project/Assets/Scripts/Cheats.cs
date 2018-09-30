using UnityEngine;

public class Cheats : MonoBehaviour
{
    public SpellSelfInvincible invincibilitySpell;

    private SpellSelfInvincible invulnerability;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (invulnerability)
                Destroy(invulnerability.gameObject);
            else
            {
                invulnerability = Instantiate(invincibilitySpell, Mage.instance.transform, false).GetComponent<SpellSelfInvincible>();
                invulnerability.fadeTime = 0F;
            }
        }
    }
}
