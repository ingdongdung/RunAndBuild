using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonicController : MonoBehaviour
{
    public Animator animator;

    bool meetEnemy;
    bool onceForCoroutine;

    public Coroutine demonicAttackTimer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        animator.SetBool("Run", true);
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
        StopCoroutine(demonicAttackTimer);
    }
}
