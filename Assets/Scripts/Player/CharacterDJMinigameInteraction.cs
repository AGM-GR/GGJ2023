using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDJMinigameInteraction : MonoBehaviour
{
    [SerializeField] DJMinigame djMinigame;

    public bool InMinigame { get; set; }

    public void OnMinigameSouthButton() {
        if (InMinigame) {
            djMinigame.ReceiveInput(MinigameButton.SOUTH);
        }
    }

    public void OnMinigameEastButton() {
        if (InMinigame) {
            djMinigame.ReceiveInput(MinigameButton.EAST);
        }
    }

    public void OnMinigameWestButton() {
        if (InMinigame) {
            djMinigame.ReceiveInput(MinigameButton.WEST);
        }
    }

    public void OnMinigameNorthButton() {
        if (InMinigame) {
            djMinigame.ReceiveInput(MinigameButton.NORTH);
        }
    }
}
