using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : IState
{
    PlayerController controller;

    public PlayerMoveState(PlayerController playerController)
    {
        this.controller = playerController;
    }

    public void Enter()
    {
        controller.PlayerAnimator.SetBoolParametersWithValue("IsWalk");
    }

    public void Execute()
    {
        if (controller.PlayerAbilities.PlayerAttack.IsAttack) return;
        controller.PlayerMovement.Movement();
    }

    public void Exit()
    {
        
    }
}
