using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    [SerializeField] CarColor characterColor;

    public CarColor CharacterColor { get { return characterColor; } }

    public void SetCharacterColor(CarColor carColor)
    {
        characterColor = carColor;
    }
}
