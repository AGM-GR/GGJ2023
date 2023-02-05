using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour
{
    [SerializeField] DJMinigame[] djMinigames;

    AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    public void StartMusicAndGames() {
        foreach(DJMinigame djMinigame in djMinigames) {
            djMinigame.StartMovement();
        }
        audioSource.Play();
    }
}
