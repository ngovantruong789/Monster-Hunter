using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObject/Player")]
public class PlayerSO : ScriptableObject
{
    public int hpMax;
    public int damage;
    public int defense;
    public float speed;
    public float attackSpeed;
    public float attackRecovery;
    public float timeSendDamage;
    public float timeEndSendDamage;
}
