using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IState
{
    EnemyController controller;

    public EnemyIdleState(EnemyController controller)
    {
        this.controller = controller;
    }

    public void Enter()
    {
        controller.EnemyAnimator.SetTriggerAnimator("Tr_Idle");
    }

    public void Execute()
    {

    }

    public void Exit()
    {

    }
}
