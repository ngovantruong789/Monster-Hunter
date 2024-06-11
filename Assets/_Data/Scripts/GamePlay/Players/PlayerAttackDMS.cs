using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackDMS : DamageSender
{
    [SerializeField] protected PlayerAbilities playerAbilities;
    public PlayerAbilities PlayerAbilities => playerAbilities;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPlayerAbilities();
        LoadDamage();
    }

    protected void LoadPlayerAbilities()
    {
        if (playerAbilities != null) return;
        playerAbilities = transform.parent.GetComponent<PlayerAbilities>();
    }

    protected override void LoadDamage()
    {
        damage = playerAbilities.PlayerController.PlayerSO.damage;
    }
}
