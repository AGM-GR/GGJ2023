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

    [System.Serializable]
    private struct ClipBPMAndCarColor {
        public ClipAndBMPs clipAndBPM;
        public CarColor color;
    }

    public static event Action OnMusicChanged;
    public float BeatMultiplier { get; private set; }
    public float NormalizedBeatTime { get; private set; }

    [SerializeField] DJMinigame[] djMinigames;
    [SerializeField] ClipAndBMPs[] clips;
    [SerializeField] float startingClipBPM = 160;

    [Header("Energy Drink")]
    [SerializeField] AudioSource musicAudioSource;
    [SerializeField] float musicVolume = 1;
    [SerializeField] AudioSource energyDrinkAudioSource;
    [SerializeField] float energyDrinkVolume = 1;
    [SerializeField] float fadeSpeed = 1;
    [SerializeField] Animator energyDrinkVFX;
    [SerializeField] List<ClipBPMAndCarColor> drinkMusics;

    bool crossFading;
    float t;
    float currentBPM;

    private void Awake() {
        Instance = this;

        currentBPM = startingClipBPM;
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

        NormalizedBeatTime = (Time.time % (1 / currentBPM * 60)) / (1 / currentBPM * 60);
        
    }

    public void StartMusicAndGames() {
        int clipIndex = UnityEngine.Random.Range(0, clips.Length);
        currentBPM = clips[clipIndex].BPM;

        musicAudioSource.clip = clips[clipIndex].audioClip;
        musicAudioSource.Play();

        NormalizedBeatTime = 0;

        BeatMultiplier = currentBPM / 60;
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player")) {
            foreach (Animator animator in player.GetComponentsInChildren<Animator>()) {
                animator.SetFloat("Beat", BeatMultiplier);
            }
        }

        foreach(DJMinigame djMinigame in djMinigames) {
            djMinigame.StartMovement();
        }

        OnMusicChanged.Invoke();
    }

    public void PlayEnergyDrink(CarColor color) {
        crossFading = false;
        musicAudioSource.volume = 0;

        ClipBPMAndCarColor clipData = GetClipData(color);
        energyDrinkAudioSource.clip = clipData.clipAndBPM.audioClip;
        energyDrinkAudioSource.volume = energyDrinkVolume;
        energyDrinkAudioSource.Play();

        energyDrinkVFX.SetFloat("Beat", 1 / (60 / clipData.clipAndBPM.BPM));
        energyDrinkVFX.SetTrigger("RootStart");
    }

    public void EndEnergyDrink() {
        t = 0;
        crossFading = true;
        energyDrinkVFX.SetTrigger("RootEnd");
    }

    private ClipBPMAndCarColor GetClipData(CarColor color) {
        return drinkMusics.Find(x => x.color == color);
    }
}
