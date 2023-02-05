using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] CarColor characterColor;
    public int CharacterIndex;
    public GameObject[] Models;
    public Animator CharacterAnimator;

    public bool IsInit;

    public CarColor CharacterColor { get { return characterColor; } }

    public void Initialize(int index)
    {
        CharacterIndex = index;
        characterColor = (CarColor) index;
        Models[CharacterIndex].SetActive(true);
        CharacterAnimator = Models[CharacterIndex].GetComponent<Animator>();

        IsInit = true;
    }
}
