using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseButton : TruongMonoBehaviour
{
    [Header("Base Button")]
    [SerializeField] protected Button button;
    [SerializeField] protected float resetScale = 1f;
    [SerializeField] protected float resetDuration = 0.15f;
    [SerializeField] protected float scaleFactor = 0.8f;
    [SerializeField] protected float scaleDuration = 0.15f;

    protected override void Start()
    {
        base.Start();
        AddOnClickEvent();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadButton();
    }

    protected void LoadButton()
    {
        button = GetComponent<Button>();
    }

    protected void AddOnClickEvent()
    {
        button.onClick.AddListener(OnClick);
    }

    protected virtual void ActionOnlick()
    {
        transform.DOScale(scaleFactor, scaleDuration).SetEase(Ease.OutQuad).OnComplete(ResetButtonScale);
    }

    private void ResetButtonScale()
    {
        transform.DOScale(resetScale, resetDuration).SetEase(Ease.OutQuad);
    }

    protected abstract void OnClick();
}
