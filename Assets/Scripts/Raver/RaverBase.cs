using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RaverBase : MonoBehaviour
{
    internal Vector3 _finalDestination;
    internal RaversSpawner _raverSpawner;

    public virtual void SetSpawner(RaversSpawner spawner)
    {
        _raverSpawner = spawner;
    }

    public abstract void SetDestination(Vector3 destination);

    public virtual void SetExitDestination(Vector3 destination)
    {
        _finalDestination = destination;
    }

    public abstract void EnableRaver();

    public abstract void DisableRaver();

    public abstract void InfluencedByPlayer(RaveColor raveColor, Vector3 destination);

    public virtual void StartRaverLogic()
    {
        EnableRaver();
        StartCoroutine(RaverLogic());
    }

    protected virtual IEnumerator RaverLogic()
    {
        yield return new WaitForSeconds(Random.Range(0.2f, 1f));
        SetDestination(_finalDestination);
    }
}
