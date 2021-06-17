using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingdomBuilding : Singleton<KingdomBuilding>
{
    public FollowPlayerCamera camera;
    public bool bSelected;
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        camera = FindObjectOfType<FollowPlayerCamera>();
        bSelected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            if (camera.isBuilding)
                camera.isBuilding = false;
            else
            {
                camera.isBuilding = true;
                bSelected = false;
            }
        }


        if (camera.isBuilding && !bSelected)
        {
            Vector3 pos = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(pos);
            RaycastHit hit;

            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject.tag == "kingdomBuilding")
                    {
                        hit.transform.position = pos;
                        Building building = hit.transform.gameObject.GetComponent<Building>();
                        building.isClick = false;
                        building.isModifying = true;
                        bSelected = true;
                    }
                }
            }
        }

    }
}
