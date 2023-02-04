using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DJMinigame : MonoBehaviour {
    [Header("Game settings")]
    [SerializeField] float BPM = 156;
    [SerializeField] bool invert;

    [Header("Thresholds and limits")]
    [SerializeField] float skipLimit = 200;
    [SerializeField] float pressThreshold = 35;

    [Header("AudioClips")]
    [SerializeField] AudioClip successClip;
    [SerializeField] AudioClip failClip;

    DJButton[] djButtons;
    AudioSource audioSource;

    DJButton currentPressableButton;
    bool alreadyTried;

    private void Awake() {
        djButtons = GetComponentsInChildren<DJButton>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        for (int i = 0; i < djButtons.Length; ++i) {
            djButtons[i].Initialize(BPM, invert, skipLimit, pressThreshold);
            djButtons[i].transform.localPosition = new Vector3(BPM * i * (invert ? -1 : 1), 0, 0);
        }

        Animator anim = GetComponentInChildren<Animator>();
        if (anim) {
            anim.SetFloat("frequency", 1 / (60 / BPM));
        }
    }

    public void ReceiveInput(MinigameButton button) {
        if (currentPressableButton) {
            if (!currentPressableButton.AlreadyTried) {
                bool success = button == currentPressableButton.CurrentMinigameButton;
                currentPressableButton.TryButton(success);
                if (success) {
                    CorrectInput();
                } else {
                    FailedInput();
                }
            }
        } else if (!alreadyTried){
            FailedInput();
            alreadyTried = true;
        }
    }

    private void CorrectInput() {
        audioSource.PlayOneShot(successClip);
    }

    private void FailedInput() {
        audioSource.PlayOneShot(failClip);
    }

    public void ButtonEnteredThreshold(DJButton button) {
        currentPressableButton = button;
        alreadyTried = false;
    }

    public void ButtonExitedThreshold() {
        currentPressableButton = null;
    }
}
