using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;

public class FSM_Player_Jump : StateBase
{
    public override void OnStateEnter(params object[] objs)
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
