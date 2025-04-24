using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasInitializer : MonoBehaviour
{
    public GameObject canvasPrefab; // Assign in Inspector (this should be the gameplay canvas prefab)
    public string[] excludedScenes = { "MainMenu", "GameOver" };

    void Awake()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        // If the scene is excluded, do nothing
        if (System.Array.Exists(excludedScenes, scene => scene == currentScene.name))
            return;

        // If a canvas doesn't already exist, create one
        if (PauseMenu.Instance == null)
        {
            GameObject canvasInstance = Instantiate(canvasPrefab);
            DontDestroyOnLoad(canvasInstance);
        }

        Debug.Log($"CanvasInitializer: Loaded in scene {currentScene.name}");
    }
}

