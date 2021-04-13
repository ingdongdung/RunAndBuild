using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyController : MonoBehaviour
{
    public Animator animator;

    bool meetEnemy;
    bool onceForCoroutine;

    Coroutine fairyAttackTimer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        animator.SetBool("Fly Idle", true);
        animator.SetBool("Fly Forward", true);
    }

    private void OnEnable()
    {
        meetEnemy = false;
        onceForCoroutine = false;
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
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Fly Cast Spell 02"))
            {
                animator.Play("Fly Cast Spell 01");
            }
            yield return new WaitForSeconds(2);
        }
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
