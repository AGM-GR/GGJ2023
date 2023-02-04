using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaversSpawner : MonoBehaviour
{
    public Raver _raverPrefab;
    public int _initializeRaverCount = 20;
    public float _spawnRadio = 4f;

    private List<Raver> _ravers = new List<Raver>();

    private void Awake()
    {
        for (int i=0; i < _initializeRaverCount; i++)
        {
            Raver raver = GetRaver();
            raver.SetDestination(new Vector3(
                transform.position.x + Random.Range(-_spawnRadio/2f, _spawnRadio/2f), 
                transform.position.y, 
                transform.position.z + Random.Range(-_spawnRadio / 2f, _spawnRadio / 2f)));
        }
    }

    public Raver GetRaver()
    {
        foreach (Raver raver in _ravers)
        {
            if (!raver.gameObject.activeSelf)
            {
                return raver;
            }
        }

        Raver newRaver = Instantiate(_raverPrefab, transform);
        _ravers.Add(newRaver);

        return newRaver;
    }

}
