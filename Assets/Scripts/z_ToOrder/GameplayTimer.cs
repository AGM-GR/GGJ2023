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

    public float TotalTimeInMinutes;
    private int _secondsLeft = 0;

    private void Start()
    {
        _cars = FindObjectsOfType<Car>();

        int totalSeconds = Mathf.FloorToInt(TotalTimeInMinutes * 60f);
        _secondsLeft = totalSeconds;
        StartCoroutine(StartTimer());
    }

    public IEnumerator StartTimer()
    {
        do
        {
            var span = new TimeSpan(0, 0, _secondsLeft);
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

        //var scoresOrdered = _cars.OrderByDescending(c => c.CurrentRavers).Select(c => c.CurrentRavers).ToArray();
        //var indexesOrdered = _cars.OrderByDescending(c => c.CurrentRavers).Select(c => c.CharacterIndex).ToArray();


        var scores = _cars
            .Where(c => c.CharacterIndex != -1)
            .OrderBy(c => c.CharacterIndex)
            .Select(c => c.CurrentRavers)
            .ToArray();


        PlayerPrefs.SetString("Scores", string.Join(",", scores));

        //PlayerPrefs.SetString("Indexes", string.Join(",", indexesOrdered));
        SceneManager.LoadScene("EndGameScene");
    }
}
