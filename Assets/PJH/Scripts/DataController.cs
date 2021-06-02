using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

[Serializable]
public class GameData
{
    public int gold = 0;
    public bool firstStageClear = false;
    public bool middleStageClear = false;
    public bool finalStageClear = false;
}

public class DataController : Singleton<DataController>
{
    private string gameDataFileName = "/RunAndBuildSaveData.json";
    public GameData gameData;

    // Start is called before the first frame update 
    void Start()
    {
        LoadGameData();
    }

    public void LoadGameData()
    {
        string filePath = Application.dataPath + gameDataFileName;

        if (File.Exists(filePath))
        {
            Debug.Log("�ҷ����� ����");
            string FromJsonData = File.ReadAllText(filePath);
            gameData = JsonUtility.FromJson<GameData>(FromJsonData);
        }
        else
        {
            Debug.Log("���ο� ���� ����");
            gameData = new GameData();
        }
    }

    public void SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(gameData);
        string filePath = Application.dataPath + gameDataFileName;
        File.WriteAllText(filePath, ToJsonData);
        Debug.Log("���� ����");
    }

    // Update is called once per frame 
    void Update()
    {

    }

    private void OnApplicationQuit()
    {
        SaveGameData();
    }
}
