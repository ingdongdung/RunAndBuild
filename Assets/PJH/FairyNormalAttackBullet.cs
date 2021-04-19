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
        bulletSpeed = 7.5f;
    }

    private void OnEnable()
    {
        bulletDir = fc.fairyDir;
        transform.position = fc.transform.position + new Vector3(0f, 1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += bulletDir * bulletSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyController>().TakeDamage(fc.fairyPower);
            GameObject obj = ObjectPool.Instance.PopFromPool("FairyBulletEffect");
            obj.transform.position = gameObject.transform.position;
            ObjectPool.Instance.PushToPool(gameObject.name, gameObject);
        }
    }
}
