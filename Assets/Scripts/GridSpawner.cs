using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;  // Assign your object in the inspector
    public int columns = 5;           // Number of objects along X-axis
    public int rows = 5;              // Number of objects along Y-axis
    public float spacing = 2.0f;      // Spacing between objects

    void Start()
    {
        SpawnGrid();
    }

    void SpawnGrid()
    {
        if (objectToSpawn == null)
        {
            Debug.LogError("No object assigned to spawn!");
            return;
        }

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                Vector3 spawnPosition = new Vector3(x * spacing, y * spacing, 0);
                Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
            }
        }
    }
}
