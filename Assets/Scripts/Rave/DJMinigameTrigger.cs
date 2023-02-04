using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DJMinigameTrigger : MonoBehaviour
{
    [SerializeField] DJMinigame djMinigame;

    RaveColor myRaveColor;

    private void Start() {
        myRaveColor = GetComponentInParent<Rave>().RaveColor;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent<Character>(out Character character) && character.CharacterColor == myRaveColor) {
            djMinigame.Activate();
            character.GetComponent<CharacterDJMinigameInteraction>().InMinigame = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.TryGetComponent<Character>(out Character character) && character.CharacterColor == myRaveColor) {
            djMinigame.Deactivate();
            character.GetComponent<CharacterDJMinigameInteraction>().InMinigame = false;
        }
    }
}
