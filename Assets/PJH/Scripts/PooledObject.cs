using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PooledObject
{
    [SerializeField]
    private List<GameObject> poolList = new List<GameObject>();
    // 생성한 객체들을 저장할 리스트

    // 객체를 검색할 때 사용할 이름
    public string poolItemName = string.Empty;
    // 오브젝트 풀에 저장할 프리팹
    public GameObject prefab = null;
    // 초기화할 때 생성할 객체의 수
    public int poolCount = 0;
    
    public void Initialize(Transform parent = null)
    {
        for (int ix = 0; ix < poolCount; ++ix)
            poolList.Add(CreateItem(parent));
    }

    public void PushToPool(GameObject item, Transform parent = null)
    {
        item.transform.SetParent(parent);
        item.SetActive(false);
        poolList.Add(item);
    }

    public GameObject PopFromPool(Transform parent = null)
    {
        if (poolList.Count == 0)
            poolList.Add(CreateItem(parent));

        GameObject item = poolList[0];
        poolList.RemoveAt(0);
        item.SetActive(true);
        return item;
    }

    private GameObject CreateItem(Transform parent = null)
    {
        GameObject item = Object.Instantiate(prefab) as GameObject;
        item.name = poolItemName;
        item.transform.SetParent(parent);
        item.SetActive(false);
        return item;
    }
}