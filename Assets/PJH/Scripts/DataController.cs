using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class DataController : MonoBehaviour
{
    static GameObject _container;
    static GameObject Container
    {
        get
        {
            return _container;
        }
    }
    static DataController _instance;
    public static DataController Instance
    {
        get
        {
            if (!_instance)
            {
                _container = new GameObject();
                _container.name = "DataController";
                _instance = _container.AddComponent(typeof(DataController)) as DataController;
                DontDestroyOnLoad(_container);
            }
            return _instance;
        }
    }

    public string gameDataFileName = "RunAndBuildData.json";
    public GameData gameData;
    public GameData _gameData
    {
        get
        {
            if (_gameData == null)
            {
                LoadGameData();
                SaveGameData();
            }
            return _gameData;
        }
    }

    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }

    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + gameDataFileName;

        if (File.Exists(filePath))
        {
            Debug.Log("불러오기 성공");
            string fromJsonData = File.ReadAllText(filePath);
            gameData = JsonUtility.FromJson<GameData>(fromJsonData);
        }
        else
        {
            Debug.Log("새로운 파일 생성");

            gameData = new GameData();
        }
    }

    public void SaveGameData()
    {
        string toJsonData = JsonUtility.ToJson(gameData);
        string filePath = Application.persistentDataPath + gameDataFileName;
        File.WriteAllText(filePath, toJsonData);
        Debug.Log("저장 성공");
    }

    private void OnApplicationQuit()
    {
        SaveGameData();
    }
}
