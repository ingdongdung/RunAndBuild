using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossController : MonoBehaviour
{
    private Vector3 skillDir;
    private Coroutine skillCoroutine;
    private bool onceForCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        skillDir = new Vector3(0f, 0f, 0f);
        onceForCoroutine = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.meetEnemy && onceForCoroutine)
        {
            onceForCoroutine = false;
            skillCoroutine = StartCoroutine(SkillTimer());
        }
    }

    private void OnDisable()
    {
        if (skillCoroutine != null)
            StopCoroutine(skillCoroutine);

        onceForCoroutine = true;
    }

    IEnumerator SkillTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(6f);
            
            if (GameManager.Instance.ac)
            {
                GameManager.Instance.firstBossSkillDir = GameManager.Instance.ac.transform.position - transform.position;
                GameManager.Instance.firstBossSkillDir.Normalize();
                ObjectPool.Instance.PopFromPool("FirstBossSkill");
            }
            if (GameManager.Instance.dc)
            {
                GameManager.Instance.firstBossSkillDir = GameManager.Instance.dc.transform.position - transform.position;
                GameManager.Instance.firstBossSkillDir.Normalize();
                ObjectPool.Instance.PopFromPool("FirstBossSkill");
            }
            if (GameManager.Instance.fc)
            {
                GameManager.Instance.firstBossSkillDir = GameManager.Instance.fc.transform.position - transform.position;
                GameManager.Instance.firstBossSkillDir.Normalize();
                ObjectPool.Instance.PopFromPool("FirstBossSkill");
            }

        }
    }
}
