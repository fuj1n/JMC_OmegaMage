using UnityEngine;

[EntityFactory('f', 'e', 'a', 'w')]
public class EntityUnlockElement : MonoBehaviour, IEntity
{
    public Vector3 rotateSpeed;

    public AudioClip pickupSound;

    private ElementType element;
    private Transform effect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mage"))
        {
            Mage.instance.UnlockElement(element);
            if (pickupSound)
                AudioSource.PlayClipAtPoint(pickupSound, Camera.main.transform.position, Mage.instance.masterVolume);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        effect.localRotation *= Quaternion.Euler(rotateSpeed * Time.deltaTime);
    }

    [EntityFactoryCallback]
    public void OnSpawned(char key)
    {
        switch (key)
        {
            case 'f':
                element = ElementType.FIRE;
                break;
            case 'e':
                element = ElementType.EARTH;
                break;
            case 'a':
                element = ElementType.AIR;
                break;
            case 'w':
                element = ElementType.WATER;
                break;
        }

        if (Mage.instance.IsUnlockedElement(element))
            Destroy(gameObject);

        effect = transform.GetChild(0);

        effect.GetComponent<SpriteRenderer>().sprite = Mage.instance.elementSprites[(int)element];
        effect.GetComponent<Light>().color = Mage.instance.elementColors[(int)element];
    }
}
