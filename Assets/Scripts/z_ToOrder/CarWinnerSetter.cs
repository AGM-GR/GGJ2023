using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarWinnerSetter : MonoBehaviour
{
    public GameObject[] cars;
    public List<TextMeshProUGUI> scoreTexts;
    public List<GameObject> characterBanners;
    public List<GameObject> winnerGOs;
    
    private void Start()
    {
        SetWinner();
        SetLeaderboards();
    }


    private void SetLeaderboards()
    {
        string[] scores = PlayerPrefs.GetString("Scores").Split(",");
        //string[] indexes = PlayerPrefs.GetString("Indexes").Split(","); // para poder ordenarlos.

        for (int i = 0; i < scores.Length; i++)
        {
            scoreTexts[i].text = scores[i];
            characterBanners[i].SetActive(true);
        }

    }

    private void SetWinner()
    {
        int winnerIndex = PlayerPrefs.GetInt("Winner");
        var winner = cars[winnerIndex];
        winner.SetActive(true);

        Animator a = winner.GetComponentInChildren<Animator>();
        a.SetFloat("Beat", 2.8f);
        a.SetTrigger("Scratch");

        winnerGOs[winnerIndex].SetActive(true);
    }
}