using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinText : BaseText
{
    private void FixedUpdate()
    {
        SetText();
    }

    protected void SetText()
    {
        text.text = CoinBank.Instance.Coin.ToString();
    }
}
