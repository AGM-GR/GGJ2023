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
    internal Car _currentInfluencingCar;

    internal RaverState _currentState;

    [SerializeField]
    private AudioSource aSource;

    [SerializeField]
    private List<AudioClip> clips;

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

    public virtual void EnableRaver()
    {
        _currentState = RaverState.IDLE;
    }

    public virtual void DisableRaver()
    {
        // Disconnec the old car
        if (_currentInfluencingCar != null)
            _currentInfluencingCar.onInfluenceChanged -= ChangeSpeedMultiplier;
        _currentInfluencingCar = null;
    }

    public virtual void InfluencedByPlayer(CarColor raveColor, Car influencingCar)
    {
        _currentState = RaverState.INFLUENCED;

        // Disconnec the old car
        if (_currentInfluencingCar != null)
            _currentInfluencingCar.onInfluenceChanged -= ChangeSpeedMultiplier;

        // Connec to new car
        _currentInfluencingCar = influencingCar;
        _currentInfluencingCar.onInfluenceChanged += ChangeSpeedMultiplier;

        PlaySfx(influencingCar);
    }

    private void PlaySfx(Car influencingCar)
    {
        if (influencingCar != _currentInfluencingCar)
        {
            AudioClip clip = clips.GetRandomElement();
            aSource.PlayOneShot(clip);
        }
    }


    public abstract void ChangeSpeedMultiplier(float speedMultiplier);

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
            yield return new WaitForSeconds(Random.Range(0.8f, 1.4f));

            if (_currentState == RaverState.IDLE)
            {
                SetDestination(_finalDestination);
                _currentState = RaverState.EXITING;
            }
        }
    }
}
