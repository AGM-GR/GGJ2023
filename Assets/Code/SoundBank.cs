using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundBank", menuName = "ScriptableObjects/SoundBank", order = 1)]
public class SoundBank : ScriptableObject
{
    public List<AudioClip> Clips;

    //public AudioClip GetRandomClip()
    //{

    //}
}