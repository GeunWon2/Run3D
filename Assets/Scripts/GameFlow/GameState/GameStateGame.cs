using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStateGame : GameState
{
    public GameObject gameUI;
    [SerializeField] private TextMeshProUGUI fishCnt;
    [SerializeField] private TextMeshProUGUI socreCnt;
    public override void Construct()
    {
        GameManager.Instance.motor.ResumePlayer();
        GameManager.Instance.ChangeCamera(GameCamera.Game);

        GameStats.Instance.OnCollectFish += OnCollectFish;
        GameStats.Instance.OnScoreChange += OnScoreChange;

        gameUI.SetActive(true);
    }

    private void OnCollectFish(int amnCollected)
    {
        fishCnt.text = GameStats.Instance.FishToText();
    }

    private void OnScoreChange(float score)
    {
        socreCnt.text = GameStats.Instance.ScoreToText();

    }


    public override void UpdateState()
    {
        GameManager.Instance.worldGeneration.ScanPosition();
        GameManager.Instance.sceneChunkGeneration.ScanPosition();

    }

    public override void Destruct()
    {
        gameUI.SetActive(false);

        GameStats.Instance.OnCollectFish -= OnCollectFish;
        GameStats.Instance.OnScoreChange -= OnScoreChange;
    }
}
