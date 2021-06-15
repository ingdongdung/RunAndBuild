using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectStage : MonoBehaviour
{
    public void OnClickFirstStage()
    {
        SceneLoadManager.Instance.MoveScene(Scene.FirstStage);
    }

    public void OnClickMiddleStage()
    {
        SceneLoadManager.Instance.MoveScene(Scene.MiddleStage);
    }

    public void OnClickFinalStage()
    {
        SceneLoadManager.Instance.MoveScene(Scene.FinalStage);
    }

    public void OnClickClose()
    {
        this.gameObject.SetActive(false);
    }

    private void OnEnable() {
        this.FirstStageBtn.SetActive(!DataManager.Instance.UserData.isFirstStage);
        this.FirstStageDimmedBtn.SetActive(DataManager.Instance.UserData.isFirstStage);

        this.MiddleStageBtn.SetActive(!DataManager.Instance.UserData.isMiddleStage);
        this.MiddleStageDimmedBtn.SetActive(DataManager.Instance.UserData.isMiddleStage);

        this.FinalStageBtn.SetActive(!DataManager.Instance.UserData.isFinalStage);
        this.FinalStageDimmedBtn.SetActive(DataManager.Instance.UserData.isFinalStage);
    }

    [SerializeField] private GameObject FirstStageBtn;
    
    [SerializeField] private GameObject MiddleStageBtn;
    
    [SerializeField] private GameObject FinalStageBtn;

     [SerializeField] private GameObject FirstStageDimmedBtn;
    
    [SerializeField] private GameObject MiddleStageDimmedBtn;
    
    [SerializeField] private GameObject FinalStageDimmedBtn;
}
