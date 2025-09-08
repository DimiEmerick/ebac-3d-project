using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using Ebac.StateMachine;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    public TMP_Text FSMTutorial;
    public enum GameStates
    {
        INTRO,
        GAMEPLAY,
        PAUSE,
        WIN,
        LOSE
    }

    public StateMachine<GameStates> stateMachine;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        ForceSwitchStates();
    }

    public void Init()
    {
        stateMachine = new StateMachine<GameStates>();
        stateMachine.Init();
        stateMachine.RegisterStates(GameStates.INTRO, new GMStateIntro());
        stateMachine.RegisterStates(GameStates.GAMEPLAY, new GMStateGameplay());
        stateMachine.RegisterStates(GameStates.PAUSE, new StateBase());
        stateMachine.RegisterStates(GameStates.WIN, new StateBase());
        stateMachine.RegisterStates(GameStates.LOSE, new StateBase());

        stateMachine.SwitchState(GameStates.INTRO);
    }

    public void ForceSwitchStates()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            FSMTutorial.enabled = false;
            if (GameManager.Instance != null && GameManager.Instance.stateMachine.CurrentStateName == GameManager.GameStates.INTRO)
                stateMachine.SwitchState(GameStates.GAMEPLAY);
            else if (GameManager.Instance != null && GameManager.Instance.stateMachine.CurrentStateName == GameManager.GameStates.GAMEPLAY)
                stateMachine.SwitchState(GameStates.INTRO);

        }
    }
}
