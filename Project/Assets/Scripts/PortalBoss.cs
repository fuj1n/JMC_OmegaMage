using System.Collections.Generic;
using UnityEngine;

public class PortalBoss : Portal
{
    private static HashSet<int> keysCollected = new HashSet<int>();
    private static bool isRegistered = false;

    public Texture2D[] keyTextures = { };

    private new Renderer renderer;
    private Material material;
    private new Collider collider;

    private Vector3 startPos;

    private void Awake()
    {
        renderer = GetComponentInChildren<Renderer>();
        material = renderer.material;
        collider = GetComponent<Collider>();

        startPos = renderer.transform.position;

        UpdateState();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            KeyCollected(Random.Range(0, 99999999));
    }

    protected override void OnPortalEntered()
    {
        if (keysCollected.Count >= LayoutTiles.KEY_COUNT)
            base.OnPortalEntered();
    }

    private void UpdateState()
    {
        if (keyTextures.Length > keysCollected.Count)
            material.mainTexture = keyTextures[keysCollected.Count];

        collider.isTrigger = keysCollected.Count >= LayoutTiles.KEY_COUNT;

        if (keysCollected.Count >= LayoutTiles.KEY_COUNT)
            renderer.transform.position = startPos + Vector3.forward * 0.9F;
    }

    public static bool IsKeyCollected(int id)
    {
        return keysCollected.Contains(id);
    }

    public static void KeyCollected(int id)
    {
        if (!isRegistered)
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += (s, m) =>
            {
                // Clear collected keys on scene reload
                if (m == UnityEngine.SceneManagement.LoadSceneMode.Single)
                    keysCollected.Clear();
            };
        }

        keysCollected.Add(id);

        PortalBoss boss = FindObjectOfType<PortalBoss>();

        if (boss)
            boss.UpdateState();

        EffectDoor.instance.Simulate(keysCollected.Count);
    }
}
