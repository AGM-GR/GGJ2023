using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleBeatRandomizer : MonoBehaviour
{
    void Start()
    {
        Animator[] animators = GetComponentsInChildren<Animator>();
        foreach (var a in animators)
        {
            //UnityEngine.Random.Range(0f, 5f)
            a.SetFloat("Beat", 2.8f);
            a.Play("Idle", 0, UnityEngine.Random.value);
        }
    }
}
