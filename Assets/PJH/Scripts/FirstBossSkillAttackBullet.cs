using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossSkillAttackBullet : MonoBehaviour
{
    private float bulletSpeed;
    public Vector3 bulletDir;

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
        bulletDir = GameManager.Instance.firstBossSkillDir;
        transform.position = GameManager.Instance.bossTra.position + new Vector3(0f, 1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += bulletDir * bulletSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.name.Substring(0, 5) == "Angel")
                GameManager.Instance.ac.TakeDamage(GameManager.Instance.firstBossSkillAttackDamage);
            else if (other.gameObject.name.Substring(0, 5) == "Demon")
                GameManager.Instance.dc.TakeDamage(GameManager.Instance.firstBossSkillAttackDamage);
            else if (other.gameObject.name.Substring(0, 5) == "Fairy")
                GameManager.Instance.fc.TakeDamage(GameManager.Instance.firstBossSkillAttackDamage);

            GameObject obj = ObjectPool.Instance.PopFromPool("FirstBossSkillEffect");
            obj.transform.position = gameObject.transform.position;
            ObjectPool.Instance.PushToPool(gameObject.name, gameObject);
        }
    }
}
