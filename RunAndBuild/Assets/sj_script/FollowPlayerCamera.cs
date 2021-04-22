using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    public Transform target;
    private Transform cameraTrasnform;

    // Start is called before the first frame update
    void Start()
    {
        cameraTrasnform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        cameraTrasnform.position = new Vector3(target.position.x,
            cameraTrasnform.position.y, target.position.z - 6.56f);

        cameraTrasnform.LookAt(target);
    }
}
