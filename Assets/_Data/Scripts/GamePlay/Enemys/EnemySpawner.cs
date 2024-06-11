using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
    private static EnemySpawner instance;
    public static EnemySpawner Instance => instance;

    [SerializeField] protected int limitSpawn;
    private void Awake()
    {
        if(instance == null)
            instance = this;
        else Destroy(gameObject);
    }

    public void SpawnRandom(Vector3 pos, Quaternion rot)
    {
        float rand = Random.Range(0, 101);
        foreach (Transform prefab in prefabs)
        {
            EnemyController enemyController = prefab.GetComponent<EnemyController>();
            if(rand <= enemyController.EnemySO.rateSpawn)
            {
                Transform enemy = Spawn(prefab.name, pos, rot);
                enemy.gameObject.SetActive(true);
                break;
            }
        }
    }
}
