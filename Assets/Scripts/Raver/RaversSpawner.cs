using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class RaversSpawner : NavMeshSpawner<Raver>
{
    [Header("Ravers Spawner")]
    public int _initializeRaverCount = 20;
    public float _spawnRatio = 0.1f;

    private IEnumerator Start()
    {
        for (int i=0; i < _initializeRaverCount; i++)
        {
            SpawnRandom();

            yield return new WaitForSeconds(_spawnRatio);
        }
    }

}
