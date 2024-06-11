using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunState : IState
{
    EnemyController controller;

    public EnemyRunState(EnemyController controller)
    {
        this.controller = controller;
    }

    public void Enter()
    {
        controller.EnemyAnimator.SetTriggerAnimator("Tr_Run");
    }

    public void Execute()
    {
        controller.EnemyAI.FollowTarget();
    }

    public void Exit()
    {

    }
}
