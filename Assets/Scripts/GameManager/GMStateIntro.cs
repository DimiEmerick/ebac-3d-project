using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;

public class GMStateIntro : StateBase
{
    public override void OnStateEnter(params object[] objs)
    {
        Debug.Log("Entered Intro State");
    }
    public override void OnStateExit()
    {
        Debug.Log("Exit Intro State");
    }

    public override void OnStateStay()
    {
        //
    }
}
