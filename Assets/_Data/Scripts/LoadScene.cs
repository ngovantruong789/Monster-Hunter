using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    private static LoadScene instance;
    public static LoadScene Instance => instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(LoadScene.instance.gameObject);
        }
        else Destroy(LoadScene.instance.gameObject);
    }

    public void LoadNextScene(string sceneName)
    {
        if (sceneName == "this") SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        else SceneManager.LoadScene(sceneName);
    }
}
