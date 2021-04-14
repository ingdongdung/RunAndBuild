using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonicController : MonoBehaviour
{
    public const float MAXHP = 300f;

    public Animator animator;

    bool meetEnemy;
    bool onceForCoroutine;

    public float demonicHp = 300f;
    public float demonicPower = 30f;

    public Coroutine demonicAttackTimer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        animator.SetBool("Run", true);

        demonicAttackTimer = null;
    }

    private void OnEnable()
    {
        meetEnemy = false;
        onceForCoroutine = false;
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
        if (meetEnemy && !onceForCoroutine)
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
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Melee Left Attack 01"))
            {
                animator.Play("Right Punch Attack");
            }
            yield return new WaitForSeconds(2);
        }
    }

    public void TakeDamage(float damage)
    {
        demonicHp -= damage;
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
        StopCoroutine(demonicAttackTimer);
    }
}
