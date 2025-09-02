using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;

public class GMStateGameplay : StateBase
{
    public override void OnStateEnter(object o = null)
    {
        Debug.Log("Entrou no estado Gameplay");
    }
}
