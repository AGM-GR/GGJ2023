using System.Collections;
using UnityEngine;

public class RaversSpawner : NavMeshSpawner<Raver>
{
    [Header("Ravers Spawner")]
    public int _initializeRaverCount = 20;
    public float _spawnRatio = 0.1f;
    public Renderer[] exitAreas;

    private IEnumerator Start()
    {
        for (int i=0; i < _initializeRaverCount; i++)
        {
            Raver raverSpawned = SpawnRandom();
            if (raverSpawned != null)
            {
                yield return new WaitForSeconds(_spawnRatio);
                Vector3 exitPoint = Utils.GetRandomPointInPlane(exitAreas[Random.Range(0, exitAreas.Length)]);
                raverSpawned.SetDestination(exitPoint);
            }

            yield return null;
        }
    }

}
