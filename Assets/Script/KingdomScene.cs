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

    public void OnClickQuit()
    {
        Debug.Log("눌리니?");
        Application.Quit();
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
            Vector3 pos = new Vector3(data.x, data.y, data.z);
            // GameObject target = GameObject.Find("sj_asset/Prefab_Load/" + data.name + "_load").gameObject;

            //GameObject obj = Resources.Load(objs.Find(o => o.name == data.name) as GameObject;
            Debug.Log("data nam e: " + data.name.Substring(0, data.name.Length - 7));

            GameObject findObj = objs.Find(o => o.name == data.name.Substring(0, data.name.Length - 7));
            GameObject obj = Instantiate(findObj, pos, Quaternion.identity);
            // obj.transform.localPosition = pos;
        }
        this.Refresh();
    }

    [SerializeField] SelectStage selectStage;
    [SerializeField] ShopContent shopContent;
    [SerializeField] TopUI topUI;
    private BuildingObjPool buildingObjPool;
    [SerializeField] List<GameObject> objs;
}
