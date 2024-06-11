using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerDeath : TruongMonoBehaviour
{
    [SerializeField] protected float yOffset = 10f;
    [SerializeField] float animationDuration = 1f;

    protected override void OnEnable()
    {
        base.OnEnable();
        PlayAppear();
    }

    protected void PlayAppear()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        Vector3 initialPosition = transform.position;
        transform.position = new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z);
        canvasGroup.alpha = 0f;
        transform.DOMoveY(initialPosition.y, animationDuration);
        canvasGroup.DOFade(1f, animationDuration);
    }
}
