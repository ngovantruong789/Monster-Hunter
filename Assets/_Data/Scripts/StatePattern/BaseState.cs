using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState : TruongMonoBehaviour
{
    private IState playerState;
    public void ChangeState(IState state)
    {
        if (playerState != null && playerState.GetType() == state.GetType()) return;
        playerState = state;
        if (playerState == null)
        {
            playerState.Exit();
            return;
        }

        playerState.Enter();
        //Debug.Log(playerState.ToString());
    }

    private void Update()
    {
        if (playerState == null) return;
        playerState.Execute();
    }
}
