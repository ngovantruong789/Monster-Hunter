using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IState
{
    EnemyController controller;
    int randAttack;
    public EnemyAttackState (EnemyController controller, int randAttack)
    {
        this.controller = controller;
        this.randAttack = randAttack;
    }

    public void Enter()
    {
        controller.EnemyAnimator.SetTriggerAttack(randAttack);
    }

    public void Execute()
    {
        
    }

    public void Exit()
    {
        
    }

}
