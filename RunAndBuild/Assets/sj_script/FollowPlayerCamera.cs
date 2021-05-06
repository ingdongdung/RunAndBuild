using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    public Transform[] targets = new Transform[3];
    public Transform target;
    private Transform cameraTrasnform;

    int order;

    // Start is called before the first frame update
    void Start()
    {
        cameraTrasnform = GetComponent<Transform>();
        order = 1;
        target = targets[order];
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            ++order;
            if (order > 2)
                order = 0;
            target = targets[order];
        }
     
    }

    void LateUpdate()
    {
        cameraTrasnform.position = new Vector3(target.position.x,
            5f, target.position.z - 6.56f);


        cameraTrasnform.LookAt(target);
    }
}
