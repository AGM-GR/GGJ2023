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

    float skipLimit;
    float BPM;
    bool invert;
    float pressThreshold;

    Image image;
    DJMinigame djMinigame;

    bool pressable;

    private void Awake() {
        image = GetComponent<Image>();
        djMinigame = GetComponentInParent<DJMinigame>();
        ResetMinigameButton();
    }

    public void Initialize(float BPM, bool invert, float skipLimit, float pressThreshold) {
        this.BPM = BPM;
        this.invert = invert;
        this.skipLimit = skipLimit;
        this.pressThreshold = pressThreshold;
    }

    public void TryButton(bool disableImage) {
        AlreadyTried = true;
        image.enabled = !disableImage;
    }

    private void Update() {
        if (true) {
            transform.Translate(-BPM * (1 / (60 / BPM)) * Time.deltaTime * (invert? -1 : 1), 0, 0);
            if (!pressable && InPressableSpace()) {
                pressable = true;
                djMinigame.ButtonEnteredThreshold(this);
            } else if (pressable && !InPressableSpace()) {
                pressable = false;
                djMinigame.ButtonExitedThreshold();
            } else if ((!invert && transform.localPosition.x < -skipLimit) || (invert && transform.localPosition.x > skipLimit)) {
                transform.Translate(BPM * transform.parent.childCount * (invert ? -1 : 1), 0, 0);
                ResetMinigameButton();
            }
        }
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
