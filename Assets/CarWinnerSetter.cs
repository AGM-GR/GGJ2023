using UnityEngine;

public class CarWinnerSetter : MonoBehaviour
{
    public GameObject[] cars;
    
    private void Start()
    {
        int winnerIndex = PlayerPrefs.GetInt("Winner");
        var winner = cars[winnerIndex];
        winner.SetActive(true);

        Animator a = winner.GetComponentInChildren<Animator>();
        a.SetFloat("Beat", 2.8f);
        a.SetTrigger("Scratch");
    }
}