using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PlayerLinked
{
    [Header("Player Movement")]
    [SerializeField] protected Vector2 inputMove;
    [SerializeField] protected float speed;

    [Header("Player Attack")]
    [SerializeField] protected bool isAttack;

    [Header("Player DMR")]
    [SerializeField] protected bool isDeath;

    private void Update()
    {
        ChangeState();
    }

    protected void ChangeState() {
        Death();
        if (isDeath) return;

        Movement();
        Attack();

        if(isAttack) playerState.ChangeState(new PlayerAttackState(this));
        else if (inputMove == Vector2.zero) playerState.ChangeState(new PlayerIdleState(this));
        else playerState.ChangeState(new PlayerMoveState(this));
    }

    protected void Movement()
    {
        inputMove = playerMovement.InputMove;
        speed = playerMovement.Speed;
    }

    protected void Attack()
    {
        isAttack = playerAbilities.PlayerAttack.IsAttack;
        playerAbilities.PlayerAttack.OnAttack();
    }

    protected void Death()
    {
        isDeath = playerDMR.IsDeath;
    }
}
