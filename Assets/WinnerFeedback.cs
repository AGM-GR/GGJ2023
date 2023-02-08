using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WinnerFeedback : MonoBehaviour
{
    public List<Car> cars;
    public Animator[] animators;

    private int prevIndex = -1;
    private int currentIndex = 0;

    public void Update()
    {
        int max = cars.Select(c => c.CurrentRavers).Max();
        if (max == 0)
        {
            animators[currentIndex].SetBool("IsWinning", false);
            return;
        }

        for (int i = 0; i < cars.Count; i++)
        {
            if (max == cars[i].CurrentRavers)
            {
                currentIndex = i;
                break;
            }
        }

        if (prevIndex != currentIndex)
        {            
            if(prevIndex >= 0) animators[prevIndex].SetBool("IsWinning", false);
            animators[currentIndex].SetBool("IsWinning", true);
            prevIndex = currentIndex;
        }

    }
}
