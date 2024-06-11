using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : IState
{
    PlayerController controller;

    public PlayerAttackState(PlayerController playerController)
    {
        this.controller = playerController;
    }

    public void Enter()
    {
        controller.PlayerAnimator.SetTriggerParameters("Tr_Attack");
    }

    public void Execute()
    {
        
    }

    public void Exit()
    {

    }
}
