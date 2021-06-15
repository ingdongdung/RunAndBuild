using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : KingdomBuilding
{
    public GameObject target;
    public bool isClick;

    // Start is called before the first frame update
    void Start()
    {
        isClick = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isClick)
        {
            Vector3 pos = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(pos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                pos = hit.transform.position;
                target.transform.position = pos;

                if(Input.GetMouseButtonDown(0))
                {
                    target.GetComponent<BoxCollider>().enabled = true;
                    isClick = true;
                    FollowPlayerCamera camera = FindObjectOfType<FollowPlayerCamera>();
                    camera.isBuilding = false;
                }

                if(Input.mouseScrollDelta.y > 0)
                {
                    target.transform.Rotate(0f, 90f, 0f);
                }
                else if(Input.mouseScrollDelta.y < 0)
                {
                    target.transform.Rotate(0f, -90f, 0f);
                }

            }
        }
    }
}
