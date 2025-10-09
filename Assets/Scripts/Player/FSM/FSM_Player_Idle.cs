using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;

public class FSM_Player_Idle : StateBase
{
    public Player player;
    public override void OnStateEnter(params object[] objs)
    {
        Debug.Log("Entered Idle state.");
        FSM_Player.Instance.player.playerAnimator.SetBool("Idle", true);
    }

    public override void OnStateStay()
    {
        Debug.Log("Player is standing still.");
    }

    public override void OnStateExit()
    {
        Debug.Log("Exit Idle state.");
        FSM_Player.Instance.player.playerAnimator.SetBool("Idle", false);
    }
}
