using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour
{
    public static MusicController Instance { get; private set; }

    [System.Serializable]
    private struct ClipAndBMPs {
        public AudioClip audioClip;
        public float BPM;
    }

    public static event Action OnMusicChanged;
    public static float beatMultiplier;

    [SerializeField] DJMinigame[] djMinigames;
    [SerializeField] ClipAndBMPs[] clips;

    [Header("Energy Drink")]
    [SerializeField] AudioSource musicAudioSource;
    [SerializeField] float musicVolume = 1;
    [SerializeField] AudioSource energyDrinkAudioSource;
    [SerializeField] float energyDrinkVolume = 1;
    [SerializeField] float fadeSpeed = 1;
    [SerializeField] Animator energyDrinkVFX;
    [SerializeField] List<AudioClip> drinkMusics;

    bool crossFading;
    float t;

    private void Awake() {
        Instance = this;
    }

    private void Update() {
        if (crossFading) {
            t = Mathf.MoveTowards(t, 1, fadeSpeed * Time.deltaTime);
            musicAudioSource.volume = Mathf.Lerp(0, musicVolume, t);
            energyDrinkAudioSource.volume = Mathf.Lerp(energyDrinkVolume, 0, t);
            if (t == 1) {
                crossFading = false;
            }
        }
    }

    public void StartMusicAndGames() {
        int clipIndex = UnityEngine.Random.Range(0, clips.Length);

        musicAudioSource.clip = clips[clipIndex].audioClip;
        musicAudioSource.Play();

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

    public void PlayEnergyDrink() {
        crossFading = false;
        musicAudioSource.volume = 0;
        energyDrinkAudioSource.clip = drinkMusics.GetRandomElement();
        energyDrinkAudioSource.volume = energyDrinkVolume;
        energyDrinkAudioSource.Play();

        energyDrinkVFX.SetFloat("Beat", beatMultiplier);
        energyDrinkVFX.SetTrigger("RootStart");
    }

    public void EndEnergyDrink() {
        t = 0;
        crossFading = true;
        energyDrinkVFX.SetTrigger("RootEnd");
    }
}
