using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPBar : BaseSlider
{
    [SerializeField] protected PlayerController playerController;

    protected override void Start()
    {
        base.Start();
        Invoke(nameof(LoadPlayerController), 1f);
    }
    private void FixedUpdate()
    {
        SetHP();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
    }

    protected void LoadPlayerController()
    {
        if (playerController != null) return;
        playerController = FindObjectOfType<PlayerController>();
    }

    protected void SetHP()
    {
        if (playerController == null) return;
        int hp = playerController.PlayerDMR.Hp;
        int hpMax = playerController.PlayerDMR.HpMax;
        float hpRatio = (float)hp / (float)hpMax;
        slider.value = hpRatio;
    }
}
