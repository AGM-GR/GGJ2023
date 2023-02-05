using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDJMinigameInteraction : MonoBehaviour
{
    public DJMinigame DJMinigame { get; set; }

    public bool InDJMinigame { get { return DJMinigame && DJMinigame.MinigameActive; } }

    public void OnMinigameSouthButton() {
        if (InDJMinigame) {
            DJMinigame.ReceiveInput(MinigameButton.SOUTH);
        }
    }

    public void OnMinigameEastButton() {
        if (InDJMinigame) {
            DJMinigame.ReceiveInput(MinigameButton.EAST);
        }
    }

    public void OnMinigameWestButton() {
        if (InDJMinigame) {
            DJMinigame.ReceiveInput(MinigameButton.WEST);
        }
    }

    public void OnMinigameNorthButton() {
        if (InDJMinigame) {
            DJMinigame.ReceiveInput(MinigameButton.NORTH);
        }
    }
}
