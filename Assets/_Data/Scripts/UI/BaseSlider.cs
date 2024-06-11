using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseSlider : TruongMonoBehaviour
{
    [SerializeField] protected Slider slider;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSlider();
    }

    protected void LoadSlider()
    {
        slider = GetComponent<Slider>();
    }
}
