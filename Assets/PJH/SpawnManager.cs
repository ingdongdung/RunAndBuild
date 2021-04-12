﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // 오브젝트가 출현할 위치를 담을 배열
    public Transform[] points;

    public float _elapsedTimeForTree = 0f;
    public float _elapsedTimeForEnemy = 0f;
    public float _elapsedTimeForBoss = 0f;
    string treeName = "Tree";
    string enemyName = "Enemy";
    string bossName = "Boss";
    int treeCount = 3;
    int enemyCount = 4;
    

    // Start is called before the first frame update
    void Start()
    {
        // Hierarchy View의 Spawn Point를 찾아 하위에 있는 모든 Transform 컴포넌트를 찾아옴
        points = GameObject.Find("SpawnPoint").GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // 나무
        if (GetTreeTimer() >= 0.5f)
        {
            SetTreeTimer();

            foreach(var point in points)
            {
                if (point.name.Substring(0, 4) == "tree")
                {
                    int randNum = Random.Range(0, treeCount);
                    switch (randNum)
                    {
                        case 0:
                            treeName += "01";
                            break;
                        case 1:
                            treeName += "02";
                            break;
                        case 2:
                            treeName += "03";
                            break;
                    }
                    GameObject tree = ObjectPool.Instance.PopFromPool(treeName);
                    tree.transform.position = point.position + new Vector3(0f, 1f, 0f);
                    treeName = "Tree";
                }
            }
        }

        // 몹 & 보스
        if (GetEnemyTimer() >= 3.0f)
        {
            SetEnemyTimer();

            foreach (var point in points)
            {
                if (point.name.Substring(0, 6) == "enemyP")    // 몹
                {
                    int randNum = Random.Range(0, enemyCount);
                    switch (randNum)
                    {
                        case 0:
                            enemyName += "01";
                            break;
                        case 1:
                            enemyName += "02";
                            break;
                        case 2:
                            enemyName += "03";
                            break;
                        case 3:
                            enemyName += "04";
                            break;
                    }
                    GameObject enemy = ObjectPool.Instance.PopFromPool(enemyName);
                    enemy.transform.position = point.position + new Vector3(0f, 1f, 0f);
                    enemyName = "Enemy";
                }
                else if (point.name.Substring(0, 6) == "enemyB")    // 보스
                {

                }
            }
        }
    }

    float GetTreeTimer()
    {
        return (_elapsedTimeForTree += Time.deltaTime);
    }

    void SetTreeTimer()
    {
        _elapsedTimeForTree = 0f;
    }

    float GetEnemyTimer()
    {
        return (_elapsedTimeForEnemy += Time.deltaTime);
    }

    void SetEnemyTimer()
    {
        _elapsedTimeForEnemy = 0f;
    }
}
