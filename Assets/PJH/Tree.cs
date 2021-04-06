using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : Poolable
{
    Rigidbody rigidbody;
    Vector3 dir;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        dir = transform.right;
    }

    private void OnEnable()
    {
        rigidbody.AddForce(dir * 10f);
    }

    private void OnBecameInvisible()
    {
        Push();
    }
}
