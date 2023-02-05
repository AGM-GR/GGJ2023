using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject tutorialPanel;

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowTutorial(bool show)
    {
        tutorialPanel.SetActive(show);
    }
}
