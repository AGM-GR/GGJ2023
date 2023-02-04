using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Raver : RaverBase
{
    [SerializeField]
    private NavMeshAgent _navMeshAgent;
    [SerializeField]
    private RaverMaterials _raverMaterials;

    public NavMeshAgent NavMeshAgent => _navMeshAgent;

    private void OnValidate()
    {
        if (_navMeshAgent == null)
            _navMeshAgent = GetComponentInChildren<NavMeshAgent>();

        if (_raverMaterials == null)
            _raverMaterials = GetComponentInChildren<RaverMaterials>();
    }

    public override void SetDestination(Vector3 destination)
    {
        _navMeshAgent.SetDestination(destination);
    }

    public override void EnableRaver()
    {
        gameObject.SetActive(true);
        _navMeshAgent.enabled = true;
        _raverSpawner.RaverEnabled();
    }

    public override void DisableRaver()
    {
        _raverSpawner.RaverDisabled();
        _navMeshAgent.enabled = false;
        gameObject.SetActive(false);
    }

    public override void InfluencedByPlayer(CarColor raveColor, Vector3 destination)
    {
        base.InfluencedByPlayer(raveColor, destination);
        SetPlayerMaterial(raveColor);
        SetDestination(destination);
    }

    public void SetRaverMaterial()
    {
        _raverMaterials.SetRaverMaterial();
    }

    public void SetPlayerMaterial(CarColor raveColor)
    {
        _raverMaterials.SetPlayerMaterial((int)raveColor);
    }
}
