using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoints : SpawnPoints
{
    [SerializeField] protected float repeatRate;
    protected override void Start()
    {
        base.Start();
        InvokeRepeating(nameof(SpawnEnemy), 0f, repeatRate);
    }

    protected void SpawnEnemy()
    {
        if (points.Count <= 0) return;

        int rand = Random.Range(0, points.Count);
        Transform point = GetSpawnPoints(rand);
        EnemySpawner.Instance.SpawnRandom(point.position, Quaternion.identity);
    }

    protected Transform GetSpawnPoints(int index)
    {
        return points[index];
    }
}
