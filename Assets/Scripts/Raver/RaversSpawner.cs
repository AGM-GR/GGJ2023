using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaversSpawner : MonoBehaviour
{
    public Raver _raverPrefab;
    public int _initializeRaverCount = 20;
    public float _spawnRadio = 4f;
    public float _spawnRatio = 0.1f;

    private List<Raver> _ravers = new List<Raver>();

    private IEnumerator Start()
    {
        for (int i=0; i < _initializeRaverCount; i++)
        {
            Raver raver = GetRaver();
            Vector2 randomPointInCircle = Random.insideUnitCircle * _spawnRadio;
            raver.SetDestination(new Vector3(randomPointInCircle.x, transform.position.y, randomPointInCircle.y));

            yield return new WaitForSeconds(_spawnRatio);
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
