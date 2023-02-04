using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DJButton : MonoBehaviour
{
    [SerializeField] Sprite[] buttonSprites;
    
    float limit = 200;
    float BPM = 157;
    bool invert;

    Image image;

    private void Awake() {
        image = GetComponent<Image>();
        ResetSprite();
    }

    public void Initialize(float BPM, bool invert, float limit) {
        this.BPM = BPM;
        this.invert = invert;
        this.limit = limit;
    }

    private void Update() {
        if (true) {
            transform.Translate(-BPM * (1 / (60 / BPM)) * Time.deltaTime * (invert? -1 : 1), 0, 0);

            if ((!invert && transform.localPosition.x < -limit) ||
                (invert && transform.localPosition.x > limit)) {                 
                transform.Translate(BPM * transform.parent.childCount * (invert ? -1 : 1), 0, 0);
                ResetSprite();
            }
        }
    }

    private void ResetSprite() {
        if (buttonSprites.Length > 0) {
            image.sprite = buttonSprites[Random.Range(0, buttonSprites.Length)];
        }
    }
}
