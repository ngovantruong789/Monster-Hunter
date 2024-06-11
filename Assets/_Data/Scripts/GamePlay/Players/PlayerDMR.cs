using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDMR : DamageReceiver
{
    [SerializeField] protected PlayerController playerController;
    public PlayerController PlayerController => playerController;

    [SerializeField] protected GameObject panelPlayerDeath;

    public int Hp => hp;
    public int HpMax => hpMax;

    protected override void Start()
    {
        base.Start();
        CollectedManager.Instance.CollectSO.ResetCount();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPlayerController();
        LoadStats();
        LoadPanelPlayerDeath();
    }

    protected void LoadPlayerController()
    {
        if (playerController != null) return;
        playerController = transform.parent.GetComponent<PlayerController>();
    }

    protected void LoadPanelPlayerDeath()
    {
        if (panelPlayerDeath != null) return;
        panelPlayerDeath = FindObjectOfType<Canvas>().transform.Find("PlayerDeath").gameObject;
        panelPlayerDeath.SetActive(false);
    }

    protected override void LoadStats()
    {
        hpMax = playerController.PlayerSO.hpMax;
    }

    public override void DetuctHp(int amount)
    {
        base.DetuctHp(amount);
        Color takeHit = Color.red;
        SpriteRenderer spriteRenderer = playerController.PlayerAnimator.gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = takeHit;
        Invoke(nameof(ResetColor), 0.2f);
    }

    protected void ResetColor()
    {
        Color defaultColor = Color.white;
        SpriteRenderer spriteRenderer = playerController.PlayerAnimator.gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = defaultColor;
    }

    protected override void OnDead()
    {
        isDeath = true;
        playerController.PlayerState.ChangeState(new PlayerDeathState(playerController));
        CollectedManager.Instance.UpdateCollectToPlayFab();
        Invoke(nameof(Dead), 0.5f);
    }

    protected void Dead()
    {
        panelPlayerDeath.SetActive(true);
        Invoke(nameof(StopTime), 1.5f);
    }

    protected void StopTime()
    {
        TimeManager.Instance.ChangeTimeScale(0);
    }
}
