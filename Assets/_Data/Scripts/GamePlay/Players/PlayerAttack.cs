using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : BaseAbility
{
    [Header("PlayerAttack")]
    [SerializeField] protected PlayerAbilities playerAbilities;
    public PlayerAbilities PlayerAbilities => playerAbilities;

    [SerializeField] protected PlayerAttackDMS playerAttackDMS;
    public PlayerAttackDMS PlayerAttackDMS => playerAttackDMS;

    [SerializeField] protected Collider2D colliderAttack;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected float timeSendDamage;
    [SerializeField] protected float timeEndSendamage;
    [SerializeField] protected bool isAttack;
    public bool IsAttack => isAttack;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPlayerAbilities();
        LoadColliderAttack();
        LoadPlayerAttackDMS();
        LoadStats();
    }

    protected void LoadPlayerAbilities()
    {
        if (playerAbilities != null) return;
        playerAbilities = transform.parent.GetComponent<PlayerAbilities>();
    }

    protected void LoadColliderAttack()
    {
        if (colliderAttack != null) return;
        colliderAttack = transform.GetComponent<Collider2D>();
        colliderAttack.isTrigger = true;
        colliderAttack.enabled = false;
    }

    protected void LoadPlayerAttackDMS()
    {
        if (playerAttackDMS != null) return;
        playerAttackDMS = transform.GetComponent<PlayerAttackDMS>();
    }

    protected void LoadStats()
    {
        attackSpeed = playerAbilities.PlayerController.PlayerSO.attackSpeed;
        timeAbility = playerAbilities.PlayerController.PlayerSO.attackRecovery;
        timeSendDamage = playerAbilities.PlayerController.PlayerSO.timeSendDamage;
        timeEndSendamage = playerAbilities.PlayerController.PlayerSO.timeEndSendDamage;
    }

    public void OnAttack()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (!isReady) return;
        if (isAttack) return;

        isAttack = true;
        StartCoroutine(Attack());
    }

    protected IEnumerator Attack()
    {
        yield return new WaitForSeconds(timeSendDamage);
        colliderAttack.enabled = true;
        yield return new WaitForSeconds(timeEndSendamage);
        colliderAttack.enabled = false;
        yield return new WaitForSeconds(attackSpeed);
        isReady = false;
        isAttack = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Enemy")) return;

        playerAttackDMS.SendDamage(collision.transform);
    }
}
