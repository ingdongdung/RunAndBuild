using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //가 출현할 위치를 담을 배열
    public Transform[] points;

    public float _elapsedTime = 0f;
    string treeName = "Tree";
    int treeNum = 3;


    // Start is called before the first frame update
    void Start()
    {
        // Hierarchy View의 Spawn Point를 찾아 하위에 있는 모든 Transform 컴포넌트를 찾아옴
        points = GameObject.Find("SpawnPoint").GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetTimer() >= 0.5f)
        {
            SetTimer();

            for (int i = 1; i < points.Length; ++i)
            {
                int randNum = Random.Range(0, treeNum);
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
                tree.transform.position = points[i].position + new Vector3(0f, 1f, 0f);
                
                treeName = "Tree";
            }
        }
    }

    float GetTimer()
    {
        return (_elapsedTime += Time.deltaTime);
    }

    void SetTimer()
    {
        _elapsedTime = 0f;
    }
}
