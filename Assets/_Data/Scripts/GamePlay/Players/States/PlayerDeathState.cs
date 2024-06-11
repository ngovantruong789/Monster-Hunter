using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : IState
{
    PlayerController controller;

    public PlayerDeathState(PlayerController playerController)
    {
        this.controller = playerController;
    }

    public void Enter()
    {
        controller.PlayerAnimator.SetTriggerParameters("Tr_Death");
    }

    public void Execute()
    {
        
    }

    public void Exit()
    {
        
    }
}
