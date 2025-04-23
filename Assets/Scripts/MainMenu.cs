using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject blackScreen; // Assign the black screen GameObject here

    public void NewGame()
    {
        // Enable black screen instantly
        blackScreen.SetActive(true);

        // Now load the scene
        SceneManager.LoadSceneAsync(1);
    }
}