using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    Rigidbody rb;
    Vector3 dir;
    float treeSpeed = 35f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        dir = new Vector3(0f, 0f, -1f);
    }

    private void OnEnable()
    {
        //rb.AddForce(dir * 100f);
    }

    private void OnBecameInvisible()
    {
        ObjectPool.Instance.PushToPool(gameObject.name, gameObject);
    }

    private void Update()
    {
        transform.position += dir * treeSpeed * Time.deltaTime;
    }
}
