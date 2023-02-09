using System.Collections;
using UnityEngine;

public class ItemsSpawner : NavMeshSpawner<Item>
{
    public static bool EnergyDrinkSpawned = false;
    
    [Header("Items Spawner")]
    public int _maxItemsCount = 100;
    public int _itemsSpawnBatch = 10;
    public float _spawnRatio = 0.1f;
    public float nextBatchRatio = 2f;
    public bool onlyOneDrink = true;
    private int _totalActiveItems = 0;

    public AudioSource aSource;
    public AudioClip spawnSfx;


    private IEnumerator Start()
    {
        EnergyDrinkSpawned = false;

        while (true)
        {
            if (_totalActiveItems < _maxItemsCount)
            {
                for (int i = 0; i < _itemsSpawnBatch; i++)
                {
                    Item itemSpawned = SpawnRandom();
                    while (itemSpawned != null && onlyOneDrink && itemSpawned.Data.Type == ItemType.EnergyDrink && EnergyDrinkSpawned) 
                    {
                        itemSpawned.gameObject.SetActive(false);
                        itemSpawned = SpawnRandom();
                    }

                    if (itemSpawned != null)
                    {
                        if (itemSpawned.Data.Type == ItemType.EnergyDrink)
                        {
                            EnergyDrinkSpawned = true;
                        }

                        itemSpawned.GetComponent<Animator>().SetFloat("Beat", MusicController.Instance.BeatMultiplier);
                        aSource.PlayOneShot(spawnSfx);
                        itemSpawned.Spawner = this;
                        yield return new WaitForSeconds(_spawnRatio);
                        itemSpawned.gameObject.SetActive(true);
                        _totalActiveItems++;
                    }
                }

                yield return new WaitForSeconds(nextBatchRatio);
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
