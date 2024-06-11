using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : TruongMonoBehaviour
{
    private static TimeManager instance;
    public static TimeManager Instance => instance;

    protected void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(TimeManager.instance.gameObject);
        }
        else Destroy(TimeManager.instance.gameObject);
    }

    public void ChangeTimeScale(int value)
    {
        Time.timeScale = value;
    }
}
