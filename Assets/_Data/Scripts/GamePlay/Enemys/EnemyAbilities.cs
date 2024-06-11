using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAbilities : TruongMonoBehaviour
{
    [SerializeField] protected EnemyController enemyController;
    public EnemyController EnemyController => enemyController;

    [SerializeField] protected EnemyAttack enemyAttack;
    public EnemyAttack EnemyAttack => enemyAttack;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadEnemyController();
        LoadEnemyAttack();
    }

    protected void LoadEnemyController()
    {
        if (enemyController != null) return;
        enemyController = transform.parent.GetComponent<EnemyController>();
    }

    protected void LoadEnemyAttack()
    {
        if (enemyAttack != null) return;
        enemyAttack = transform.GetComponentInChildren<EnemyAttack>();
    }
}
