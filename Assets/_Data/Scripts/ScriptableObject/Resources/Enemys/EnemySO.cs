using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObject/Enemy")]
public class EnemySO : ScriptableObject
{
    public int coin;
    public int rateSpawn;
    public int hpMax;
    public float speed;
    public int damage;
    public float distanceAttack;
    public float attackSpeed;
    public float attackRecovery;
    public float timeSendDamage;
    public float timeEndSendDamage;
    public float timerTakeHit;
}
