using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Test
{
    public enum Test2
    {
        NONE
    }
    public void Test3()
    {
        StateMachine<Test2> stateMachine = new StateMachine<Test2>();

        stateMachine.RegisterStates(Test.Test2.NONE, new StateBase());
    }
}

public class StateMachine<T> where T : System.Enum
{
    public Dictionary<T, StateBase> dictionaryState;
    public float timeToStartGame = 1f;

    private StateBase _currentState;

    public void Init()
    {
        dictionaryState = new Dictionary<T, StateBase>();
    }

    public void SwitchState(T state)
    {
        if (_currentState != null) _currentState.OnStateExit();
        _currentState = dictionaryState[state];
        _currentState.OnStateEnter();
    }

    public void RegisterStates(T typeEnum, StateBase state)
    {
        dictionaryState.Add(typeEnum, state);
    }

    public void Update()
    {
        if (_currentState != null) _currentState.OnStateStay();
    }
}
