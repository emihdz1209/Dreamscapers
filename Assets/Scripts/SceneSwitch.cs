using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    [Tooltip("Drag the scene to load from the Build Settings")]
    public int sceneBuildIndex; //for inspector

    [Header("Trigger Mode")]
    public bool requireKeyPress = false;
    public KeyCode interactionKey = KeyCode.E;
    
    [Header("Timer Mode (Overrides Key Press)")]
    public bool useTimer = false;
    public float delayBeforeLoad = 3f;
    
    private bool playerInTrigger = false;
    private float timer = 0f;

    void Update()
    {
        //timer-based loading
        if (playerInTrigger && useTimer)
        {
            timer += Time.deltaTime;
            if (timer >= delayBeforeLoad)
            {
                LoadScene();
            }
        }
        //key press loading
        else if (playerInTrigger && requireKeyPress && Input.GetKeyDown(interactionKey))
        {
            LoadScene();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
            timer = 0f; //reset timer on enter
            
            //immediate load if no special mode
            if (!requireKeyPress && !useTimer)
            {
                LoadScene();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
            timer = 0f; //reset timer on exit
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneBuildIndex);
    }
}
