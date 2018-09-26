using UnityEngine;

public class FireGroundSpell : MonoBehaviour
{
    public float duration = 4;
    public float durationVariance = 0.5F;
    public float fadeTime = 1F;

    private float startTime;

    private void Start()
    {
        startTime = Time.time;
        duration = Random.Range(duration - durationVariance, duration + durationVariance);
    }

    private void Update()
    {
        float elapsed = Mathf.Clamp01((Time.time - startTime) / duration);

        float fadeAt = 1 - (fadeTime / duration);
        if (elapsed > fadeAt)
        {
            float fadePercent = Mathf.Clamp01((elapsed - fadeAt) / (1 - fadeAt));
            Vector3 position = transform.position;
            position.z = fadePercent * 2;
            transform.position = position;
        }

        if (elapsed >= 1F)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject go = Utils.FindTaggedParent(other.gameObject);
        if (!go)
            go = other.gameObject;

        Utils.tr("Flame hit", go.name);

        //TODO: damage
    }
}
