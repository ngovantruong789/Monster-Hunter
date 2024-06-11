using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : TruongMonoBehaviour
{
    [SerializeField] protected EnemyController enemyController;
    public EnemyController EnemyController => enemyController;

    [SerializeField] protected Transform target;
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected float distanceAttack;
    public float DistanceAttack => distanceAttack;

    [SerializeField] protected float distance;
    public float Distance => distance;

    protected override void OnEnable()
    {
        base.OnEnable();
        LoadTarget();
        LoadStats();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadEnemyController();
        LoadAgent();
        LoadStats();
    }

    protected void LoadEnemyController()
    {
        if (enemyController != null) return;
        enemyController = transform.GetComponent<EnemyController>();
    }

    protected void LoadTarget()
    {
        if (target != null) return;
        target = FindObjectOfType<PlayerController>().transform;
    }

    protected void LoadAgent()
    {
        if(agent != null) return;
        agent = transform.GetComponent<NavMeshAgent>();
        
        agent.speed = enemyController.EnemySO.speed;
        distanceAttack = enemyController.EnemySO.distanceAttack;
        agent.stoppingDistance = distanceAttack;
    }

    protected void LoadStats()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    public void FollowTarget()
    {
        if (agent == null || target == null) return;

        if (GetDistance() <= distanceAttack) return;
        ChangeLook();
        agent.SetDestination(target.position);
    }

    protected void ChangeLook()
    {
        Vector3 scale = transform.localScale;
        if (target.position.x < transform.position.x && scale.x > 0) scale.x *= -1;
        else if (target.position.x > transform.position.x && scale.x < 0) scale.x *= -1;

        transform.localScale = scale;
    }

    public float GetDistance()
    {
        distance = Vector3.Distance(transform.position, target.position);
        return distance;
    }

    public bool CheckStop()
    {
        if(enemyController.EnemyDMR.IsDeath) return true;
        if(enemyController.EnemyDMR.IsTakeHit) return true;

        return false;
    }
}
