using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; // Required for AudioMixer
using UnityEngine.UI;    // Required for Slider
using TMPro;
using UnityEngine.SceneManagement; // Required for scene management

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance { get; private set; } // Singleton instance

    [Header("Sensitivity Settings")]
    public Slider sensitivitySlider;
    public PlayerCam playerCam; // Assign in Inspector
    public TextMeshProUGUI sensitivityText; // Optional label

    [Header("UI Elements")]
    public GameObject pauseMenuUI; // Assign in Inspector
    public GameObject doorEnterText; // Assign in Inspector (scene-specific)

    [Header("Audio Settings")]
    public AudioSource backgroundMusic; // Assign in Inspector
    public AudioMixer masterMixer; // Assign in Inspector
    public Slider volumeSlider; // Assign in Inspector

    private bool isPaused = false;

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate Canvas
            return;
        }

        Instance = this;
        DontDestroyOnLoad(pauseMenuUI.transform.root.gameObject); // Persist Canvas across scenes
    }

    private void Start()
    {
        // Load saved volume (default: 0.8f if no save exists)
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 0.8f);
        volumeSlider.value = savedVolume;
        SetVolume(savedVolume);

        // Load saved sensitivity (default: 100f)
        float savedSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 100f);
        sensitivitySlider.value = savedSensitivity;
        UpdateSensitivityDisplay(savedSensitivity);

        // Disable scene-specific elements by default
        if (doorEnterText != null)
        {
            doorEnterText.SetActive(false);
        }
    }

    private void Update()
    {
        // Check if the Esc key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void OnSensitivityChanged(float newValue)
    {
        // Update player camera sensitivity
        playerCam.UpdateSensitivity(newValue);

        // Save the new sensitivity value
        PlayerPrefs.SetFloat("MouseSensitivity", newValue);

        // Update the sensitivity display text
        UpdateSensitivityDisplay(newValue);
    }

    private void UpdateSensitivityDisplay(float value)
    {
        if (sensitivityText != null)
            sensitivityText.text = $"Sensitivity: {value:F0}"; // F0 = no decimals
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Unfreeze game
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor (for FPS games)
        Cursor.visible = false;
        isPaused = false;

        // Resume music
        if (backgroundMusic != null)
            backgroundMusic.UnPause();
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Freeze game
        Cursor.lockState = CursorLockMode.None; // Unlock cursor
        Cursor.visible = true;
        isPaused = true;

        // Pause music
        if (backgroundMusic != null)
            backgroundMusic.Pause();
    }

    public void SetVolume(float volume)
    {
        // Convert linear slider (0-1) to logarithmic dB scale (-80 to 0)
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);

        // Save the setting
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void ShowDoorEnterText()
    {
        if (doorEnterText != null)
        {
            doorEnterText.SetActive(true);
        }
    }

    public void HideDoorEnterText()
    {
        if (doorEnterText != null)
        {
            doorEnterText.SetActive(false);
        }
    }

    public void RestartScene()
{
    //hide pause menu UI manually
    pauseMenuUI.SetActive(false);

    //unpause the game first (in case the player restarts while paused)
    Time.timeScale = 1f;

    //reload the currently active scene
    Scene currentScene = SceneManager.GetActiveScene();
    SceneManager.LoadScene(currentScene.buildIndex);
}
}