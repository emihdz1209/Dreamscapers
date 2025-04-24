using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneUIActivator : MonoBehaviour
{
    [Tooltip("Names of UI GameObjects to activate based on the loaded scene.")]
    public List<SceneUISet> sceneUISets = new();

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
        foreach (var set in sceneUISets)
        {
            bool shouldActivate = set.sceneName == scene.name;
            foreach (var uiName in set.uiObjectNames)
            {
                GameObject obj = GameObject.Find(uiName);
                if (obj != null)
                    obj.SetActive(shouldActivate);
            }
        }
    }

    [System.Serializable]
    public class SceneUISet
    {
        public string sceneName;
        public List<string> uiObjectNames;
    }
}

