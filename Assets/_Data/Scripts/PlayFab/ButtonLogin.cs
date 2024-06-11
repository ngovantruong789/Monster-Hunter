using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ButtonLogin : ButtonLoadScene
{
    protected override void OnClick()
    {
        Time.timeScale = 1;
        if (sceneName == "") LoadSceneAgain();
        else
        {
            LoginPlayFab();
            ActionOnlick();
            //StartCoroutine(LoadSceneWithDelay(2f));
        }
    }

    private IEnumerator LoadSceneWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        LoadScene();
    }

    protected abstract void LoginPlayFab();
}
