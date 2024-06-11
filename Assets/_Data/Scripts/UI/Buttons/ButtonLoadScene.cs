using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLoadScene : BaseButton
{
    [SerializeField] protected string sceneName;
    [SerializeField] protected float timerLoad = 0.5f;
    protected override void OnClick()
    {
        Time.timeScale = 1;
        ActionOnlick();
        if (sceneName == "")
            Invoke(nameof(LoadSceneAgain), timerLoad);
        else
            Invoke(nameof(LoadScene), timerLoad);
    }

    protected virtual void LoadSceneAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    protected virtual void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
