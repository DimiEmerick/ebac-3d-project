using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;

public class GMStateGameplay : StateBase
{
    public override void OnStateEnter(params object[] objs)
    {
        Debug.Log("Entered Gameplay State");
    }

    public override void OnStateStay()
    {
        //
    }
}
