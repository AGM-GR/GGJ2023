using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DJMinigame : MonoBehaviour {
    [SerializeField] float BPM = 156;
    [SerializeField] bool invert;
    [SerializeField] float limit = 200;

    private void Start() {
        DJButton[] djButtons = GetComponentsInChildren<DJButton>();
        for (int i = 0; i < djButtons.Length; ++i) {
            djButtons[i].Initialize(BPM, invert, limit);
            djButtons[i].transform.localPosition = new Vector3(BPM * i * (invert ? -1 : 1), 0, 0);
        }

        Animator anim = GetComponentInChildren<Animator>();
        if (anim) {
            anim.SetFloat("frequency", 1 / (60 / BPM));
        }
    }
}
