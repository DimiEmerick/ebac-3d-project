using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;

public class GMStateIntro : StateBase
{
    public override void OnStateEnter(object o = null)
    {
        Debug.Log("Entered Intro State");
    }
    public override void OnStateExit()
    {
        Debug.Log("Exit Intro State");
    }
}
