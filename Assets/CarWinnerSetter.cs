using UnityEngine;

public class CarWinnerSetter : MonoBehaviour
{
    public GameObject[] cars;
    
    private void Start()
    {
        int winnerIndex = PlayerPrefs.GetInt("Winner");
        cars[winnerIndex].SetActive(true);
    }
}