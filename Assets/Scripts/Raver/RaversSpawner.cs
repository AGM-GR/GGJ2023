using System.Collections;
using UnityEngine;

public class RaversSpawner : NavMeshSpawner<RaverBase>
{
    [Header("Ravers Spawner")]
    public int _maxRaverCount = 100;
    public int _raversSpawnBatch = 10;
    public float _spawnRatio = 0.1f;
    public Renderer[] _exitAreas;

    private int _totalActiveRavers = 0;

    private IEnumerator Start()
    {
        while(true)
        {
            if (_totalActiveRavers < _maxRaverCount)
            {
                for (int i = 0; i < _raversSpawnBatch; i++)
                {
                    RaverBase raverSpawned = SpawnRandom();
                    if (raverSpawned != null)
                    {
                        yield return new WaitForSeconds(_spawnRatio);
                        Vector3 exitPoint = Utils.GetRandomPointInPlane(_exitAreas[Random.Range(0, _exitAreas.Length)]);

                        raverSpawned.SetSpawner(this);
                        raverSpawned.SetExitDestination(exitPoint);
                        raverSpawned.StartRaverLogic();
                    }
                }

                yield return new WaitForSeconds(_spawnRatio);
            } 
            else
            {
                yield return null;
            }
        }
    }

    public void RaverEnabled()
    {
        _totalActiveRavers++;
    }

    public void RaverDisabled()
    {
        _totalActiveRavers--;
    }
}
