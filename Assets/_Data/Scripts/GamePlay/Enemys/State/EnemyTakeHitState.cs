using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeHitState : IState
{
    EnemyController controller;
    public EnemyTakeHitState(EnemyController controller)
    {
        this.controller = controller;
    }

    public void Enter()
    {
        controller.EnemyAnimator.SetTriggerAnimator("Tr_TakeHit");
        controller.StartCoroutine(controller.EnemyDMR.TakeHit());
    }

    public void Execute()
    {

    }

    public void Exit()
    {

    }
}
