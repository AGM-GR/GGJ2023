using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour
{
    [System.Serializable]
    private struct ClipAndBMPs {
        public AudioClip audioClip;
        public float BPM;
    }

    public static event Action OnMusicChanged;
    public static float beatMultiplier;

    [SerializeField] DJMinigame[] djMinigames;
    [SerializeField] ClipAndBMPs[] clips;

    AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    public void StartMusicAndGames() {
        int clipIndex = UnityEngine.Random.Range(0, clips.Length);

        audioSource.clip = clips[clipIndex].audioClip;
        audioSource.Play();

        beatMultiplier = 1 / (60 / clips[clipIndex].BPM);
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player")) {
            foreach (Animator animator in player.GetComponentsInChildren<Animator>()) {
                animator.SetFloat("Beat", beatMultiplier);
            }
        }

        foreach(DJMinigame djMinigame in djMinigames) {
            djMinigame.StartMovement();
        }

        OnMusicChanged.Invoke();
    }
}
