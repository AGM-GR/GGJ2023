using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DJMinigame : MonoBehaviour {
    [System.Serializable]
    private struct MinigameTier {
        public float duration;
        public float speedMultiplier;
        public int successGoalToLevelUp;
        public float raveInfluence;
        public bool halfBeats;
    }

    [Header("Game settings")]
    [SerializeField] float BPM = 156;
    [SerializeField] bool invert;
    [SerializeField] int currentTier = 1;
    [SerializeField] MinigameTier[] tiers;

    [Header("Thresholds and limits")]
    [SerializeField] float skipLimit = 200;
    [SerializeField] float pressThreshold = 35;

    [Header("AudioClips")]
    [SerializeField] AudioClip[] successClips;
    [SerializeField] AudioClip failClip;
    [SerializeField] AudioClip tierUpClip;

    [Header("References")]
    [SerializeField] TextMeshProUGUI targetText;
    [SerializeField] MinigameOutcomeFeedback feedback;

    public bool MinigameActive { get; set; }
    public bool IsOnMaxTier => currentTier == tiers.Length - 1;

    Car car;
    Character character;
    DJButton[] djButtons;
    AudioSource audioSource;

    DJButton currentPressableButton;
    bool alreadyTried;

    float speedMultiplier = 1;
    int remainingSuccessesToTierUp;

    float timeToTierDown;

    float activatedXPosition;
    float deactivatedXPosition;

    private void Awake() {
        djButtons = GetComponentsInChildren<DJButton>();
        audioSource = GetComponent<AudioSource>();

        activatedXPosition = transform.localPosition.x;
        deactivatedXPosition = activatedXPosition + GetComponent<RectTransform>().sizeDelta.x * 2 * (invert ? 1 : -1);
    }

    private void Start() {
        ResetTier();

        for (int i = 0; i < djButtons.Length; ++i) {
            djButtons[i].Initialize(BPM, invert, skipLimit, pressThreshold, speedMultiplier);
        }

        Animator anim = GetComponentInChildren<Animator>();
        if (anim) {
            anim.SetFloat("frequency", 1 / (60 / BPM));
        }

        Deactivate(false);
    }
    
    private void Update() {
        if (!MinigameActive && currentTier > 1) {
            timeToTierDown -= Time.deltaTime;
            if (timeToTierDown <= 0) {
                TierDown();
            }
        }
    }

    public void SetCar(Car car) {
        this.car = car;
    }

    public void Activate(Character character) {
        transform.localPosition = new Vector3(activatedXPosition, transform.localPosition.y, 0);
        MinigameActive = true;
        this.character = character;

        foreach (DJButton djButton in djButtons)
        {
            djButton.SetControlScheme(character.ControlScheme);
        }
    }

    public void Deactivate(bool showInteractHint = true) {
        transform.localPosition = new Vector3(deactivatedXPosition, transform.localPosition.y, 0);
        MinigameActive = false;
        if (character) {
            character.GetComponent<CharacterMovement>().IsMovementAllowed = true;
            character.CharacterAnimator.SetTrigger("ScratchEnd");
        }

        if (showInteractHint && !IsOnMaxTier) {
            car.ShowFliparteHint();
        }
    }

    public void StartMovement() {
        foreach (DJButton djButton in djButtons) {
            djButton.InMovement = true;
        }
        GetComponentInChildren<Animator>().SetTrigger("Start");
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
        audioSource.PlayOneShot(successClips[Random.Range(0, successClips.Length)]);
        remainingSuccessesToTierUp--;
        targetText.text = remainingSuccessesToTierUp.ToString();
        currentPressableButton.Succeded = true;

        if (remainingSuccessesToTierUp == 0) {
            TierUp();
        }
    }

    private void FailedInput(bool closing = true) {
        if (closing) {
            audioSource.PlayOneShot(failClip);
            StartCoroutine(DeactivateNextFrame());
            feedback.ShowFailFeedback();
        }

        remainingSuccessesToTierUp = tiers[currentTier].successGoalToLevelUp;
        targetText.text = remainingSuccessesToTierUp.ToString();
    }

    private IEnumerator DeactivateNextFrame() {
        yield return null;
        Deactivate();
    }

    private void TierUp() {
        if (currentTier + 1 < tiers.Length) {
            currentTier++;
        }
        ResetTier();
        car.LowTierPassed();
        audioSource.PlayOneShot(tierUpClip);
        ShowFeedback();
        StartCoroutine(DeactivateNextFrame());
    }

    private void TierDown() {
        currentTier--;
        ShowFeedback();
        ResetTier();
    }

    private void ShowFeedback()
    {
        feedback.ShowFeedback(currentTier);
    }

    public void LowestTier() {
        currentTier = 0;
        ResetTier();
    }

    private void ResetTier() {
        speedMultiplier = tiers[currentTier].speedMultiplier;
        timeToTierDown = tiers[currentTier].duration;
        remainingSuccessesToTierUp = tiers[currentTier].successGoalToLevelUp;
        targetText.text = tiers[currentTier].successGoalToLevelUp.ToString();
        car.SetInfluence(tiers[currentTier].raveInfluence);
        car.SetAwesomessLevel(currentTier);
        ResetAllButtons();
    }

    public void ButtonEnteredThreshold(DJButton button) {
        currentPressableButton = button;
        alreadyTried = false;
    }

    public void ButtonExitedThreshold(DJButton button) {
        if (MinigameActive) {
            if (!button.Succeded) {
                FailedInput(false);
            }

            if (currentPressableButton == button) {
                currentPressableButton = null;
            }
        }
    }

    private void ResetAllButtons() {
        float distance = BPM * speedMultiplier * (tiers[currentTier].halfBeats ? 2 : 1);
        for (int i = 0; i < djButtons.Length; ++i) {
            djButtons[i].Reset(distance * (i + 1) + distance * MusicController.Instance.NormalizedBeatTime, speedMultiplier, tiers[currentTier].halfBeats);
        }
        currentPressableButton = null;
    }
}
