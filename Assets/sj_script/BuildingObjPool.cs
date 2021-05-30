using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingObjPool : MonoBehaviour
{
    // Start is called before the first frame update

    public static BuildingObjPool Instance;

    [SerializeField]
    private GameObject poolingObjPrefab;

    Queue<Building> poolingObjQueue = new Queue<Building>();

    
    void Start()
    {
        Instance = this;
        CreateObject();
    }

    public Building CreateObject()
    {
        var newObj = Instantiate(poolingObjPrefab).GetComponent<Building>();
        poolingObjQueue.Enqueue(newObj);

        return newObj;
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
