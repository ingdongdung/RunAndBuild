using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class UserData
{
    public UserData(string userName, int money, int heart, bool firststage = false, bool middlestage = false, bool finalstage = false, Scene curScene = Scene.TitleScene, bool isClr = false)
    {
        this.userName = userName;
        this.money = money;
        this.heart = heart;
        this.isFirstStage = firststage;
        this.isMiddleStage = middlestage;
        this.isFinalStage = finalstage;
        this.currentScene = curScene;
        this.isClear = isClr;
    }

    public int money;
    public int heart;
    public string userName;
    public bool isFirstStage;
    public bool isMiddleStage;
    public bool isFinalStage;
    public Scene currentScene;
    public bool isClear;
    public List<PurchaseData> itemList = new List<PurchaseData>();
}

[System.Serializable]
public class PurchaseData
{
    public PurchaseData(string name, int count)
    {
        this.name = name;
        this.count = count;
    }
    public string name;
    public int count;
}
