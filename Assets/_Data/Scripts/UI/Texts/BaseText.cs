using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseText : TruongMonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI text;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadText();
    }

    protected void LoadText()
    {
        if (text != null) return;
        text = GetComponent<TextMeshProUGUI>();
    }
}
