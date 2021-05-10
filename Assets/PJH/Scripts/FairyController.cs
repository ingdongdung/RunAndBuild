using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyController : MonoBehaviour
{
    private bool onceForCoroutine;
    private int layerMask;

    public Coroutine fairyAttackTimer;
    public Animator animator;
    public float MAXHP = 300f;
    public float fairyHp = 300f;
    public float fairyPower = 40f;
    public Vector3 fairyDir;
    public Vector3 basePosition;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        animator.SetBool("Fly Idle", true);
        animator.SetBool("Fly Forward", true);

        fairyAttackTimer = null;
        layerMask = 1 << LayerMask.NameToLayer("Enemy");    // enemy ���̾ �浹 üũ

        fairyDir = new Vector3(0f, 0f, 0f);
        basePosition = new Vector3(0f, 0f, 13.79f);

        transform.position = basePosition;
    }

    private void OnEnable()
    {
        onceForCoroutine = false;
        fairyHp = MAXHP;
    }

    private void OnDisable()
    {
        if (fairyAttackTimer != null)
        {
            StopCoroutine(fairyAttackTimer);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.meetEnemy && !onceForCoroutine)
        {
            animator.SetBool("Fly Forward", false);
            fairyAttackTimer = StartCoroutine(PlayerAttackTimer());
            onceForCoroutine = true;
        }
    }

    IEnumerator PlayerAttackTimer()
    {
        while (true)
        {
            TargetToEnemy();

            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Fly Cast Spell 02"))
            {
                animator.Play("Fly Cast Spell 01");
            }
            yield return new WaitForSeconds(2);
        }
    }

    private void TargetToEnemy()
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
        // ù��° ���� ���� Ÿ���� �ٲ�µ� Ȯ���غ��� ���� �Ÿ� ����� ������ �����ϴ°ſ���
        // �� ù��° Ÿ�� ���� ���� �ι�° ���ĺ��Ͷ� Ÿ�� �Ÿ��� �޶������� �𸣰���
        // --> ù��°�� �˻��� ���� �������� �˻��� �� ������ �� �̵� ���� ������ ���� �Ÿ� ���̰� �ִ� �� ��
        transform.LookAt(GameManager.Instance.enemyArray[saveNumber].transform);

        RaycastHit hit;
        if (Physics.Raycast(transform.position + new Vector3(0f, 0.75f, 0f), transform.forward, out hit, 10f, layerMask))
        {
            Debug.DrawRay(transform.position + new Vector3(0f, 0.75f, 0f), transform.forward * 10f, Color.red, 10f);
            fairyDir = transform.forward;       // bullet�� ������ ���� ����
            Invoke("ShootTheBullet", 0.5f);
        }
    }

    private void ShootTheBullet()
    {
        ObjectPool.Instance.PopFromPool("FairyBullet");
    }

    private float DistanceToEnemy(GameObject enemy)
    {
        return Vector3.Distance(transform.position, enemy.transform.position);
    }

    public void TakeDamage(float damage)
    {
        fairyHp -= damage;

        GameManager.Instance.fHpBarImage.fillAmount = fairyHp / MAXHP;
    }

    public void Initialize()        // �÷��̾� �� ���� �� ������ ������ �޼ҵ�
    {
        GameManager.Instance.meetEnemy = false;
        onceForCoroutine = false;
        StopCoroutine(fairyAttackTimer);
    }
}
