using DG.Tweening;
using UnityEngine;

public class EffectDoor : MonoBehaviour
{
    public static EffectDoor instance;

    public Camera simulationCamera;
    public Canvas gameUI;

    public Renderer background;
    public Renderer foreground;

    public AudioClip collectedSound;
    public AudioClip allCollectedSound;

    public ParticleSystem particles;

    public Texture2D[] textures = { };

    [HideInInspector]
    public GameObject worldAnchor;

    private void Awake()
    {
        instance = this;

        background.material.SetInt("_ZWrite", 1);
        foreground.material.SetInt("_ZWrite", 1);
    }

    public void Simulate(int keysCollected)
    {
        if (keysCollected > LayoutTiles.KEY_COUNT)
            return;

        if (textures == null || textures.Length == 0)
            return;

        Mage.instance.ClearInput();
        Mage.instance.StopWalking();

        Mage.instance.gameObject.SetActive(false);
        worldAnchor.SetActive(false);
        gameUI.gameObject.SetActive(false);

        simulationCamera.gameObject.SetActive(true);

        foreground.material.mainTexture = textures[Mathf.Max(keysCollected - 1, 0)];
        background.material.mainTexture = textures[Mathf.Min(textures.Length - 1, keysCollected)];
        foreground.material.color = Color.white;
        foreground.material.DOFade(0F, 2F).OnComplete(() =>
        {
            AudioClip clip = keysCollected >= LayoutTiles.KEY_COUNT ? allCollectedSound : collectedSound;
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, 0.5F);

            if (keysCollected < LayoutTiles.KEY_COUNT)
            {
                DOTween.To(() => 1, x => { }, 0F, clip.length).OnComplete(() =>
                {
                    Mage.instance.gameObject.SetActive(true);
                    worldAnchor.SetActive(true);
                    simulationCamera.gameObject.SetActive(false);
                    gameUI.gameObject.SetActive(true);
                });
            }
            else
            {
                if (particles)
                {
                    particles.Play();
                    DOTween.To(() => 1, x => { }, 0F, clip.length * 0.6F).OnComplete(() => particles.Stop());
                }
                background.transform.DOMove(background.transform.position + Vector3.down, clip.length).OnComplete(() =>
                {
                    Mage.instance.gameObject.SetActive(true);
                    worldAnchor.SetActive(true);
                    simulationCamera.gameObject.SetActive(false);
                    gameUI.gameObject.SetActive(true);
                });
            }
        });
    }
}
