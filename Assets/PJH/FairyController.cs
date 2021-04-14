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
        // 첫번째 공격 이후 타겟이 바뀌는데 확인해보니 제일 거리 가까운 애한테 공격하는거였음
        // 왜 첫번째 타겟 정할 때랑 두번째 이후부터랑 타겟 거리가 달라지는지 모르겠음
        //print(saveNumber);
        transform.LookAt(enemyArray[saveNumber].transform);

        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit, 10);

        // **************************** 21.04.14. 16:14 여기서부터
        // 게임 오브젝트 찾아지는지 확인해야 됨
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
    
    public void Initialize()        // 플레이어 앞 적이 다 죽으면 실행할 메소드
    {
        meetEnemy = false;
        onceForCoroutine = false;
        StopCoroutine(fairyAttackTimer);
    }
}
