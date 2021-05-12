using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 dir;
    private bool onceForCoroutine;
    private Image hpBarImage;
    private GameObject hb;

    public const float MAXHP = 300f;
    public const float SPEED = 25f;
    public float bossSpeed;
    public float bossHp;
    public float bossPower;

    public Animator animator;
    public bool meetPlayer;
    public bool attackPlayer;
    public Coroutine bossAttackTimer;
    public Coroutine bossDeadTimer;

    public Vector3 hpBarOffset = new Vector3(0f, 2.2f, 0f);

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        bossSpeed = SPEED;
        bossHp = MAXHP;
        bossPower = 50f;

        bossAttackTimer = null;
    }

    private void OnEnable()
    {
        animator.SetBool("Walk", false);
        animator.SetBool("Run", true);

        // 재사용 시 초기화되어야 할 것들
        dir = new Vector3(0f, 0f, -1f);
        transform.Rotate(new Vector3(0f, 180f, 0f));
        meetPlayer = false;
        attackPlayer = false;
        onceForCoroutine = false;
        bossHp = MAXHP;
        bossSpeed = SPEED;

        SetHpBar();
    }

    private void OnDisable()
    {
        if (bossAttackTimer != null)
        {
            StopCoroutine(bossAttackTimer);
        }
        if (bossDeadTimer != null)
        {
            StopCoroutine(bossDeadTimer);
        }

        if (hb != null)
        {
            if (hb.transform.parent.name == "UI Canvas for enemy")
            {
                hb.transform.parent = ObjectPool.Instance.gameObject.transform;
                ObjectPool.Instance.PushToPool(hb.name, hb);
            }
            else
            {
                ObjectPool.Instance.PushToPool(hb.name, hb);
            }
        }
    }

    private void Update()
    {
        if (DistanceToPlayer() <= 20f && !meetPlayer && !attackPlayer)
        {
            bossSpeed -= 0.04f;
        }

        if (DistanceToPlayer() <= 5f && !meetPlayer && !attackPlayer)
        {
            meetPlayer = true;
            attackPlayer = true;
            animator.SetBool("Run", false);

            GameManager.Instance.meetEnemy = true;
        }

        if (!meetPlayer && !attackPlayer)
        {
            MoveToPlayer();
        }

        if (attackPlayer && !onceForCoroutine)
        {
            bossAttackTimer = StartCoroutine(BossAttackTimer());
            onceForCoroutine = true;
        }
    }

    private float DistanceToPlayer()
    {
        return Vector3.Distance(transform.position, GameManager.Instance.transform.position);
    }

    private void MoveToPlayer()
    {
        transform.LookAt(GameManager.Instance.fc.transform);
        transform.position += dir * bossSpeed * Time.deltaTime;
    }

    public void TakeDamage(float damage)
    {
        bossHp -= damage;

        if (bossHp > 0f)
            animator.Play("Take Damage");

        hpBarImage.fillAmount = bossHp / MAXHP;

        CheckToDead();
    }

    private void CheckToDead()
    {
        if (bossHp <= 0f)
        {
            animator.Play("Die");
            bossDeadTimer = StartCoroutine(CheckToDeadTimer());
        }
    }

    IEnumerator CheckToDeadTimer()
    {
        yield return new WaitForSeconds(1.0f);

        ObjectPool.Instance.PushToPool(gameObject.name, gameObject);
    }

    private void DamageDistribution()
    {
        // angel - demonic - fairy

        if (GameManager.Instance.ac && GameManager.Instance.ac.angelHp > 0f)
        {
            GameManager.Instance.ac.TakeDamage(bossPower);
        }
        else if (GameManager.Instance.dc && GameManager.Instance.dc.demonicHp > 0f)
        {
            GameManager.Instance.dc.TakeDamage(bossPower);
        }
        else if (GameManager.Instance.fc && GameManager.Instance.fc.fairyHp > 0f)
        {
            GameManager.Instance.fc.TakeDamage(bossPower);
        }
    }

    private void SetHpBar()
    {
        hb = ObjectPool.Instance.PopFromPool("EnemyHpBar");
        hb.transform.parent = GameManager.Instance.uiCanvas.GetComponent<Transform>();
        hpBarImage = hb.GetComponentsInChildren<Image>()[1];
        hpBarImage.fillAmount = MAXHP;

        var hpBar = hb.GetComponent<EnemyHpBarController>();
        hpBar.targetTransform = gameObject.transform;
        hpBar.offset = hpBarOffset;
    }

    IEnumerator BossAttackTimer()
    {
        while (true)
        {
            if (bossHp > 0f)
            {
                if (transform.name == "FirstBoss")
                {
                    animator.Play("Cast Spell 01");
                    DamageDistribution();
                }
                else if (transform.name == "MidBoss")
                {
                    animator.Play("Cast Spell 01");
                    DamageDistribution();
                }
                else if (transform.name == "FinalBoss")
                {
                    animator.Play("Melee Right Attack 03");
                    DamageDistribution();
                }
            }
            yield return new WaitForSeconds(2);
        }
    }
}
