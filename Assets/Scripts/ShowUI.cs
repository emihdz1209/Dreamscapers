using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUI : MonoBehaviour
{
    [Header("UI Settings")]
    public string textIdentifier; // Unique identifier for the text
    public GameObject uiObject;   // UI element to show/hide
    public bool useTimer = true;  // Toggle between timer or manual control
    public float displayTime = 5f; // Default, only used if useTimer=true

    [Header("Manual Control")]
    public bool hideOnExit = false; // Hide UI when player exits trigger

    void Start()
    {
        // Dynamically find and assign the correct text object if it's not already assigned
        if (uiObject == null && !string.IsNullOrEmpty(textIdentifier))
        {
            uiObject = SceneTextManager.Instance.GetTextByIdentifier(textIdentifier);
        }

        if (uiObject != null)
            uiObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && uiObject != null)
        {
            uiObject.SetActive(true);

            if (useTimer)
                StartCoroutine(HideAfterDelay());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (hideOnExit && other.CompareTag("Player") && uiObject != null)
        {
            uiObject.SetActive(false);
        }
    }

    IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(displayTime);
        if (uiObject != null)
            uiObject.SetActive(false);
    }
    public void ManualHideUI()
    {
        if (uiObject != null)
            uiObject.SetActive(false);
    }
}
