using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNormalAttackBullet : MonoBehaviour
{
    private float bulletSpeed;
    private Vector3 bulletDir;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        bulletSpeed = 7.5f;
    }

    private void OnEnable()
    {
        bulletDir = GameManager.Instance.bossDir;
        transform.position = GameManager.Instance.bossTra.position + new Vector3(0f, 1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += bulletDir * bulletSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);

        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log(other.gameObject.name);

            if (other.gameObject.name.Substring(0, 5) == "Angel")
                GameManager.Instance.ac.TakeDamage(GameManager.Instance.boss0102Power);
            else if (other.gameObject.name.Substring(0, 5) == "Demon")
                GameManager.Instance.dc.TakeDamage(GameManager.Instance.boss0102Power);
            else if (other.gameObject.name.Substring(0, 5) == "Fairy")
                GameManager.Instance.fc.TakeDamage(GameManager.Instance.boss0102Power);

            GameObject obj = ObjectPool.Instance.PopFromPool("BulletEffect");
            obj.transform.position = gameObject.transform.position;
            ObjectPool.Instance.PushToPool(gameObject.name, gameObject);
        }
    }
}
