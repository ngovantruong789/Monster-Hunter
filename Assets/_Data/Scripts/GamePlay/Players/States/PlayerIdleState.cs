using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IState
{
    PlayerController controller;

    public PlayerIdleState(PlayerController playerController)
    {
        this.controller = playerController;
    }

    public void Enter()
    {
        controller.PlayerAnimator.SetBoolParametersWithValue("IsIdle");
        
    }

    public void Execute()
    {
        
    }

    public void Exit()
    {
        
    }
}
