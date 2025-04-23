using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTextManager : MonoBehaviour
{
    public static SceneTextManager Instance { get; private set; }

    public GameObject[] sceneTexts; // Assign scene-specific texts in the Inspector
    private Dictionary<string, GameObject> textDictionary;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeTextDictionary();
    }

    private void InitializeTextDictionary()
    {
        textDictionary = new Dictionary<string, GameObject>();

        foreach (GameObject text in sceneTexts)
        {
            if (text != null)
            {
                textDictionary[text.name] = text; // Use the GameObject's name as the identifier
            }
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        DeactivateAllTexts();
    }

    private void DeactivateAllTexts()
    {
        foreach (GameObject text in sceneTexts)
        {
            if (text != null)
                text.SetActive(false);
        }

        ShowUI[] showUIScripts = FindObjectsOfType<ShowUI>();
        foreach (ShowUI showUI in showUIScripts)
        {
            if (showUI.uiObject != null)
                showUI.uiObject.SetActive(false);
        }
    }

    public GameObject GetTextByIdentifier(string identifier)
    {
        if (textDictionary != null && textDictionary.ContainsKey(identifier))
        {
            return textDictionary[identifier];
        }

        return null;
    }
}