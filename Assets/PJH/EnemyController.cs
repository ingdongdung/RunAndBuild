using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public const float MAXHP = 100f;

    public float enemySpeed;
    public float enemyHp;
    public float enemyPower;

    Rigidbody rb;
    Vector3 dir;

    FairyController fc;
    AngelController ac;
    DemonicController dc;

    bool onceForCoroutine;

    public Animator animator;
    public bool meetPlayer;
    public bool attackPlayer;

    public Coroutine enemyAttackTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
        fc = GameObject.Find("Fairy").GetComponent<FairyController>();
        ac = GameObject.Find("Angel").GetComponent<AngelController>();
        dc = GameObject.Find("Demonic").GetComponent<DemonicController>();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Walk", false);
        animator.SetBool("Run", true);

        enemySpeed = 25f;
        enemyHp = 100f;
        enemyPower = 10f;

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

            fc.MeetEnemy();
            ac.MeetEnemy();
            dc.MeetEnemy();
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
        return Vector3.Distance(transform.position, fc.transform.position);
    }

    private void MoveToPlayer()
    {
        transform.LookAt(fc.transform);
        transform.position += dir * enemySpeed * Time.deltaTime;
    }

    public void TakeDamage(float damage)
    {
        enemyHp -= damage;
        //print(transform.name + " was damaged of " + damage);
    }

    IEnumerator EnemyAttackTimer()
    {
        while (true)
        {
            if (transform.name == "Enemy01")
            {
                animator.Play("Melee Left Attack 01");
            }
            else if (transform.name == "Enemy02")
            {
                animator.Play("Melee Right Attack 02");
            }
            else if (transform.name == "Enemy03")
            {
                animator.Play("Melee Right Attack 03");
            }
            else if (transform.name == "Enemy04")
            {
                animator.Play("Melee Right Attack 01");
            }
            yield return new WaitForSeconds(2);
        }
    }
}
