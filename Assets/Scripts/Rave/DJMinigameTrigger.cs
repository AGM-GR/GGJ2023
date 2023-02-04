using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DJMinigameTrigger : MonoBehaviour
{
    [SerializeField] DJMinigame djMinigame;

    CarColor myRaveColor;

    private void Start() {
        myRaveColor = GetComponentInParent<Rave>().RaveColor;
        djMinigame.SetRave(GetComponentInParent<Rave>());
        GetComponentInParent<Rave>().DJMinigame = djMinigame;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out Character character) && character.CharacterColor == myRaveColor) {
            djMinigame.Activate();
            CharacterDJMinigameInteraction interaction = character.GetComponent<CharacterDJMinigameInteraction>();
            interaction.InMinigame = true;
            interaction.DJMinigame = djMinigame;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.TryGetComponent(out Character character) && character.CharacterColor == myRaveColor) {
            djMinigame.Deactivate();
            character.GetComponent<CharacterDJMinigameInteraction>().InMinigame = false;
        }
    }
}
