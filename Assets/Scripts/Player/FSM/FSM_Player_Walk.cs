using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;

public class FSM_Player_Walk : StateBase
{
    public Player player;
    public override void OnStateEnter(params object[] objs)
    {
        Debug.Log("Entered Walk state.");
        FSM_Player.Instance.player.playerAnimator.SetBool("Run", true);
    }

    public override void OnStateStay()
    {
        Debug.Log("Player is walking.");
        FSM_Player.Instance.player.Walk();
    }

    public override void OnStateExit()
    {
        Debug.Log("Exit Walk state.");
        FSM_Player.Instance.player.playerAnimator.SetBool("Run", false);
    }
}
