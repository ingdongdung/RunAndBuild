using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingObjPool : MonoBehaviour
{
    // Start is called before the first frame update
    Queue<GameObject> poolingObjQueue = new Queue<GameObject>();


    void Start()
    {
    }

    public GameObject CreateObject(string findTag)
    {
        Debug.Log("CreateObject111");
        Vector3 position = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            position = hit.transform.position;
        }
        Quaternion rotation = new Quaternion(0f, 0f, 0f, 1f);
        GameObject target = GameObject.Find("KingdomBuilding").transform.Find(findTag).gameObject;
        GameObject obj = Instantiate(target, position, rotation);
        obj.SetActive(true);
        poolingObjQueue.Enqueue(obj);

        FollowPlayerCamera camera = FindObjectOfType<FollowPlayerCamera>();
        camera.isBuilding = true;

        DataManager.Instance.UserData.buildingList.Add(new BuildingData(findTag, position.x, position.y, position.z));
        DataManager.Instance.SaveJsonData(DataManager.Instance.UserData);
        return obj;

    }

    public GameObject CreateObject(string name, float x, float y, float z)
    {
        Debug.Log("CreateObject");
        Vector3 position = new Vector3(x, y, z);
        Quaternion rotation = new Quaternion(0f, 0f, 0f, 1f);
        GameObject target = GameObject.Find("KingdomBuilding").transform.Find(name).gameObject;
        GameObject obj = Instantiate(target, position, rotation);
       
        obj.SetActive(true);
        obj.GetComponent<Building>().isClick = true;
        obj.GetComponent<Building>().setColor();
        poolingObjQueue.Enqueue(obj);

        DataManager.Instance.UserData.buildingList.Add(new BuildingData(name, position.x, position.y, position.z));
        return obj;

    }

    // Update is called once per frame
    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.F1))
        //{
        //    CreateObject("WaterTower");
        //}
        //else if (Input.GetKeyDown(KeyCode.F2))
        //{
        //    CreateObject("House_1");
        //}
        //else if (Input.GetKeyDown(KeyCode.F3))
        //{
        //    CreateObject("House_2");
        //}
    }
}
