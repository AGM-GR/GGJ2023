using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDJMinigameInteraction : MonoBehaviour
{
    public DJMinigame DJMinigame { get; set; }

    public void OnMinigameSouthButton() {
        if (DJMinigame && DJMinigame.MinigameActive) {
            DJMinigame.ReceiveInput(MinigameButton.SOUTH);
        }
    }

    public void OnMinigameEastButton() {
        if (DJMinigame && DJMinigame.MinigameActive) {
            DJMinigame.ReceiveInput(MinigameButton.EAST);
        }
    }

    public void OnMinigameWestButton() {
        if (DJMinigame && DJMinigame.MinigameActive) {
            DJMinigame.ReceiveInput(MinigameButton.WEST);
        }
    }

    public void OnMinigameNorthButton() {
        if (DJMinigame && DJMinigame.MinigameActive) {
            DJMinigame.ReceiveInput(MinigameButton.NORTH);
        }
    }
}
