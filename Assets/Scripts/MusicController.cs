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

    [SerializeField] DJMinigame[] djMinigames;
    [SerializeField] ClipAndBMPs[] clips;

    AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    public void StartMusicAndGames() {
        int clipIndex = Random.Range(0, clips.Length);

        audioSource.clip = clips[clipIndex].audioClip;
        audioSource.Play();

        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player")) {
            foreach (Animator animator in player.GetComponentsInChildren<Animator>()) {
                animator.SetFloat("Beat", 1 / (60 / clips[clipIndex].BPM));
            }
        }

        foreach(DJMinigame djMinigame in djMinigames) {
            djMinigame.StartMovement();
        }
    }
}
