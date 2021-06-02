using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelController : MonoBehaviour
{
    private bool onceForCoroutine;
    private int layerMask;

    public Coroutine angelAttackTimer;
    public Coroutine angelDeadTimer;
    public Animator animator;
    public float MAXHP = 300f;
    public float angelHp = 300f;
    public float angelPower = 30f;
    public Vector3 basePosition;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        animator.SetBool("Run", true);

        angelAttackTimer = null;
        layerMask = 1 << LayerMask.NameToLayer("Enemy");    // enemy ���̾ �浹 üũ

        basePosition = new Vector3(-2f, 0f, 16.34f);

        transform.position = basePosition;
    }

    private void OnEnable()
    {
        onceForCoroutine = false;
        angelHp = MAXHP;
    }

    private void OnDisable()
    {
        if (angelAttackTimer != null)
        {
            StopCoroutine(angelAttackTimer);
        }
        if (angelDeadTimer != null)
        {
            StopCoroutine(angelDeadTimer);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.meetEnemy && !onceForCoroutine)
        {
            animator.SetBool("Run", false);
            angelAttackTimer = StartCoroutine(PlayerAttackTimer());
            onceForCoroutine = true;
        }
    }

    IEnumerator PlayerAttackTimer()
    {
        while (true)
        {
            LookAtEnemy();
            yield return new WaitForSeconds(0.5f);
            TargetToEnemy();
            yield return new WaitForSeconds(1.5f);
        }
    }

    private void LookAtEnemy()
    {
        GameManager.Instance.enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
        if (GameManager.Instance.enemyArray.Length == 0)
            return;

        int saveNumber = 0;
        float minDistance = DistanceToEnemy(GameManager.Instance.enemyArray[0]);
        for (int i = 1; i < GameManager.Instance.enemyArray.Length; ++i)
        {
            if (minDistance >= DistanceToEnemy(GameManager.Instance.enemyArray[i]))
            {
                minDistance = DistanceToEnemy(GameManager.Instance.enemyArray[i]);
                saveNumber = i;
            }
        }
        transform.LookAt(GameManager.Instance.enemyArray[saveNumber].transform);

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Jump Right Attack 01"))
        {
            if (angelHp > 0f)
                animator.Play("Melee Right Attack 03");
        }
    }

    private void TargetToEnemy()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + new Vector3(0f, 0.75f, 0f), transform.forward, out hit, 10f, layerMask))
        {
            Debug.DrawRay(transform.position + new Vector3(0f, 0.75f, 0f), transform.forward * 10f, Color.red, 10f);

            if (hit.collider.gameObject.name.Substring(0, 4) == "Enem")
                hit.collider.gameObject.GetComponent<EnemyController>().TakeDamage(angelPower);
            else
                hit.collider.gameObject.GetComponent<BossController>().TakeDamage(angelPower);
        }
    }

    private float DistanceToEnemy(GameObject enemy)
    {
        return Vector3.Distance(transform.position, enemy.transform.position);
    }

    public void TakeDamage(float damage)
    {
        angelHp -= damage;

        GameManager.Instance.aHpBarImage.fillAmount = angelHp / MAXHP;

        CheckToDead();
    }

    private void CheckToDead()
    {
        if (angelHp <= 0f)
        {
            animator.Play("Die");
            angelDeadTimer = StartCoroutine(CheckToDeadTimer());
        }
    }

    IEnumerator CheckToDeadTimer()
    {
        yield return new WaitForSeconds(1.0f);

        GameManager.Instance.aBtn.gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public void Initialize()        // �÷��̾� �� ���� �� ������ ������ �޼ҵ�
    {
        GameManager.Instance.meetEnemy = false;
        onceForCoroutine = false;
        StopCoroutine(angelAttackTimer);
    }
}
