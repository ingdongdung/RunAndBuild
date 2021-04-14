using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyController : MonoBehaviour
{
    public const float MAXHP = 300f;

    public Animator animator;

    bool meetEnemy;
    bool onceForCoroutine;

    public float fairyHp = 300f;
    public float fairyPower = 40f;

    Coroutine fairyAttackTimer;
    GameObject[] enemyArray;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        animator.SetBool("Fly Idle", true);
        animator.SetBool("Fly Forward", true);

        fairyAttackTimer = null;
    }

    private void OnEnable()
    {
        meetEnemy = false;
        onceForCoroutine = false;
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
        if (meetEnemy && !onceForCoroutine)
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
        enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
        int saveNumber = 0;
        float minDistance = DistanceToEnemy(enemyArray[0]);
        for (int i = 1; i < enemyArray.Length; ++i)
        {
            //print("enemy" + i + " : " + DistanceToEnemy(enemyArray[i]));
            if (minDistance >= DistanceToEnemy(enemyArray[i]))
            {
                minDistance = DistanceToEnemy(enemyArray[i]);
                saveNumber = i;
            }
        }
        // ù��° ���� ���� Ÿ���� �ٲ�µ� Ȯ���غ��� ���� �Ÿ� ����� ������ �����ϴ°ſ���
        // �� ù��° Ÿ�� ���� ���� �ι�° ���ĺ��Ͷ� Ÿ�� �Ÿ��� �޶������� �𸣰���
        //print(saveNumber);
        transform.LookAt(enemyArray[saveNumber].transform);

        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit, 10);

        // **************************** 21.04.14. 16:14 ���⼭����
        // ���� ������Ʈ ã�������� Ȯ���ؾ� ��
        //hit.collider.gameObject.GetComponent<EnemyController>().TakeDamage(fairyPower);
    }

    private float DistanceToEnemy(GameObject enemy)
    {
        return Vector3.Distance(transform.position, enemy.transform.position);
    }

    public void TakeDamage(float damage)
    {
        fairyHp -= damage;
    }

    public void MeetEnemy()
    {
        meetEnemy = true;
    }

    public bool GetCharacterState()
    {
        return meetEnemy;
    }
    
    public void Initialize()        // �÷��̾� �� ���� �� ������ ������ �޼ҵ�
    {
        meetEnemy = false;
        onceForCoroutine = false;
        StopCoroutine(fairyAttackTimer);
    }
}
