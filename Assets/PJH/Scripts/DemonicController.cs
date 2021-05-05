using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonicController : MonoBehaviour
{
    private bool onceForCoroutine;
    private int layerMask;
    private Coroutine demonicAttackTimer;
    private GameObject[] enemyArray;

    public Animator animator;
    public float MAXHP = 300f;
    public float demonicHp = 300f;
    public float demonicPower = 30f;
    public Vector3 basePosition;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        animator.SetBool("Run", true);

        demonicAttackTimer = null;
        layerMask = 1 << LayerMask.NameToLayer("Enemy");    // enemy 레이어만 충돌 체크

        basePosition = new Vector3(2f, 0f, 16.34f);
    }

    private void OnEnable()
    {
        onceForCoroutine = false;
        demonicHp = MAXHP;
    }

    private void OnDisable()
    {
        if (demonicAttackTimer != null)
        {
            StopCoroutine(demonicAttackTimer);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.meetEnemy && !onceForCoroutine)
        {
            animator.SetBool("Run", false);
            demonicAttackTimer = StartCoroutine(PlayerAttackTimer());
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

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Melee Left Attack 01"))
        {
            animator.Play("Right Punch Attack");
        }
    }

    private void TargetToEnemy()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + new Vector3(0f, 0.75f, 0f), transform.forward, out hit, 10f, layerMask))
        {
            Debug.DrawRay(transform.position + new Vector3(0f, 0.75f, 0f), transform.forward * 10f, Color.red, 10f);
            hit.collider.gameObject.GetComponent<EnemyController>().TakeDamage(demonicPower);
        }
    }

    private float DistanceToEnemy(GameObject enemy)
    {
        return Vector3.Distance(transform.position, enemy.transform.position);
    }

    public void TakeDamage(float damage)
    {
        demonicHp -= damage;
    }

    public void Initialize()        // 플레이어 앞 적이 다 죽으면 실행할 메소드
    {
        GameManager.Instance.meetEnemy = false;
        onceForCoroutine = false;
        StopCoroutine(demonicAttackTimer);
    }
}
