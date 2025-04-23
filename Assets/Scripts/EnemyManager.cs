using System.Collections;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private Transform _spawnPoint;

    private GameObject _spawnedEnemy;
    private IEnumerator _spawnAndDestroyCoroutine;

    void Start()
    {
        if (_enemyPrefab == null)
        {
            Debug.LogError("ENEMY PREFAB CANNOT BE NULL IN PREFABSPAWNER");
            return;
        }

        if (_spawnPoint == null)
        {
            Debug.LogError("SPAWN POINT CANNOT BE NULL IN PREFABSPAWNER");
            return;
        }

        _spawnAndDestroyCoroutine = SpawnAndDestroyCoroutine();
        StartCoroutine(_spawnAndDestroyCoroutine);
    }

    IEnumerator SpawnAndDestroyCoroutine()
    {
        yield return new WaitForSeconds(10);

        _spawnedEnemy = Instantiate(_enemyPrefab, _spawnPoint.position, Quaternion.identity);
        Debug.Log("Prefab Spawned!");

        yield return new WaitForSeconds(30);

        if (_spawnedEnemy != null)
        {
            Destroy(_spawnedEnemy);
            Debug.Log("Prefab Destroyed!");
        }
    }
}
