using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    [Tooltip("Drag the scene to load from the Build Settings")]
    public int sceneBuildIndex; //for inspector

    [Header("Trigger Settings")]
    public bool requireKeyPress = false;
    public KeyCode interactionKey = KeyCode.E;
    private bool playerInTrigger = false;

    void Update()
    {
        //handle key press (if needed)
        if (playerInTrigger && requireKeyPress && Input.GetKeyDown(interactionKey))
        {
            LoadScene();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
            
            //auto-load if no key press required
            if (!requireKeyPress)
                LoadScene();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInTrigger = false;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneBuildIndex);
    }
}
