using SFB;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void ChangeScenes(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ExitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void LoadOriginalLevel()
    {
        ResetLevel();
        LayoutTiles.loadOriginal = true;
    }

    public void ResetLevel()
    {
        LayoutTiles.loadOriginal = false;
        LayoutTiles.loadPath = "";
    }

    public void CustomLevel()
    {
        string selection = StandaloneFileBrowser.OpenFilePanel("Load rooms file", @"C:\", "json", false).SingleOrDefault();

        if (!string.IsNullOrWhiteSpace(selection) && File.Exists(selection))
        {
            ResetLevel();
            LayoutTiles.loadPath = selection;
        }
    }
}
