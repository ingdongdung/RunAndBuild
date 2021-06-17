using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingdomScene : Singleton<KingdomScene>
{
    public void OnClickInGameScene()
    {
        // SceneLoadManager.Instance.MoveScene(Scene.InGame);
        this.selectStage.gameObject.SetActive(true);
    }

    public void OnClickShop()
    {
        shopContent.gameObject.SetActive(true);
    }

    public void Refresh()
    {
        this.topUI.SetData(DataManager.Instance.LoadJsonData(DataManager.Instance.UserData.userName));
    }

    private void Start()
    {
        buildingObjPool = FindObjectOfType<BuildingObjPool>();

        Debug.Log(DataManager.Instance.UserData.buildingList + "_____");

        foreach (BuildingData data in DataManager.Instance.UserData.buildingList)
        {

            buildingObjPool.CreateObject(data.name, data.x, data.y, data.z);
        }
        this.Refresh();
    }

    [SerializeField] SelectStage selectStage;
    [SerializeField] ShopContent shopContent;
    [SerializeField] TopUI topUI;
    private BuildingObjPool buildingObjPool;
}
