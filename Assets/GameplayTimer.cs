using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayTimer : MonoBehaviour
{
    private Car[] _cars;
    public Slider slider;

    public int TotalTimeInMinutes;
    private int _secondsLeft = 0;

    private void Start()
    {
        _cars = FindObjectsOfType<Car>();

        var totalSeconds = TotalTimeInMinutes * 60;
        _secondsLeft = totalSeconds;
        slider.minValue = 0;
        slider.maxValue = _secondsLeft;
        StartCoroutine(StartTimer());
    }

    public IEnumerator StartTimer()
    {
        do
        {
            slider.value = _secondsLeft;
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
