using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject blackScreen; // Assign the black screen GameObject here

    void Start()
    {
        if (blackScreen != null)
        {
            blackScreen.SetActive(false); //hide it manually
        }
    }

    public void NewGame()
    {
        // Enable black screen instantly
        blackScreen.SetActive(true);

        // Now load the scene
        SceneManager.LoadSceneAsync(1);
    }

    public void ContinueGame()
    {
        blackScreen.SetActive(true);

        int? savedScene = SaveSystem.LoadGame();
        if (savedScene != null)
        {
            SceneManager.LoadSceneAsync(savedScene.Value);
        }
    }
}