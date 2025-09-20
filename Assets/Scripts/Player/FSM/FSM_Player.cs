using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;
using Ebac.Core.Singleton;

public class FSM_Player : Singleton<FSM_Player>
{
    public Player player;
    public StateMachine<PlayerStates> stateMachine;

    private bool _isMoving;
    public enum PlayerStates
    {
        PLAYER_IDLE,
        PLAYER_WALK,
        PLAYER_JUMP
    }
    private void Start()
    {
        stateMachine = new StateMachine<PlayerStates>();
        stateMachine.Init();
        stateMachine.RegisterStates(PlayerStates.PLAYER_IDLE, new FSM_Player_Idle());
        stateMachine.RegisterStates(PlayerStates.PLAYER_WALK, new FSM_Player_Walk());
        stateMachine.RegisterStates(PlayerStates.PLAYER_JUMP, new FSM_Player_Jump());
        stateMachine.SwitchState(PlayerStates.PLAYER_IDLE);
    }

    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.stateMachine.CurrentStateName == GameManager.GameStates.GAMEPLAY)
        {
            stateMachine.Update();
            _isMoving = Input.GetKey(KeyCode.W) ||
                        Input.GetKey(KeyCode.A) ||
                        Input.GetKey(KeyCode.S) ||
                        Input.GetKey(KeyCode.D);
            if (_isMoving)
                stateMachine.SwitchState(PlayerStates.PLAYER_WALK);
            else
                stateMachine.SwitchState(PlayerStates.PLAYER_IDLE);
            if(Input.GetKeyDown(KeyCode.Space) && player.isGrounded) 
                stateMachine.SwitchState(PlayerStates.PLAYER_JUMP);
        }
    }
}
