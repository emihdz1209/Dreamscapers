using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUI : MonoBehaviour
{
    [Header("UI Settings")]
    public GameObject uiObject;  //UI element to show/hide
    public bool useTimer = true; //toggle between timer or manual control
    public float displayTime = 5f; //default, only used if useTimer=true

    [Header("Manual Control")]
    public bool hideOnExit = false; //hide UI when player exits trigger

    void Start()
    {
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

    //call this from other scripts to manually hide the UI
    public void ManualHideUI()
    {
        if (uiObject != null)
            uiObject.SetActive(false);
    }
}
