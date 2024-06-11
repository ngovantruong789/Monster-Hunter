using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButtonm : BaseButton
{

    protected override void OnClick()
    {
        transform.DOScale(0.8f, 0.15f).SetEase(Ease.OutQuad).OnComplete(ResetButtonScale);
    }

    private void ResetButtonScale()
    {
        // Đặt lại tỷ lệ thu/phóng ban đầu của button
        transform.DOScale(1f, 0.15f).SetEase(Ease.OutQuad);
    }
}
