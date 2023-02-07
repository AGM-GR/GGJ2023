using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class IntroLoadMainMenu : MonoBehaviour
{
    public PlayableDirector playable;
    public float timeToAllowSkip = 5f;
    [SerializeField] private TMP_Text _skipIntroText;

    [Header("Skip Action")]
    [SerializeField] private InputAction _skipIntro;

    private void OnEnable()
    {
        _skipIntro.Enable();
    }

    private void OnDisable()
    {
        _skipIntro.Disable();
    }

    private IEnumerator Start()
    {
        _skipIntroText.alpha = 0;
        bool canSkipIntro = PlayerPrefs.GetInt("introPlayed", 0) == 1;
        float startTIme = Time.time;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainMenu");
        asyncLoad.allowSceneActivation = false;

        //Wait to finish async load scene
        yield return null;
        yield return new WaitUntil(() => asyncLoad.progress >= 0.8);

        if (canSkipIntro)
        {
            //Wait the remaining time to allow skip
            float timeToShowSkip = timeToAllowSkip - (Time.time - startTIme);
            if (timeToAllowSkip > 0)
                yield return new WaitForSeconds(timeToAllowSkip);

            StartCoroutine(ShowSkipText());
        }

        //Wait intro to finish or skip
        yield return new WaitUntil(() => playable.state != PlayState.Playing || (canSkipIntro && _skipIntro.WasPressedThisFrame()));

        PlayerPrefs.SetInt("introPlayed", 1);
        asyncLoad.allowSceneActivation = true;
    }

    private IEnumerator ShowSkipText()
    {
        float alpha = 0;
        while (alpha <= 1)
        {
            alpha += Time.deltaTime;
            _skipIntroText.alpha = alpha;

            yield return null;
        }
    }
}
