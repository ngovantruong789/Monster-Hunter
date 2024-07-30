using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDMR : DamageReceiver
{
    [SerializeField] protected EnemyController enemyController;
    public EnemyController EnemyController => enemyController;

    [SerializeField] protected float timerTakeHit;
    [SerializeField] protected float timerDespawn = 0.5f;
    protected override void OnDisable()
    {
        ResetValue();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadEnemyController();
        LoadStats();
    }

    protected void LoadEnemyController()
    {
        if (enemyController != null) return;
        enemyController = transform.parent.GetComponent<EnemyController>();
    }

    protected override void LoadStats()
    {
        hpMax = enemyController.EnemySO.hpMax;
        timerTakeHit = enemyController.EnemySO.timerTakeHit;
    }

    public override void DetuctHp(int amount)
    {
        base.DetuctHp(amount);
        isTakeHit = true;
    }

    public IEnumerator TakeHit()
    {
        yield return new WaitForSeconds(timerTakeHit);
        isTakeHit = false;
        enemyController.EnemyAbilities.EnemyAttack.ResetAttack();
        enemyController.EnemyState.ChangeState(new EnemyIdleState(enemyController));
    }

    protected override void OnDead()
    {
        colliderImpact.enabled = false;
        enemyController.EnemyState.ChangeState(new EnemyDeathState(enemyController));
        Invoke(nameof(DespawnObject), timerDespawn);
    }

    protected void DespawnObject()
    {
        CollectedManager.Instance.AddStatsFromEnemy(enemyController.name, enemyController.EnemySO.coin);
        EnemySpawner.Instance.Despawn(transform.parent);
    }

    protected void ResetValue()
    {
        isDeath = false;
        isTakeHit = false;
        colliderImpact.enabled = true;
        Reborn();
    }
}
