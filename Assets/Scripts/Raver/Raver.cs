using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Raver : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent _navMeshAgent;
    [SerializeField]
    private RaverMaterials _raverMaterials;

    private void OnValidate()
    {
        if (_navMeshAgent == null)
            _navMeshAgent = GetComponentInChildren<NavMeshAgent>();

        if (_raverMaterials == null)
            _raverMaterials = GetComponentInChildren<RaverMaterials>();
    }

    public void SetDestination(Vector3 destination)
    {
        _navMeshAgent.SetDestination(destination);
    }

    public void InfluencedByPlayer(RaveColor raveColor, Vector3 destination)
    {
        _raverMaterials.SetPlayerMaterial((int)raveColor);
        _navMeshAgent.SetDestination(destination);
    }
}
