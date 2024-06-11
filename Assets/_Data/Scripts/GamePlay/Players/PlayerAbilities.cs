using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : TruongMonoBehaviour
{
    [SerializeField] protected PlayerController playerController;
    public PlayerController PlayerController => playerController;

    [SerializeField] protected PlayerAttack playerAttack;
    public PlayerAttack PlayerAttack => playerAttack;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPlayerController();
        LoadPlayerAttack();
    }

    protected void LoadPlayerController()
    {
        if (playerController != null) return;
        playerController = transform.parent.GetComponent<PlayerController>();
    }

    protected void LoadPlayerAttack()
    {
        if (playerAttack != null) return;
        playerAttack = transform.GetComponentInChildren<PlayerAttack>();
    }
}
