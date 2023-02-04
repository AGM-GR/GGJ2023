using System.Collections;
using UnityEngine;

public class ItemsSpawner : NavMeshSpawner<Item>
{
    [Header("Items Spawner")]
    public int _maxItemsCount = 100;
    public int _itemsSpawnBatch = 10;
    public float _spawnRatio = 0.1f;
    private int _totalActiveItems = 0;


    private IEnumerator Start()
    {
        while (true)
        {
            if (_totalActiveItems < _maxItemsCount)
            {
                for (int i = 0; i < _itemsSpawnBatch; i++)
                {
                    Debug.Log("Spawn");
                    Item raverSpawned = SpawnRandom();
                    if (raverSpawned != null)
                    {
                        raverSpawned.Spawner = this;
                        yield return new WaitForSeconds(_spawnRatio);
                        raverSpawned.gameObject.SetActive(true);
                        _totalActiveItems++;
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

    public void ItemDisabled()
    {
        _totalActiveItems--;
    }
}
