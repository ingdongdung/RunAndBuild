using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingdomScene : Singleton<KingdomScene>
{
    public void OnClickInGameScene()
    {
        SceneLoadManager.Instance.MoveScene(Scene.InGame);
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
        this.Refresh();
    }

    [SerializeField] ShopContent shopContent;
    [SerializeField] TopUI topUI;
}
