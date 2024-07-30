using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectedManager : TruongMonoBehaviour
{
    private static CollectedManager instance;
    public static CollectedManager Instance => instance;

    [SerializeField] protected CollectSO collectSO;
    public CollectSO CollectSO => collectSO;

    protected void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(CollectedManager.instance.gameObject);
        }
        else Destroy(CollectedManager.instance.gameObject);

        SetCollectSO();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCollectSO();
    }

    protected void LoadCollectSO()
    {
        if (collectSO != null) return;

        string resPath = "Collects/CollectSO";
        collectSO = Resources.Load<CollectSO>(resPath);
    }

    public void SetCollectSO()
    {
        PlayFabController.Instance.GetStats(() =>
        {
            List<StatisticValue> statisticList = PlayFabController.Instance.GetStatisticsList();
            if (statisticList.Count <= 0)
            {
                collectSO.ResetTotalCount();
                return;
            }
            foreach (StatisticValue statistic in statisticList)
                collectSO.SetToTalCount(statistic.StatisticName, statistic.Value);
        });
    }
    public void AddStatsFromEnemy(string name, int count)
    {
        foreach (ScoreCollectInfor infor in collectSO.listCollectInfor)
        {
            if (infor.collectName != name) continue;
            infor.totalCoin += count;
            if (infor.isPlayerData) continue;
            infor.collectCount += 1;
            //if (infor.totalCoin < infor.collectCount) infor.totalCoin = infor.collectCount;
        }
    }

    public void UpdateCollectToPlayFab()
    {
        TotalCoin();

        if (PlayerPrefs.GetString("DeviceID") == PlayFabController.Instance.MyID)
            UpdateWithID("Device");
        else if (PlayerPrefs.GetString("EmailID") == PlayFabController.Instance.MyID)
            UpdateWithID("Email");
    }

    protected void UpdateWithID(string idName)
    {
        foreach (ScoreCollectInfor infor in collectSO.listCollectInfor)
        {
            if (infor.isPlayerData) continue;
            if (infor.totalCount < infor.collectCount) infor.totalCount = infor.collectCount;
            PlayFabController.Instance.SetStats(infor.collectName + idName, infor.totalCount);
        }
    }

    protected void TotalCoin()
    {
        int totalCoin = 0;
        foreach (ScoreCollectInfor infor in collectSO.listCollectInfor)
        {
            if (infor.isPlayerData) continue;
            totalCoin += infor.totalCoin;
        }

        foreach (ScoreCollectInfor infor in collectSO.listCollectInfor)
        {
            if (infor.isPlayerData && infor.collectName == "Coin")
            {
                int coin = CoinBank.Instance.Coin;
                //infor.totalCoin = totalCoin;
                totalCoin += coin;
                if(totalCoin >= CoinBank.Instance.MaxCoin) totalCoin = CoinBank.Instance.MaxCoin;
                Dictionary<string, string> data = new Dictionary<string, string>();
                data.Add(infor.collectName, totalCoin.ToString());
                PlayFabController.Instance.SetUserData(data);
                break;
            }
        }
    }
}
