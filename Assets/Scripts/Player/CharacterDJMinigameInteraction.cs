using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDJMinigameInteraction : MonoBehaviour
{

    public bool InMinigame { get; set; }
    public DJMinigame DJMinigame { get; set; }

    public void OnMinigameSouthButton() {
        if (InMinigame) {
            DJMinigame.ReceiveInput(MinigameButton.SOUTH);
        }
    }

    public void OnMinigameEastButton() {
        if (InMinigame) {
            DJMinigame.ReceiveInput(MinigameButton.EAST);
        }
    }

    public void OnMinigameWestButton() {
        if (InMinigame) {
            DJMinigame.ReceiveInput(MinigameButton.WEST);
        }
    }

    public void OnMinigameNorthButton() {
        if (InMinigame) {
            DJMinigame.ReceiveInput(MinigameButton.NORTH);
        }
    }
}
