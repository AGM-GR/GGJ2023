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

    private Vector3 _finalDestination;

    public NavMeshAgent NavMeshAgent => _navMeshAgent;
    public RaversSpawner RaverSpawner { get; set;}

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

    public void SetExitDestination(Vector3 destination)
    {
        _finalDestination = destination;
    }

    public void StartRaverLogic()
    {
        gameObject.SetActive(true);
        _navMeshAgent.enabled = true;
        StartCoroutine(RaverLogic());
    }

    private IEnumerator RaverLogic()
    {
        yield return new WaitForSeconds(Random.Range(0.2f, 1f));
        SetDestination(_finalDestination);
    }

    public void DisableRaver()
    {
        RaverSpawner.RaverDisabled();
        _navMeshAgent.enabled = false;
        gameObject.SetActive(false);
    }

    public void InfluencedByPlayer(RaveColor raveColor, Vector3 destination)
    {
        _raverMaterials.SetPlayerMaterial((int)raveColor);
        _navMeshAgent.SetDestination(destination);
    }
}
