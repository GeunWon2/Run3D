using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStateInit : GameState
{
    public GameObject menuUI;
    [SerializeField] private TextMeshProUGUI highscoreText;
    [SerializeField] private TextMeshProUGUI fishcntText;


    public override void Construct()
    {
        GameManager.Instance.ChangeCamera(GameCamera.Init);

        highscoreText.text = "Highscore : " + "TBD";
        fishcntText.text = "Fishcnt : " + "TBD";

        menuUI.SetActive(true);
    }

    public override void Destruct()
    {
        menuUI.SetActive(false);
    }

    public void OnPlayClick()
    {
        brain.ChangeState(GetComponent<GameStateGame>());
    }

    public void OnShopClick()
    {
        // brain.ChangeState(GetComponent<GameStateShop>());
        Debug.Log("Shop Click");
    }

}
