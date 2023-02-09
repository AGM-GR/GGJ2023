using System;
using UnityEngine;

public class TutorialPagination : MonoBehaviour
{
    public MainMenu mainMenu;
    public ButtonSelector empezarBtn;
    public GameObject[] pages;
    public int _currentPage = 0;


    public void NextPage()
    {
        pages[_currentPage].SetActive(false);
        _currentPage++;
        if(_currentPage == pages.Length)
        {
            _currentPage = 0;
            pages[_currentPage].SetActive(true);
            ExitTutorial();
        }
        else
        {
            pages[_currentPage].SetActive(true);
        }
    }

    private void ExitTutorial()
    {
        mainMenu.ShowTutorial(false);
        empezarBtn.Select();
    }

}
