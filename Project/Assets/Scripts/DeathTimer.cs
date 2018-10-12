using UnityEngine;

public class DeathTimer : MonoBehaviour
{
    public float time;

    private void Awake()
    {
        Invoke("Die", time);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
