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
        INFLUENCED_ROOT
    }

    internal Vector3 _finalDestination;
    internal RaversSpawner _raverSpawner;
    internal Car _currentInfluencingCar;

    internal RaverState _currentState;

    [SerializeField]
    private AudioSource aSource;

    [SerializeField]
    private List<AudioClip> clips;
    [SerializeField]
    private List<AudioClip> cannotInfluenceClips;

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

    public virtual void InfluencedByPlayer(CarColor raveColor, Car influencingCar, bool withRoot = false)
    {
        if (influencingCar == _currentInfluencingCar) return;

        if (_currentState == RaverState.INFLUENCED_ROOT) // Si está influido por la root, lo ignora
        {
            PlayCannotInfluenceSfx();
            return;
        }

        if (influencingCar != _currentInfluencingCar)
            PlayInfluenceSfx();

        // Disconnec the old car
        if (_currentInfluencingCar != null)
            _currentInfluencingCar.onInfluenceChanged -= ChangeSpeedMultiplier;


        // Connec to new car
        _currentInfluencingCar = influencingCar;
        _currentInfluencingCar.onInfluenceChanged += ChangeSpeedMultiplier;

    }

    private void PlayCannotInfluenceSfx()
    {
        // Algo tipo: "Nah", "booh", "paso"        
        AudioClip clip = cannotInfluenceClips.GetRandomElement();
        aSource.PlayOneShot(clip);
    }

    private void PlayInfluenceSfx()
    {
        AudioClip clip = clips.GetRandomElement();
        aSource.PlayOneShot(clip);
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
            yield return new WaitForSeconds(Random.Range(0.8f, 1.4f)); // delay inicial

            if (_currentState == RaverState.IDLE)
            {
                SetDestination(_finalDestination);
                _currentState = RaverState.EXITING;
            }
        }
    }
}
