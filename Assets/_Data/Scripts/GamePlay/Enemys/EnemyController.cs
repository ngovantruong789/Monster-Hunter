using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : EnemyLinked
{
    [Header("Enemy AI")]
    [SerializeField] protected float distance;
    [SerializeField] protected float distanceAttack;
    [SerializeField] protected bool isStopMove;
    [Header("Enemy Attack")]
    [SerializeField] protected bool isAttack;
    [Header("Enemy DMR")]
    [SerializeField] protected bool isTakeHit;
    private void Update()
    {
        PlayState();
    }

    protected void PlayState()
    {
        Movement();
        SetTakeHit();

        if (CheckDead()) return;
        if(distance > distanceAttack && !isStopMove && !isAttack)
            enemyState.ChangeState(new EnemyRunState(this));
        else if(isTakeHit) enemyState.ChangeState(new EnemyTakeHitState(this));
        else Attack();
    }

    protected void Movement()
    {
        distance = enemyAI.GetDistance();
        distanceAttack = enemyAI.DistanceAttack;
        isStopMove = enemyAI.CheckStop();
    }

    protected void Attack()
    {
        isAttack = enemyAbilities.EnemyAttack.IsAttack;
        enemyAbilities.EnemyAttack.Attack();
    }

    protected void SetTakeHit()
    {
        isTakeHit = enemyDMR.IsTakeHit;
    }

    protected bool CheckDead()
    {
        return enemyDMR.IsDeath;
    }
}
