using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;

public class FSM_Player_Jump : StateBase
{
    public override void OnStateEnter(object o = null)
    {
        Debug.Log("Player jumped");
        FSM_Player.Instance.player.Jump();
    }

    public override void OnStateStay()
    {
        //
    }

    public override void OnStateExit()
    {
        //
    }
}
