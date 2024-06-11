
using System;
using UnityEngine;

[Serializable]
public class ScoreCollectInfor
{
    public string collectName;
    public int collectCount;
    public int totalCount;
    public int totalCoin;
    public int score;
    [SerializeField] public bool isPlayerData;
}
