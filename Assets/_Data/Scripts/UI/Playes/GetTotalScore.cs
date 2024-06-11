using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetTotalScore : TruongMonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI tilte;
    [SerializeField] protected TextMeshProUGUI textTotalCoin;
    [SerializeField] protected CollectSO collectSO;
    public CollectSO CollectSO => collectSO;

    protected override void OnEnable()
    {
        base.OnEnable();
        SetTexts();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadTexts();
        LoadCollectSO();
    }

    protected void LoadTexts()
    {
        tilte = transform.Find("Title").GetComponent<TextMeshProUGUI>();
        textTotalCoin = transform.Find("TextTotalCoin").GetComponent<TextMeshProUGUI>();
    }

    protected void LoadCollectSO()
    {
        if (collectSO != null) return;

        string resPath = "Collects/CollectSO";
        collectSO = Resources.Load<CollectSO>(resPath);
    }

    protected void SetTexts()
    {
        float totalCoin = 0f;
        foreach (ScoreCollectInfor infor in collectSO.listCollectInfor)
        {
            if (infor.isPlayerData) continue;
            totalCoin += infor.totalCoin;
        }
        textTotalCoin.text = totalCoin.ToString();
    }
}
