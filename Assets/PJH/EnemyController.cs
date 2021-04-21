using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public const float MAXHP = 100f;

    public float enemySpeed;
    public float enemyHp;
    public float enemyPower;

    Rigidbody rb;
    Vector3 dir;

    bool onceForCoroutine;

    Image hpBarImage;

    public Animator animator;
    public bool meetPlayer;
    public bool attackPlayer;

    public Coroutine enemyAttackTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Walk", false);
        animator.SetBool("Run", true);

        enemySpeed = 25f;
        enemyHp = 100f;
        enemyPower = 3f;

        enemyAttackTimer = null;
    }

    private void OnEnable()
    {
        // 재사용 시 초기화되어야 할 것들
        dir = new Vector3(0f, 0f, -1f);
        transform.Rotate(new Vector3(0f, 180f, 0f));
        meetPlayer = false;
        attackPlayer = false;
        onceForCoroutine = false;
    }

    private void OnDisable()
    {
        if (enemyAttackTimer != null)
        {
            StopCoroutine(enemyAttackTimer);
        }
    }

    private void OnBecameInvisible()
    {
        ObjectPool.Instance.PushToPool(gameObject.name, gameObject);
    }

    private void Update()
    {
        if (DistanceToPlayer() <= 5f && !meetPlayer && !attackPlayer)
        {
            meetPlayer = true;
            attackPlayer = true;
            animator.SetBool("Run", false);

            GameManager.Instance.fc.MeetEnemy();
            GameManager.Instance.ac.MeetEnemy();
            GameManager.Instance.dc.MeetEnemy();
        }

        if (!meetPlayer && !attackPlayer)
        {
            MoveToPlayer();
        }
        
        if (attackPlayer && !onceForCoroutine)
        {
            enemyAttackTimer = StartCoroutine(EnemyAttackTimer());
            onceForCoroutine = true;
        }
    }

    private float DistanceToPlayer()
    {
        return Vector3.Distance(transform.position, GameManager.Instance.fc.transform.position);
    }

    private void MoveToPlayer()
    {
        transform.LookAt(GameManager.Instance.fc.transform);
        transform.position += dir * enemySpeed * Time.deltaTime;
    }

    public void TakeDamage(float damage)
    {
        enemyHp -= damage;
        animator.Play("Take Damage");
        //print(transform.name + " was damaged of " + damage);
    }

    private void DamageDistribution()
    {
        // angel - demonic - fairy

        if (GameManager.Instance.ac.angelHp > 0f)
        {
            GameManager.Instance.ac.TakeDamage(enemyPower);
        }
        else if (GameManager.Instance.dc.demonicHp > 0f)
        {
            GameManager.Instance.dc.TakeDamage(enemyPower);
        }
        else if (GameManager.Instance.fc.fairyHp > 0f)
        {
            GameManager.Instance.fc.TakeDamage(enemyPower);
        }
    }

    IEnumerator EnemyAttackTimer()
    {
        while (true)
        {
            if (transform.name == "Enemy01")
            {
                animator.Play("Melee Left Attack 01");
                DamageDistribution();
            }
            else if (transform.name == "Enemy02")
            {
                animator.Play("Melee Right Attack 02");
                DamageDistribution();
            }
            else if (transform.name == "Enemy03")
            {
                animator.Play("Melee Right Attack 03");
                DamageDistribution();
            }
            else if (transform.name == "Enemy04")
            {
                animator.Play("Melee Right Attack 01");
                DamageDistribution();
            }
            yield return new WaitForSeconds(2);
        }
    }
}
