// SaveSystem consolidado
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;
    private const string SaveKey = "SavedScene";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void SaveGame(int sceneIndex)
    {
        PlayerPrefs.SetInt(SaveKey, sceneIndex);
        PlayerPrefs.Save();
    }

    public static int? LoadGame()
    {
        return PlayerPrefs.HasKey(SaveKey) ? PlayerPrefs.GetInt(SaveKey) : null;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Lista de escenas que NO deben guardarse
        string[] excludedScenes = { "MainMenu", "GameOver" };

        // Si la escena actual NO está en la lista excluida, guárdala
        if (!System.Array.Exists(excludedScenes, name => name == scene.name))
        {
            SaveGame(scene.buildIndex);
            Debug.Log($"Scene {scene.name} automatically saved.");
        }
        else
        {
            Debug.Log($"Scene {scene.name} excluded from save.");
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}