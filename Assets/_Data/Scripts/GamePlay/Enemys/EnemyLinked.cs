using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLinked : TruongMonoBehaviour
{
    [SerializeField] protected EnemyAnimator enemyAnimator;
    public EnemyAnimator EnemyAnimator => enemyAnimator;

    [SerializeField] protected EnemyAI enemyAI;
    public EnemyAI EnemyAI => enemyAI;

    [SerializeField] protected EnemyDMR enemyDMR;
    public EnemyDMR EnemyDMR => enemyDMR;

    [SerializeField] protected EnemyAbilities enemyAbilities;
    public EnemyAbilities EnemyAbilities => enemyAbilities;

    [SerializeField] protected EnemyState enemyState;
    public EnemyState EnemyState => enemyState;

    [SerializeField] protected EnemySO enemySO;
    public EnemySO EnemySO => enemySO;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadEnemySO();
        LoadEnemyState();
        LoadEnemyAnimator();
        LoadEnemyAI();
        LoadEnemyDMR();
        LoadEnemyAbilities();
    }

    protected void LoadEnemyDMR()
    {
        if (enemyDMR != null) return;
        enemyDMR = transform.GetComponentInChildren<EnemyDMR>();
    }

    protected void LoadEnemyAnimator()
    {
        if (enemyAnimator != null) return;
        enemyAnimator = transform.GetComponentInChildren<EnemyAnimator>();
    }
    protected void LoadEnemyAI()
    {
        if (enemyAI != null) return;
        enemyAI = transform.GetComponentInChildren<EnemyAI>();
    }

    protected void LoadEnemyAbilities()
    {
        if (enemyAbilities != null) return;
        enemyAbilities = transform.GetComponentInChildren<EnemyAbilities>();
    }

    protected void LoadEnemyState()
    {
        if (enemyState != null) return;
        enemyState = transform.GetComponent<EnemyState>();
    }

    protected void LoadEnemySO()
    {
        if (enemySO != null) return;
        string resPath = "Enemys/" + transform.name;
        enemySO = Resources.Load<EnemySO>(resPath);
    }
}
