using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [Header("UI Reference")]
    public GameObject gameOverScreen;

    [Header("Collision Settings")]
    public string deadlyObjectTag = "Enemy"; // Added missing declaration

    [Header("Audio Control")]
    public AudioSource[] audioSourcesToStop;
    public bool stopAllAudio = true;

    void ShowGameOver()
    {
        // Freeze game and show UI
        Time.timeScale = 0f;
        if (gameOverScreen != null) 
            gameOverScreen.SetActive(true);
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        StopAllAudio();
    }

    void StopAllAudio()
    {
        // Stop assigned audio sources
        foreach (AudioSource source in audioSourcesToStop)
        {
            if (source != null && source.isPlaying)
                source.Stop();
        }

        // Stop all audio if enabled
        if (stopAllAudio)
        {
            AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
            foreach (AudioSource source in allAudioSources)
            {
                if (source != null && source.isPlaying)
                    source.Stop();
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(deadlyObjectTag))
        {
            ShowGameOver();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(deadlyObjectTag))
        {
            ShowGameOver();
        }
    }
}