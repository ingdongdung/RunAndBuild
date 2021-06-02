using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public UserData UserData => this.userData;

    public void SetUserData(UserData data)
    {
        this.userData = data;
    }

    public string ObjectToJson(object obj)
    {
        return JsonUtility.ToJson(obj);
    }

    public T JsonToObject<T>(string jsonData)
    {
        return JsonUtility.FromJson<T>(jsonData);
    }

    public bool UseCoin(int money)
    {
        int result = this.userData.money - money;
        if (result > 0)
        {
            this.userData.money = result;
            this.SaveJsonData(this.userData);
            return true;
        }
        else
        {
            return false;
            // [TODO] 토스트 팝업 or 컨펌 팝업
        }
    }

    public void AddCoin(int money)
    {
        this.userData.money += money;
        this.SaveJsonData(this.userData);
    }

    public void PurchaseItem(string itemName, int count = 1)
    {
        PurchaseData pData = this.userData.itemList.Find(l => l.name.CompareTo(itemName) == 0);
        if (pData != null)
        {
            pData.count++;
        }
        else
        {
            this.userData.itemList.Add(new PurchaseData(itemName, count));
        }
        this.SaveJsonData(this.userData);
    }

    public void SaveJsonData(UserData pData)
    {
        // Json변환
        string sJsonData = this.ObjectToJson(pData);


        // 파일 저장
        string sPath = string.Format(StringConst.ID_PATH, pData.userName);
        System.IO.FileInfo file = new System.IO.FileInfo(sPath);
        file.Directory.Create();
        File.WriteAllText(file.FullName, sJsonData);

        this.userData = pData;
    }

    public UserData LoadJsonData(string id)
    {
        string sPath = string.Format(StringConst.ID_PATH, id);
        System.IO.FileInfo loadfile = new System.IO.FileInfo(sPath);
        if (loadfile.Exists == false)
        {
            Debug.LogError("파일 없음");
            return null;
        }
        string sJsonData = File.ReadAllText(loadfile.FullName);
        Debug.Log(sJsonData);

        this.userData = this.JsonToObject<UserData>(sJsonData);

        return this.userData;
    }


    private UserData userData;

}
