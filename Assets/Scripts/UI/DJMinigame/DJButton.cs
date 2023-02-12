using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class DJButton : MonoBehaviour
{
    [System.Serializable]
    public struct ControlSchemeDependantSpritesCollection
    {
        public string ControlScheme;
        [Space]
        public Sprite SouthButtonSprite;
        public Sprite WestButtonSprite;
        public Sprite EastButtonSprite;
        public Sprite NorthButtonSprite;
    }


    [SerializeField] private ControlSchemeDependantSpritesCollection[] _spriteCollections;
    private int _currentSpritesIndex = 0;
    
    public MinigameButton CurrentMinigameButton { get; private set; }
    public bool AlreadyTried { get; set; }
    public bool Succeded { get; set; }
    public bool InMovement { get; set; }

    float skipLimit;
    float BPM;
    bool invert;
    bool halfBeats;
    float pressThreshold;
    float speedMultiplier;

    Image image;
    DJMinigame djMinigame;

    bool pressable;

    private void Awake() {
        image = GetComponentInChildren<Image>();
        djMinigame = GetComponentInParent<DJMinigame>();
        ResetMinigameButton();
    }

    public void SetControlScheme(string controlScheme)
    {
        for (int i = 0; i < _spriteCollections.Length; i++)
        {
            if (_spriteCollections[i].ControlScheme == controlScheme)
            {
                _currentSpritesIndex = i;
                if(InMovement) image.sprite = GetSprite(CurrentMinigameButton);
                return;
            }
        }

        _currentSpritesIndex = 0; // default
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

    public void Reset(float newXPosition, float newSpeedMultiplier, bool halfBeats) {
        speedMultiplier = newSpeedMultiplier;
        SetNewPosition(newXPosition);
        image.enabled = true;
        Succeded = false;
        AlreadyTried = false;
        this.halfBeats = halfBeats;
    }

    private void Update() {
        if (InMovement) {
            RectTransform rect = GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x - BPM * (1 / (60 / BPM)) * speedMultiplier * Time.deltaTime * (invert ? -1 : 1), rect.anchoredPosition.y);
            if (!pressable && InPressableSpace()) {
                pressable = true;
                djMinigame.ButtonEnteredThreshold(this);
            } else if (pressable && !InPressableSpace()) {
                pressable = false;
                djMinigame.ButtonExitedThreshold(this);
            } else if ((!invert && transform.localPosition.x < -skipLimit) || (invert && transform.localPosition.x > skipLimit)) {
                SetNewPosition(transform.localPosition.x + BPM * speedMultiplier * transform.parent.childCount * (invert ? -1 : 1) * (halfBeats ? 2 : 1));
                ResetMinigameButton();
            }
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
        Succeded = false;
        image.sprite = GetSprite(CurrentMinigameButton);
    }

    private Sprite GetSprite(MinigameButton button)
    {
        switch (button)
        {
            case MinigameButton.SOUTH:
                return _spriteCollections[_currentSpritesIndex].SouthButtonSprite;
            case MinigameButton.EAST:
                return _spriteCollections[_currentSpritesIndex].EastButtonSprite;
            case MinigameButton.WEST:
                return _spriteCollections[_currentSpritesIndex].WestButtonSprite;
            case MinigameButton.NORTH:
                return _spriteCollections[_currentSpritesIndex].NorthButtonSprite;
            default:
                return null;
        }
    }
}
