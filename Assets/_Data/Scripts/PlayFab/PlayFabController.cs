using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayFabController : TruongMonoBehaviour
{
    private static PlayFabController instance;
    public static PlayFabController Instance => instance;

    [SerializeField] protected TMP_InputField username;
    [SerializeField] protected TMP_InputField email;
    [SerializeField] protected TMP_InputField password;
    [SerializeField] protected TextMeshProUGUI messageText;
    [SerializeField] protected Dictionary<string, UserDataRecord> userData = new Dictionary<string, UserDataRecord>();
    [SerializeField] protected List<StatisticValue> statisticsList = new List<StatisticValue>();
    [SerializeField] protected string nextSceneName;
    [SerializeField] protected string myID;
    public string MyID => myID;
    [SerializeField] protected bool isDeleteKey;
    private void Awake()
    {
        if(isDeleteKey)
            PlayerPrefs.DeleteAll();
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(PlayFabController.instance.gameObject);
        }
        else Destroy(PlayFabController.instance.gameObject);
    }

    protected override void Start()
    {
        base.Start();
        CheckTitleID();

        if (PlayerPrefs.HasKey("EMAIL"))
        {
            email.text = PlayerPrefs.GetString("EMAIL");
            password.text = PlayerPrefs.GetString("PASSWORD");
        }
    }

    protected void CheckTitleID()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId)) PlayFabSettings.TitleId = "D3DEF";
    }

    #region Login Email
    public void LoginWithEmail()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = email.text,
            Password = password.text
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }

    protected void OnLoginSuccess(LoginResult result)
    {
        PlayerPrefs.SetString("EMAIL", email.text);
        PlayerPrefs.SetString("PASSWORD", password.text);
        messageText.text = "Logged in successfully, wait 2 seconds to enter the game!!!";
        
        myID = result.PlayFabId;
        PlayerPrefs.SetString("EmailID", myID);
        Debug.Log("PlayerEmailID: " + myID);
        GetUserDataFromPlayFab();
        GetStats();
        Invoke(nameof(LoadLevel), 2f);
    }

    protected void OnLoginFailure(PlayFabError error)
    {
        OnRegister();
    }

    protected void OnRegister()
    {
        var request = new RegisterPlayFabUserRequest
        {
            Username = username.text,
            Email = email.text,
            Password = password.text
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailure);
    }

    protected void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        PlayerPrefs.SetString("EMAIL", email.text);
        PlayerPrefs.SetString("PASSWORD", password.text);
        myID = result.PlayFabId;

        messageText.text = "Already registered account, please log in again!!!";
    }

    protected void OnRegisterFailure(PlayFabError error)
    {
        Debug.Log("Registration failed!!!");
        messageText.text = "Registration failed!!!";
        OnError(error);
    }
    #endregion Login Email

    #region Login With Device ID
    public void LoginWithDeviceID()
    {
        OnLoginWithDeviceID();
    }

    protected void OnLoginWithDeviceID()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginDeviceIDSuccess, OnLoginDeviceIDFailure);
    }

    protected void OnLoginDeviceIDSuccess(LoginResult result)
    {
        messageText.text = "Logged in with device id, wait 2 seconds to enter the game!!!";
        myID = result.PlayFabId;
        PlayerPrefs.SetString("DeviceID", myID);
        Debug.Log("PlayerDeviceID: " + myID);
        GetUserDataFromPlayFab();
        GetStats();
        Invoke(nameof(LoadLevel), 2f);
    }

    protected void OnLoginDeviceIDFailure(PlayFabError error)
    {
        messageText.text = "Login with device id failed!!!";
    }

    #endregion Login With Device ID

    #region PlayerData
    public void GetUserDataFromPlayFab(Action callback = null)
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            PlayFabId = myID,
            Keys = null
        }, result =>
        {
            OnGetUserDataSuccess(result);
            callback?.Invoke(); // Gọi callback nếu có được cung cấp
        }, OnError);
    }

    protected void OnGetUserDataSuccess(GetUserDataResult result)
    {
        if (result.Data == null) return;
        userData.Clear();
        
        foreach(KeyValuePair<string, UserDataRecord> data in result.Data)
        {
            userData.Add(data.Key, data.Value);
        }
    }

    public Dictionary<string, UserDataRecord> GetUserData()
    {   
        return userData;
    }

    public void SetUserData(Dictionary<string, string> data)
    {
        //Debug.Log(key + ": " + value);
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = data
        }, SetIUserDataSuccess, OnError);
    }

    protected void SetIUserDataSuccess(UpdateUserDataResult result)
    {
        Debug.Log("Dua du lieu len PlayFab thanh cong!!!");
    }

    protected void OnError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }

    #endregion PlayerData

    #region Statistics
    public void SetStats(string name, int value)
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate {StatisticName = name, Value = value},
            }
        },
        result => { Debug.Log("Update Statistics thanh cong"); },
        error => { Debug.Log(error.GenerateErrorReport()); });
    }

    public void GetStats(Action callback = null)
    {
        PlayFabClientAPI.GetPlayerStatistics(
            new GetPlayerStatisticsRequest(),
            result =>
            {
                OnGetStats(result);
                callback?.Invoke();
            },
            OnError);
    }

    protected void OnGetStats(GetPlayerStatisticsResult result)
    {
        Debug.Log("Lay Stats thanh cong");
        statisticsList.Clear();
        foreach (StatisticValue stat in result.Statistics)
            statisticsList.Add(stat);
    }

    public List<StatisticValue> GetStatisticsList()
    {
        return statisticsList;
    }

    #endregion Statistics

    protected void LoadLevel()
    {
        LoadScene.Instance.LoadNextScene(nextSceneName);
    }
}
