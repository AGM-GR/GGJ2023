using UnityEngine;
using System.Threading.Tasks;

public class PlayerStunner : MonoBehaviour
{
    private Character _character;

    public CharacterMovement _movement;
    public CharacterInfluenceAction _influence;
    public bool IsStunned;
    [Space]
    public float StunnedTimeInSeconds = 2;

    private Animator Animator => _character.CharacterAnimator;

    private void Start()
    {
        _character = GetComponent<Character>();
    }


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
        Animator.SetTrigger("GetHit");
        other.gameObject.SetActive(false);
        IsStunned = true;
        _influence.CanInfluence = false;
        _movement.IsMovementAllowed = false;
    }

    private void EndStun()
    {
        Animator.SetTrigger("GetUp");
        _influence.CanInfluence = true;
        _movement.IsMovementAllowed = true;
        IsStunned = false;
    }
}
