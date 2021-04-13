using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public const float MAXHP = 100f;

    Rigidbody rb;
    Vector3 dir;
    public float enemySpeed = 25f;
    public float enemyHp = 100f;
    public float enemyPower = 10f;

    bool onceForCoroutine;

    FairyController fc;
    AngelController ac;
    DemonicController dc;

    public Animator animator;
    public bool meetPlayer;
    public bool attackPlayer;

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
            
            //fc.animator.SetBool("Fly Forward", false);
            ac.animator.SetBool("Run", false);
            dc.animator.SetBool("Run", false);
        }

        if (!meetPlayer && !attackPlayer)
        {
            MoveToPlayer();
        }
        
        if (attackPlayer && !onceForCoroutine)
        {
            StartCoroutine(EnemyAttackTimer());
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
