using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshSpawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnRadio
    {
        public float spawnRadio;
        public float spawnProbability;
    }

    private const int maxSpawnTries = 20;

    [Header("Spawner Data")]
    public T _spawnablePrefab;
    public SpawnRadio[] _spawnRadios;
    public Renderer meshGround;

    internal List<T> _spawnPool = new List<T>();

    protected virtual void OnValidate()
    {
        if (_spawnRadios == null ||_spawnRadios.Length == 0)
        {
            _spawnRadios = new SpawnRadio[1];
            _spawnRadios[0].spawnRadio = 40;
            _spawnRadios[0].spawnProbability = 1f;
        }
    }

    public T SpawnRandom()
    {
        T spawned = GetSpawnable();

        Vector3 spawnPoint;
        bool willSpawn = false;
        int spawnTries = 0;

        do
        {
            spawnPoint = Utils.GetRandomPointInPlane(meshGround);

            float distanceToSpawner = (transform.position - spawnPoint).sqrMagnitude;

            for (int x = _spawnRadios.Length - 1; x >= 0 && !willSpawn; x--)
            {
                if (distanceToSpawner < _spawnRadios[x].spawnRadio * _spawnRadios[x].spawnRadio)
                {
                    if (Random.value <= _spawnRadios[x].spawnProbability)
                    {
                        willSpawn = true;
                    }
                }
            }

            spawnTries++;

        } while (!willSpawn && spawnTries < maxSpawnTries);


        NavMeshHit hit;
        if (NavMesh.SamplePosition(spawnPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            spawned.transform.position = hit.position;
            return spawned;
        }
        else
        {
            spawned.gameObject.SetActive(false);
        }

        return null;
    }

    public T GetSpawnable()
    {
        foreach (T spawnable in _spawnPool)
        {
            if (!spawnable.gameObject.activeSelf)
            {
                return spawnable;
            }
        }

        T newSpawnlable = Instantiate(_spawnablePrefab, transform);
        _spawnPool.Add(newSpawnlable);

        return newSpawnlable;
    }

    private void OnDrawGizmosSelected()
    {
        foreach (SpawnRadio sp in _spawnRadios)
        {
            Gizmos.color = Color.Lerp(Color.red, Color.blue, sp.spawnProbability);
            CustomGizmos.DrawWireCircle(transform.position, transform.up, sp.spawnRadio);
        }
    }
}
