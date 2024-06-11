using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetScoreBoard : TruongMonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI tilte;
    [SerializeField] protected TextMeshProUGUI textValueKilled;
    [SerializeField] protected TextMeshProUGUI textValueHighScore;
    [SerializeField] protected TextMeshProUGUI textValueCoin;
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
        tilte = transform.Find("Tilte").GetComponent<TextMeshProUGUI>();
        textValueKilled = transform.Find("TextValueKilled").GetComponent<TextMeshProUGUI>();
        textValueHighScore = transform.Find("TextValueHighScore").GetComponent<TextMeshProUGUI>();
        textValueCoin = transform.Find("TextValueCoin").GetComponent<TextMeshProUGUI>();
    }

    protected void LoadCollectSO()
    {
        if (collectSO != null) return;

        string resPath = "Collects/CollectSO";
        collectSO = Resources.Load<CollectSO>(resPath);
    }

    protected void SetTexts()
    {
        foreach (ScoreCollectInfor infor in collectSO.listCollectInfor)
        {
            if (transform.name != infor.collectName) continue;

            textValueKilled.text = infor.collectCount.ToString();
            textValueHighScore.text = infor.totalCount.ToString();
            textValueCoin.text = infor.totalCoin.ToString();
        }
    }
}
