using PlayFab.InsightsModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : BaseAbility
{
    [Header("EnemyAttack")]
    [SerializeField] protected EnemyAbilities enemyAbilities;
    public EnemyAbilities EnemyAbilities => enemyAbilities;

    [SerializeField] protected List<Collider2D> collidersAttack;

    public int randAttack;

    [SerializeField] protected float attackSpeed;
    [SerializeField] protected float timeSendDamage;
    [SerializeField] protected float timeEndSendamage;

    [SerializeField] protected bool isAttack;
    public bool IsAttack => isAttack;

    protected override void OnDisable()
    {
        base.OnDisable();
        ResetAttack();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadEnemyController();
        LoadCollidersAttack();
        LoadStats();
    }

    protected void LoadEnemyController()
    {
        if (enemyAbilities != null) return;
        enemyAbilities = transform.parent.GetComponent<EnemyAbilities>();
    }

    protected void LoadCollidersAttack()
    {
        foreach (Transform transform in transform)
        {
            Collider2D collider = transform.GetComponent<Collider2D>();
            collider.isTrigger = true;
            collider.enabled = false;
            collidersAttack.Add(collider);
        }
    }

    protected void LoadStats()
    {
        attackSpeed = enemyAbilities.EnemyController.EnemySO.attackSpeed;
        timeAbility = enemyAbilities.EnemyController.EnemySO.attackRecovery;
        timeSendDamage = enemyAbilities.EnemyController.EnemySO.timeSendDamage;
        timeEndSendamage = enemyAbilities.EnemyController.EnemySO.timeEndSendDamage;
    }

    public void Attack()
    {
        if (isAttack || !isReady) return;
        isAttack = true;
        GetAttackType();
        enemyAbilities.EnemyController.EnemyState.ChangeState(new EnemyAttackState(enemyAbilities.EnemyController, randAttack));
    }

    public IEnumerator AttackEvent()
    {
        yield return new WaitForSeconds(timeSendDamage);
        collidersAttack[randAttack - 1].enabled = true;
        yield return new WaitForSeconds(timeEndSendamage);
        collidersAttack[randAttack - 1].enabled = false;
        yield return new WaitForSeconds(attackSpeed);
        ResetAttack();
        enemyAbilities.EnemyController.EnemyState.ChangeState(new EnemyIdleState(enemyAbilities.EnemyController));
    }

    protected void GetAttackType()
    {
        randAttack = Random.Range(1, 3);
    }

    public void ResetAttack()
    {
        isAttack = false;
        isReady = false;
    }
}
