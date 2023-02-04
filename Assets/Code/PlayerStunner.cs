using UnityEngine;
using System.Threading.Tasks;

public class PlayerStunner : MonoBehaviour
{
    public CharacterMovement _movement;

    private async void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stunner"))
        {
            // vfx!
            // sfx!
            _movement.enabled = false;
            await Task.Delay(2000);
            _movement.enabled = true;
        }
    }
}
