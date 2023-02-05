using UnityEngine;
using System.Threading.Tasks;
using System.Collections.Generic;
using Cinemachine;

public class PlayerStunner : MonoBehaviour
{
    public List<AudioClip> hitMaleSfx;
    public List<AudioClip> hitFemaleSfx;

    private Character _character;

    public CharacterMovement _movement;
    public CharacterInfluenceAction _influence;
    public bool IsStunned;
    [Space]
    public float StunnedTimeInSeconds = 2;

    private Animator Animator => _character.CharacterAnimator;

    private CinemachineBasicMultiChannelPerlin noise;

    AudioSource aSource;

    private void Start()
    {
        _character = GetComponent<Character>();
        aSource = GetComponent<AudioSource>();
        noise = FindObjectOfType<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }


    private async void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stunner") && !IsStunned)
        {
            // vfx!
            AudioClip clip = _character.IsMale ? hitMaleSfx.GetRandomElement() : hitFemaleSfx.GetRandomElement();
            aSource.PlayOneShot(clip);
            StartStun(other);
            await Task.Delay((int)(StunnedTimeInSeconds * 1000));
            EndStun();
        }
    }


    private async void StartStun(Collider other)
    {
        Animator.SetTrigger("GetHit");
        other.gameObject.SetActive(false);
        IsStunned = true;
        _influence.CanInfluence = false;
        _movement.IsMovementAllowed = false;

        noise.m_AmplitudeGain = 1;
        await Task.Delay(100);
        noise.m_AmplitudeGain = 0;
    }

    private void EndStun()
    {
        Animator.SetTrigger("GetUp");
        _influence.CanInfluence = true;
        _movement.IsMovementAllowed = true;
        IsStunned = false;
    }
}
