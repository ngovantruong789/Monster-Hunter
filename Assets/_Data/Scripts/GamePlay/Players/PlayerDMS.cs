using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDMS : DamageSender
{
    [SerializeField] protected PlayerController playerController;
    public PlayerController PlayerController => playerController;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPlayerController();
        LoadDamage();
    }

    protected void LoadPlayerController()
    {
        if (playerController != null) return;
        playerController = transform.parent.GetComponent<PlayerController>();
    }

    protected override void LoadDamage()
    {
        damage = playerController.PlayerSO.damage;
    }
}
