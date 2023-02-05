using System.Collections.Generic;
using UnityEngine;

public class PeopleBeatRandomizer : MonoBehaviour
{
    void Start()
    {
        Animator[] animators = GetComponentsInChildren<Animator>();
        foreach (var a in animators)
        {
            a.SetFloat("Beat", 2.8f);

            List<string> states = new List<string>() { "Idle", "Idle1", "Idle2", "Idle3", "Idle4", "IdleRelaxed" };
            a.Play(states.GetRandomElement(), 0, UnityEngine.Random.value);
        }
    }
}
