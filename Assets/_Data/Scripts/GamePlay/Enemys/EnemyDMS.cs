using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDMS : DamageSender
{
    [SerializeField] protected EnemyAttack enemyAttack;
    public EnemyAttack EnemyAttack => enemyAttack;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadEnemyAttack();
        LoadDamage();
    }

    protected void LoadEnemyAttack()
    {
        if (enemyAttack != null) return;
        enemyAttack = transform.parent.GetComponent<EnemyAttack>();
    }

    protected override void LoadDamage()
    {
        damage = enemyAttack.EnemyAbilities.EnemyController.EnemySO.damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Player")) return;
        SendDamage(collision.transform);
    }
}
