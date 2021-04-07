using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    Rigidbody rb;
    Vector3 dir;

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
        print("invisible");
        ObjectPool.Instance.PushToPool(gameObject.name, gameObject);
    }

    private void Update()
    {
        transform.position += dir * 25f * Time.deltaTime;
    }
}
