using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody rb;
    Vector3 dir;
    float enemySpeed = 15f;

    FairyController fc;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        dir = new Vector3(0f, 0f, -1f);
        transform.Rotate(new Vector3(0f, 180f, 0f));

        fc = GameObject.Find("Fairy").GetComponent<FairyController>();
    }

    private void OnEnable()
    {
        
    }

    private void OnBecameInvisible()
    {
        ObjectPool.Instance.PushToPool(gameObject.name, gameObject);
    }

    private void Update()
    {
        transform.LookAt(fc.transform);
        transform.position += dir * enemySpeed * Time.deltaTime;
    }
}
