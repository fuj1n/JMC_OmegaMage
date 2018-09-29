using UnityEngine;

[EntityFactory('k')]
public class EntityKey : MonoBehaviour, IEntity
{
    private int id;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mage"))
        {
            PortalBoss.KeyCollected(id);
            Destroy(gameObject);
        }
    }

    [EntityFactoryCallback]
    public void OnSpawned(char key, int id)
    {
        this.id = id;
        if (PortalBoss.IsKeyCollected(id))
            Destroy(gameObject); // Destoy key if it has already been collected
    }
}
