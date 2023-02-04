using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RaverBase : MonoBehaviour
{
    internal enum RaverState 
    {
        IDLE,
        EXITING,
        INFLUENCED,
    }

    internal Vector3 _finalDestination;
    internal RaversSpawner _raverSpawner;

    internal RaverState _currentState;

    public RaversGroup RaversGroup { get; internal set; }

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

    public virtual void InfluencedByPlayer(CarColor raveColor, Vector3 destination)
    {
        _currentState = RaverState.INFLUENCED;
    }

    public virtual void StartRaverLogic()
    {
        EnableRaver();
        StartCoroutine(RaverLogic());
    }

    protected virtual IEnumerator RaverLogic()
    {
        _currentState = RaverState.IDLE;

        if (_currentState == RaverState.IDLE)
        {
            yield return new WaitForSeconds(Random.Range(0.2f, 1f));

            if (_currentState == RaverState.IDLE)
            {
                SetDestination(_finalDestination);
                _currentState = RaverState.EXITING;
            }
        }
    }
}
