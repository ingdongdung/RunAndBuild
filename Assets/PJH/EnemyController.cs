using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody rb;
    Vector3 dir;
    float enemySpeed = 25f;

    FairyController fc;

    public Animator animator;
    public bool meetPlayer;
    public float _elapsedTimeForEnemy = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        dir = new Vector3(0f, 0f, -1f);
        transform.Rotate(new Vector3(0f, 180f, 0f));
        

        fc = GameObject.Find("Fairy").GetComponent<FairyController>();
        meetPlayer = false;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Walk", false);
        animator.SetBool("Run", true);
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
        if (DistanceToPlayer() <= 5f)
        {
            meetPlayer = true;
            animator.SetBool("Run", false);
        }

        if (!meetPlayer)
        {
            MoveToPlayer();
        }
        else
        {
            AttackToPlayer();
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

    private void AttackToPlayer()
    {
        SetEnemyTimer();

        if (GetEnemyTimer() <= 3.5f)
        {
            if (transform.name == "Enemy01")
            {
                animator.SetBool("Melee Right Attack 01", true);
            }
            else if (transform.name == "Enemy02")
            {
                animator.SetBool("Melee Right Attack 02", true);
            }
            else if (transform.name == "Enemy03")
            {
                animator.SetBool("Melee Right Attack 03", true);
            }
            else if (transform.name == "Enemy04")
            {
                animator.SetBool("Melee Left Attack 01", true);
            }
        }
    }

    float GetEnemyTimer()
    {
        return (_elapsedTimeForEnemy += Time.deltaTime);
    }

    void SetEnemyTimer()
    {
        _elapsedTimeForEnemy = 0f;
    }
}
