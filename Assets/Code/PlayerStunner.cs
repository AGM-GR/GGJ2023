using UnityEngine;
using System.Threading.Tasks;

public class PlayerStunner : MonoBehaviour
{
    public CharacterMovement _movement;
    public Animator _animator;
    public CharacterInfluenceAction _influence;
    public bool IsStunned;
    [Space]
    public float StunnedTimeInSeconds = 2;

    private async void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stunner") && !IsStunned)
        {
            // vfx!
            // sfx!

            StartStun(other);
            await Task.Delay((int)(StunnedTimeInSeconds * 1000));
            EndStun();
        }
    }


    private void StartStun(Collider other)
    {
        _animator.SetTrigger("GetHit");
        other.gameObject.SetActive(false);
        IsStunned = true;
        _influence.CanInfluence = false;
        _movement.IsMovementAllowed = false;
    }

    private void EndStun()
    {
        _animator.SetTrigger("GetUp");
        _influence.CanInfluence = true;
        _movement.IsMovementAllowed = true;
        IsStunned = false;
    }
}
