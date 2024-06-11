using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLinked : TruongMonoBehaviour
{
    [SerializeField] protected PlayerAnimator playerAnimator;
    public PlayerAnimator PlayerAnimator => playerAnimator;

    [SerializeField] protected PlayerMovement playerMovement;
    public PlayerMovement PlayerMovement => playerMovement;

    [SerializeField] protected PlayerDMR playerDMR;
    public PlayerDMR PlayerDMR => playerDMR;

    [SerializeField] protected PlayerAbilities playerAbilities;
    public PlayerAbilities PlayerAbilities => playerAbilities;

    [SerializeField] protected PlayerState playerState;
    public PlayerState PlayerState => playerState;

    [SerializeField] protected PlayerSO playerSO;
    public PlayerSO PlayerSO => playerSO;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPlayerState();
        LoadPlayerAnimator();
        LoadPlayerMovement();
        LoadPlayerDMR();
        LoadPlayerAbilities();
        LoadPlayerSO();
    }

    protected void LoadPlayerAnimator()
    {
        if (playerAnimator != null) return;
        playerAnimator = transform.GetComponentInChildren<PlayerAnimator>();
    }

    protected void LoadPlayerMovement()
    {
        if (playerMovement != null) return;
        playerMovement = transform.GetComponentInChildren<PlayerMovement>();
    }

    protected void LoadPlayerDMR()
    {
        if (playerDMR != null) return;
        playerDMR = transform.GetComponentInChildren<PlayerDMR>();
    }

    protected void LoadPlayerAbilities()
    {
        if (playerAbilities != null) return;
        playerAbilities = transform.GetComponentInChildren<PlayerAbilities>();
    }

    protected void LoadPlayerState()
    {
        if (playerState != null) return;
        playerState = transform.GetComponent<PlayerState>();
    }

    protected void LoadPlayerSO()
    {
        if (playerSO != null) return;

        string resPath = "Players/" + transform.name;
        playerSO = Resources.Load<PlayerSO>(resPath);
    }
}
