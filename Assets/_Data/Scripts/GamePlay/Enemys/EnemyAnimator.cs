using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : BaseAnimator
{
    [SerializeField] protected EnemyController enemyController;
    public EnemyController EnemyController => enemyController;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadEnemyController();
    }

    protected void LoadEnemyController()
    {
        if (enemyController != null) return;
        enemyController = transform.parent.GetComponent<EnemyController>();
    }

    public void SetTriggerAnimator(string name)
    {
        animator.SetTrigger(name);
    }

    public void SetTriggerAttack(int attackType)
    {
        animator.SetTrigger("Tr_Attack" + attackType.ToString());
    }

    public void PlayerAttackEvent()
    {
        StartCoroutine(enemyController.EnemyAbilities.EnemyAttack.AttackEvent());
    }
}
