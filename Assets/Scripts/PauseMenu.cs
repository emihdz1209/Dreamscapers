using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; //required for AudioMixer
using UnityEngine.UI;    //required for Slider
using TMPro;

//attach this script to the canvas or an empty game object
public class PauseMenu : MonoBehaviour
{
    [Header("Sensitivity Settings")]
    public Slider sensitivitySlider;
    public PlayerCam playerCam; //assign in Inspector
    public TextMeshProUGUI sensitivityText; //optional label

    //assign these in Inspector
    public GameObject pauseMenuUI;
    public AudioSource backgroundMusic;
    public AudioMixer masterMixer;
    public Slider volumeSlider;

    //SOUND PAUSE ALTERNATIVES
    //AudioListener.pause = true; // Pauses all audio in the scene
    //AudioListener.pause = false; // Resumes all audio

    private bool isPaused = false;

    void Start() {
        //load saved volume (default: 0.8f if no save exists)
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 0.8f);
        volumeSlider.value = savedVolume;
        SetVolume(savedVolume);

        //load saved sensitivity (default: 100f)
        float savedSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 100f);
        sensitivitySlider.value = savedSensitivity;
        UpdateSensitivityDisplay(savedSensitivity);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //esc
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void OnSensitivityChanged(float newValue)
    {
        playerCam.UpdateSensitivity(newValue);
        PlayerPrefs.SetFloat("MouseSensitivity", newValue);
        UpdateSensitivityDisplay(newValue);
    }

    void UpdateSensitivityDisplay(float value)
    {
        if (sensitivityText != null)
            sensitivityText.text = $"Sensitivity: {value:F0}"; // F0 = no decimals
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; //unfreeze game
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor (for FPS games)
        Cursor.visible = false;
        isPaused = false;

        //resume music
        if (backgroundMusic != null)
            backgroundMusic.UnPause(); //or backgroundMusic.Play();
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; //freeze game
        Cursor.lockState = CursorLockMode.None; // Unlock cursor
        Cursor.visible = true;
        isPaused = true;

        //pause music
        if (backgroundMusic != null)
            backgroundMusic.Pause();
    }

    public void SetVolume(float volume) {
        //convert linear slider (0-1) to logarithmic dB scale (-80 to 0)
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);

        //save the setting
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    //the following are not used now but could be useful later

    /*public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }*/

    /*public void QuitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // Replace with your menu scene name
    }*/
}