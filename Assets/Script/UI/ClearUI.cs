using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearUI : MonoBehaviour
{
    // Start is called before the first frame update
    public void SetClearUI(bool isClear, int stageNumber)
    {
        this.isClear = isClear;
        Debug.Log("Start Clear UI");
        switch (stageNumber)
        {
            case 1:
                this.stageText.text = "FIRST STAGE";
                currentScene = Scene.FirstStage;
                break;
            case 2:
                this.stageText.text = "MIDDLE STAGE";
                currentScene = Scene.MiddleStage;
                break;
            case 3:
                this.stageText.text = "FINAL STAGE";
                currentScene = Scene.FinalStage;
                break;
        }
        failObj.SetActive(!this.isClear);
        clearObj.SetActive(this.isClear);
        foreach (CharacterUI charUI in characterList)
        {
            if (this.isClear)
            {
                charUI.ActionClear();
            }
            else
            {
                charUI.ActionFail();
            }
        }

        this.homeBtn.SetActive(true);
        this.retryBtn.SetActive(!isClear);
        this.nextBtn.SetActive(isClear);
        this.rewardUI.SetActive(isClear);
    }

    public void OnClickHomeBtn()
    {
        SceneLoadManager.Instance.MoveScene(Scene.KingdomScene);
        if (this.isClear)
        {
            DataManager.Instance.AddCoin(10);
            DataManager.Instance.SaveJsonData(DataManager.Instance.UserData);
        }

    }

    public void OnClickRetryBtn()
    {
        SceneLoadManager.Instance.MoveScene(currentScene);
        if (this.isClear)
        {
            DataManager.Instance.AddCoin(10);
            DataManager.Instance.SaveJsonData(DataManager.Instance.UserData);
        }
    }

    public void OnClickNextBtn()
    {

        SceneLoadManager.Instance.MoveScene(currentScene);
    }

    [SerializeField] Text stageText;
    [SerializeField] GameObject clearObj;
    [SerializeField] GameObject failObj;
    [SerializeField] GameObject rewardUI;
    [SerializeField] GameObject homeBtn;
    [SerializeField] GameObject retryBtn;
    [SerializeField] GameObject nextBtn;
    [SerializeField] List<CharacterUI> characterList;

    private Scene currentScene;
    private bool isClear;
}
