using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasSceneUIManager : MonoBehaviour
{
    [Header("Scene specific UI")]
    public GameObject scene1UI;
    public GameObject scene2UI;
    public GameObject scene3UI;

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
        if (scene1UI != null) scene1UI.SetActive(scene.buildIndex == 1); // or use scene.name == "Scene1"
        if (scene2UI != null) scene2UI.SetActive(scene.buildIndex == 2);
        if (scene3UI != null) scene3UI.SetActive(scene.buildIndex == 3);
    }
}
