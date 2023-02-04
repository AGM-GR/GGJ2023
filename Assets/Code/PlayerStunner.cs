using UnityEngine;
using System.Threading.Tasks;

public class PlayerStunner : MonoBehaviour
{
    public CharacterMovement _movement;
    public CharacterInfluenceAction _influence;
    public bool IsStunned;

    private async void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stunner") && !IsStunned)
        {
            // vfx!
            // sfx!

            other.gameObject.SetActive(false);

            IsStunned = true;
            Debug.Log("Start stun!");
            _influence.CanInfluence = false;
            _movement.IsMovementAllowed = false;
            await Task.Delay(2000);
            Debug.Log("End stun!");
            _influence.CanInfluence = true;
            _movement.IsMovementAllowed = true;
            IsStunned = false;
        }
    }
}
