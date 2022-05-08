using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateDeath : GameState
{
    public override void Construct()
    {
        GameManager.Instance.motor.PausePlayer();
    }
    public override void UpdateState()
    {
        if(InputManager.Instance.SwipeDown)
        {
            ToMenu();
        }

        if(InputManager.Instance.SwipeUp)
        {
            ResumeGame();
        }
    }

    private void ResumeGame()
    {
        GameManager.Instance.motor.RespawnPlayer();
        brain.ChangeState(GetComponent<GameStateGame>());
    }

    public void ToMenu()
    {
        brain.ChangeState(GetComponent<GameStateInit>());
    }
}
