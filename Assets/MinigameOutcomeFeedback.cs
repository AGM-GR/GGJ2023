using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MinigameOutcomeFeedback : MonoBehaviour
{
    public GameObject[] feedbacks;
    public GameObject fail;

    public void ShowFeedback(int level)
    {
        feedbacks[level].SetActive(false);
        feedbacks[level].SetActive(true);
    }

    public void ShowFailFeedback()
    {
        fail.SetActive(false);
        fail.SetActive(true);
    }
}
