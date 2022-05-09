using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameStateDeath : GameState
{
    public GameObject deathUI;
    [SerializeField] private TextMeshProUGUI highScore;
    [SerializeField] private TextMeshProUGUI currentScore;
    [SerializeField] private TextMeshProUGUI fishTotal;
    [SerializeField] private TextMeshProUGUI currentFish;

    [SerializeField] private Image completionCircle;
    public float timeToDecision = 2.5f;
    private float deathTime;
    public override void Construct()
    {
        GameManager.Instance.motor.PausePlayer();

        deathTime = Time.time;
        deathUI.SetActive(true);
        completionCircle.gameObject.SetActive(true);



        highScore.text = "HighScore : TBD";
        currentScore.text = "?1234";
        fishTotal.text = "Total : TBD";
        currentFish.text = "?x20";
    }

    public override void Destruct()
    {
        deathUI.SetActive(false);
    }
    public override void UpdateState()
    {
        float ratio = (Time.time - deathTime) / timeToDecision;
        completionCircle.color = Color.Lerp(Color.green, Color.red, ratio);
        completionCircle.fillAmount = 1 - ratio;

        if(ratio > 1)
        {
            completionCircle.gameObject.SetActive(false);
        }

    }

    public void ResumeGame()
    {
        brain.ChangeState(GetComponent<GameStateGame>());
        GameManager.Instance.motor.RespawnPlayer();

    }

    public void ToMenu()
    {
        brain.ChangeState(GetComponent<GameStateInit>());

        GameManager.Instance.motor.ResetPalyer();
        GameManager.Instance.worldGeneration.ResetWorld();
      
    }
}
