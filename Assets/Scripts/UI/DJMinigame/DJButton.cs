using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DJButton : MonoBehaviour
{
    [SerializeField] Sprite southButtonSprite;
    [SerializeField] Sprite westButtonSprite;
    [SerializeField] Sprite eastButtonSprite;
    [SerializeField] Sprite northButtonSprite;
    
    public MinigameButton CurrentMinigameButton { get; private set; }
    public bool AlreadyTried { get; set; }
    public bool Succeded { get; set; }

    float skipLimit;
    float BPM;
    bool invert;
    float pressThreshold;
    float speedMultiplier;

    Image image;
    DJMinigame djMinigame;

    bool pressable;

    private void Awake() {
        image = GetComponent<Image>();
        djMinigame = GetComponentInParent<DJMinigame>();
        ResetMinigameButton();
    }

    public void Initialize(float BPM, bool invert, float skipLimit, float pressThreshold, float startingSpeedMultiplier) {
        this.BPM = BPM;
        this.invert = invert;
        this.skipLimit = skipLimit;
        this.pressThreshold = pressThreshold;
        speedMultiplier = startingSpeedMultiplier;
    }

    public void TryButton(bool disableImage) {
        AlreadyTried = true;
        image.enabled = !disableImage;
    }

    public void Reset(float newXPosition, float newSpeedMultiplier) {
        speedMultiplier = newSpeedMultiplier;
        SetNewPosition(newXPosition);
        image.enabled = true;
        Succeded = false;
        AlreadyTried = false;
    }

    private void Update() {
        RectTransform rect = GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(rect.anchoredPosition.x - BPM * (1 / (60 / BPM)) * speedMultiplier * Time.deltaTime * (invert ? -1 : 1), rect.anchoredPosition.y);
        if (!pressable && InPressableSpace()) {
            pressable = true;
            djMinigame.ButtonEnteredThreshold(this);
        } else if (pressable && !InPressableSpace()) {
            pressable = false;
            djMinigame.ButtonExitedThreshold(this);
        } else if ((!invert && transform.localPosition.x < -skipLimit) || (invert && transform.localPosition.x > skipLimit)) {
            SetNewPosition(transform.localPosition.x + BPM * speedMultiplier * transform.parent.childCount * (invert ? -1 : 1));
            ResetMinigameButton();
        }
    }

    private void SetNewPosition(float xPosition) {
        transform.localPosition = new Vector3(xPosition, 0, 0);
    }

    private bool InPressableSpace() {
        if (invert) {
            return transform.localPosition.x > -pressThreshold && transform.localPosition.x < pressThreshold;
        }
        else {
            return transform.localPosition.x < pressThreshold && transform.localPosition.x > -pressThreshold;
        }
    }

    private void ResetMinigameButton() {
        CurrentMinigameButton = (MinigameButton)Random.Range(0, 4);
        image.enabled = true;
        AlreadyTried = false;
        image.sprite = GetSprite(CurrentMinigameButton);
    }  
    
    private Sprite GetSprite(MinigameButton button) {
        switch (button) {
            case MinigameButton.SOUTH:
                return southButtonSprite;
            case MinigameButton.EAST:
                return eastButtonSprite;
            case MinigameButton.WEST:
                return westButtonSprite;
            case MinigameButton.NORTH:
                return northButtonSprite;
            default:
                return null;
        }
    }
}
