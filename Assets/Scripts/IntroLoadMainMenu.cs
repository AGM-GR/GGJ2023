using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class IntroLoadMainMenu : MonoBehaviour
{
    public PlayableDirector playable;

    private IEnumerator Start()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainMenu");
        asyncLoad.allowSceneActivation = false;

        yield return null;
        yield return new WaitWhile(() => playable.state == PlayState.Playing);
        
        asyncLoad.allowSceneActivation = true;
    }
}
