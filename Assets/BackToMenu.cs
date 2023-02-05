using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackToMenu : MonoBehaviour
{
    public TextMeshProUGUI backText;
    public Image fadeImg;

    private async void Start()
    {
        StartCoroutine(Fade());
        backText.enabled = false;
        await Task.Delay(3000);
        InputSystem.onAnyButtonPress.CallOnce(ctrl => SceneManager.LoadScene("MainMenu"));
        await Task.Delay(2000);
        backText.enabled = true;
    }

    IEnumerator Fade()
    {
        for (float f = 1f; f >= 0; f -= 0.01f)
        {
            Color c = fadeImg.color;
            c.a = f;
            fadeImg.color = c;
            yield return null;
        }
    }
}