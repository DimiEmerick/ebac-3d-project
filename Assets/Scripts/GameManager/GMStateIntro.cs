using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;

public class GMStateIntro : StateBase
{
    public override void OnStateExit()
    {
        base.OnStateExit();
        Debug.Log("Saiu do estado Intro");
    }
}
