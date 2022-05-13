using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GameStateShop : GameState
{
    public GameObject shopUI;
    public TextMeshProUGUI totalFish;
    public TextMeshProUGUI currentHatName;
    public HatLogic hatLogic;
    private bool isInit = false;

    public GameObject hatPrefab;
    public Transform hatContainer;
    private Hat[] hats;

    public override void Construct()
    {
        GameManager.Instance.ChangeCamera(GameCamera.Shop);
        hats = Resources.LoadAll<Hat>("Hat");
        totalFish.text = SaveManager.Instance.save.Fish.ToString("000");
        shopUI.SetActive(true);

        if (!isInit)
        {        
            currentHatName.text = hats[SaveManager.Instance.save.CurrentHatIndex].ItemName;
            PopulateShop();
            isInit = true;
        }

     
    }

    public override void Destruct()
    {
        shopUI.SetActive(false);
    }

    private void PopulateShop()
    {
        for (int i = 0; i < hats.Length; i++)
        {
            int index = i;
            GameObject go = Instantiate(hatPrefab, hatContainer) as GameObject;

            go.GetComponent<Button>().onClick.AddListener(() => OnHatClick(index));
            go.transform.GetChild(0).GetComponent<Image>().sprite = hats[index].Thumbnail;
            go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = hats[index].ItemName;

            if (SaveManager.Instance.save.UnlockedHatFlag[i] == 0)
            {
                go.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = hats[index].ItemPrice.ToString();
            }
            else
            {
                go.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text ="";
            }
        }
    }

    private void OnHatClick(int i)
    {
        if(SaveManager.Instance.save.UnlockedHatFlag[i] == 1)
        {
            SaveManager.Instance.save.CurrentHatIndex = i;
            currentHatName.text = hats[i].ItemName;
            hatLogic.SelectHat(i);
            SaveManager.Instance.Save();
        }
        else if(hats[i].ItemPrice <= SaveManager.Instance.save.Fish)
        {
            SaveManager.Instance.save.Fish -= hats[i].ItemPrice;
            SaveManager.Instance.save.UnlockedHatFlag[i] = 1;
            SaveManager.Instance.save.CurrentHatIndex = i;
            currentHatName.text = hats[i].ItemName;
            hatLogic.SelectHat(i);
            totalFish.text = SaveManager.Instance.save.Fish.ToString("000");
            SaveManager.Instance.Save();
            hatContainer.GetChild(i).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";

        }
        else
        {
            Debug.Log("Not enugh fish");
        }
      
    }

    public void OnHomeClick()
    {
        brain.ChangeState(GetComponent<GameStateInit>());
    }

}
