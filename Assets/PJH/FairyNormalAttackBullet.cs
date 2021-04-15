using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyNormalAttackBullet : MonoBehaviour
{
    FairyController fc;
    float bulletSpeed;
    Vector3 bulletDir;

    private void Awake()
    {
        fc = GameObject.Find("Fairy").GetComponent<FairyController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        bulletSpeed = 5f;
        bulletDir = fc.fairyDir;
        transform.position = fc.transform.position + new Vector3(0f, 1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += bulletDir * bulletSpeed * Time.deltaTime;
    }
}
