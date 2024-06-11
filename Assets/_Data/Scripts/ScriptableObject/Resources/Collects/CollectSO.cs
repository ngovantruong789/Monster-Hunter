using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Collect", menuName = "ScriptableObject/Collect")]
public class CollectSO : ScriptableObject
{
    public List<ScoreCollectInfor> listCollectInfor;

    public ScoreCollectInfor FindScoreInfor(string name)
    {
        foreach(ScoreCollectInfor infor in listCollectInfor)
            if(infor.collectName == name) return infor;

        return null;
    }

    public void ResetCount()
    {
        foreach (ScoreCollectInfor infor in listCollectInfor)
        {
            infor.collectCount = 0;
            infor.totalCoin = 0;
        }
    }

    public void SetToTalCount(string collectName, int value)
    {
        foreach (ScoreCollectInfor infor in listCollectInfor)
        {
            if (infor.collectName + "Device" == collectName || infor.collectName + "Email" == collectName)
                infor.totalCount = value;
        }
    }

    public void ResetTotalCount()
    {
        foreach (ScoreCollectInfor infor in listCollectInfor)
        {
            infor.totalCount = 0;
        }
    }
}
