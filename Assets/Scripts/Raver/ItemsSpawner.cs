using System.Collections;
using UnityEngine;

public class ItemsSpawner : NavMeshSpawner<Item>
{
    [Header("Items Spawner")]
    public int _initializeItemCount = 1;
    public float _spawnRatio = 0.5f;

    private IEnumerator Start()
    {
        for (int i = 0; i < _initializeItemCount; i++)
        {
            SpawnRandom();

            yield return new WaitForSeconds(_spawnRatio);
        }
    }
}
