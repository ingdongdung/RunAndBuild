using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    public Transform[] targets = new Transform[3];
    public Transform target;
    public Transform cameraTrasnform;

    int order;

    public bool isBuilding;

    // Start is called before the first frame update
    void Start()
    {
        isBuilding = false;
        cameraTrasnform = GetComponent<Transform>();
        order = 1;
        target = targets[order];
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab) && !isBuilding)
        {
            ++order;
            if (order > 2)
                order = 0;
            target = targets[order];
        }

        if(isBuilding)
        {
            cameraTrasnform.position = new Vector3(9.39f, 25.31f, 4.33f);
            cameraTrasnform.rotation = Quaternion.Euler(80f, 0f, 0f);
        }
     
    }

    void LateUpdate()
    {
        if (!isBuilding)
        {
            cameraTrasnform.position = new Vector3(target.position.x,

              5f, target.position.z - 6.56f);


            cameraTrasnform.LookAt(target);
        }
    }
}
