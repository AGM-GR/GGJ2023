using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    public TextMeshProUGUI backText;

    private async void Start()
    {
        InputSystem.onAnyButtonPress.CallOnce(ctrl => SceneManager.LoadScene("MainMenu"));

        backText.enabled = false;
        await Task.Delay(5000);
        backText.enabled = true;
    }
}