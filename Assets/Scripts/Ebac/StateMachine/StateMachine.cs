using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace Ebac.StateMachine
{
    public class StateMachine<T> where T : System.Enum
    {
        public T CurrentStateName { get; private set; }
        public Dictionary<T, StateBase> dictionaryState;
        public float timeToStartGame = 1f;

        private StateBase _currentState;

        public StateBase CurrentState
        {
            get { return _currentState; }
        }

        public void Init()
        {
            dictionaryState = new Dictionary<T, StateBase>();
        }

        public void SwitchState(T state)
        {
            if (CurrentStateName != null && CurrentStateName.Equals(state))
                return;
            if (_currentState != null)
                _currentState.OnStateExit();
            _currentState = dictionaryState[state];
            CurrentStateName = state;
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
}
