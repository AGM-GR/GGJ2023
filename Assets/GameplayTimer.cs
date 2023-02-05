using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayTimer : MonoBehaviour
{
    private Car[] _cars;
    public TextMeshProUGUI TimeText;

    public int TotalTimeInMinutes;
    private int _secondsLeft = 0;

    private void Start()
    {
        _cars = FindObjectsOfType<Car>();

        var totalSeconds = TotalTimeInMinutes * 60;
        _secondsLeft = totalSeconds;
        StartCoroutine(StartTimer());
    }

    public IEnumerator StartTimer()
    {
        do
        {
            var span = new TimeSpan(0, 0, _secondsLeft); //Or TimeSpan.FromSeconds(seconds); (see Jakob C´s answer)
            var result = string.Format("{0}:{1:00}",
                                        (int)span.TotalMinutes,
                                        span.Seconds);


            TimeText.text = result;
            yield return new WaitForSeconds(1);
            _secondsLeft--;

        } while (_secondsLeft > 0);

        EndGame();
    }

    private void EndGame()
    {
        int max = _cars.Select(c => c.CurrentRavers).Max();
        int winnerIndex = _cars.Where(c => c.CurrentRavers == max).Select(c => c.CharacterIndex).First();
        PlayerPrefs.SetInt("Winner", winnerIndex);
        SceneManager.LoadScene("EndGameScene");
    }
}
