using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    [SerializeField] RaveColor characterColor;

    public RaveColor CharacterColor { get { return characterColor; } }
}
