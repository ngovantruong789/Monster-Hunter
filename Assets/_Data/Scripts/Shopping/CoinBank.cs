using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinBank : TruongMonoBehaviour
{
    private static CoinBank instance;
    public static CoinBank Instance => instance;

    [SerializeField] protected int coin;
    public int Coin => coin;

    [SerializeField] protected int maxCoin;
    public int MaxCoin => maxCoin;

    protected void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(CoinBank.instance.gameObject);
    }

    protected override void Start()
    {
        base.Start();
        GetCoinFromPlayFab();
    }

    public bool DetuctCoin(int amount)
    {
        if (coin < amount) return false;
        this.coin -= amount;
        if(coin <= 0) this.coin = 0;

        return true;
    }

    protected void GetCoinFromPlayFab()
    {
        PlayFabController.Instance.GetUserDataFromPlayFab(() =>
        {
            Dictionary<string, UserDataRecord> data = PlayFabController.Instance.GetUserData();

            foreach (KeyValuePair<string, UserDataRecord> k in data)
            {
                if (k.Key != "Coin") continue;

                UserDataRecord userDataRecord = k.Value;
                string value = userDataRecord.Value;
                coin = int.Parse(value);
                Debug.Log(value);
                break;
            }
        });
    }

    public void SendCoinToPlayFab(string key, string value)
    {
        Dictionary<String, string> data = new Dictionary<string, string>();
        //PlayFabController.Instance.SetUserData("Coin", coin.ToString());
        data.Add("Coin", coin.ToString());
        data.Add(key, value);

        PlayFabController.Instance.SetUserData(data);
    }
}
