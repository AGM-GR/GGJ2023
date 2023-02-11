using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaversGroup : RaverBase
{
    [SerializeField]
    private Raver[] _raversInGroup;
    [SerializeField]
    private float _raversGroupRadio = 1;

    private Vector3[] _raversOffsetInGroup;

    public int AmountOfRavers { get { return _raversInGroup.Length; } }

    private void Awake()
    {
        _raversOffsetInGroup = new Vector3[_raversInGroup.Length];
        foreach (Raver raver in _raversInGroup)
        {
            raver.RaversGroup = this;
        }
    }

    public override void SetSpawner(RaversSpawner spawner)
    {
        base.SetSpawner(spawner);
        foreach(Raver raver in _raversInGroup)
        {
            raver.SetSpawner(spawner);
        }
    }

    public override void SetDestination(Vector3 destination)
    {
        for (int i = 0; i < _raversInGroup.Length; i++)
        {
            Vector3 raverDestinationInGroup = destination + _raversOffsetInGroup[i];
            _raversInGroup[i].SetDestination(raverDestinationInGroup);
        }
    }

    public override void EnableRaver()
    {
        base.EnableRaver();
        gameObject.SetActive(true);

        for (int i=0; i < _raversInGroup.Length; i++)
        {
            Vector2 raverPointInGroup = Random.insideUnitCircle * _raversGroupRadio;
            Vector3 raverPositionInGroup = transform.position + new Vector3(raverPointInGroup.x, 0, raverPointInGroup.y);
            _raverSpawner.GetNavMeshPoint(ref raverPositionInGroup);

            _raversInGroup[i].transform.position = raverPositionInGroup;
            _raversOffsetInGroup[i] = _raversInGroup[i].transform.localPosition;
            _raversInGroup[i].EnableRaver();
        }
    }

    public override void DisableRaver()
    {
        base.DisableRaver();

        foreach (Raver raver in _raversInGroup)
        {
            raver.DisableRaver();
        }

        gameObject.SetActive(false);
    }

    public override void InfluencedByPlayer(CarColor carColor, Car influencingCar, bool withRoot = false)
    {
        base.InfluencedByPlayer(carColor, influencingCar, withRoot);
        foreach (Raver raver in _raversInGroup)
        {
            //raver.SetPlayerMaterial(carColor);
            raver.InfluenceCircle(carColor);
        }
        SetDestination(influencingCar.PointsExit);
        ChangeSpeedMultiplier(influencingCar.GetSpeedMultiplierByInfluence());
    }

    public override void ChangeSpeedMultiplier(float speedMultiplier)
    {
        foreach (Raver raver in _raversInGroup)
        {
            raver.ChangeSpeedMultiplier(speedMultiplier);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        CustomGizmos.DrawWireCircle(transform.position, transform.up, _raversGroupRadio);
    }
}
