using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Scene Settings")]
    [Tooltip("Select the scene to load when player dies")]
    public int deathSceneBuildIndex; // Set this in the Inspector

    public void Die()
    {
        // Load the specified scene instead of reloading current one
        SceneManager.LoadScene(deathSceneBuildIndex);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("FollowPlayer"))
        {
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FollowPlayer"))
        {
            Die();
        }
    }
}