using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : IState
{
    EnemyController controller;
    public EnemyDeathState(EnemyController controller)
    {
        this.controller = controller;
    }

    public void Enter()
    {
        controller.EnemyAnimator.SetTriggerAnimator("Tr_Death");
    }

    public void Execute()
    {

    }

    public void Exit()
    {

    }
}
