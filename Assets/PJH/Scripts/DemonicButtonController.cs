using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DemonicButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private GameObject[] enemyArray;
    private Coroutine demonicSkillCoroutine;
    private float demonicSkillDamage;

    private void Awake()
    {
        demonicSkillDamage = 150f;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnDisable()
    {
        StopCoroutine(demonicSkillCoroutine);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!GameManager.Instance.dc.GetCharacterState())
        {
            GameManager.Instance.dc.animator.SetBool("Run", false);
            GameManager.Instance.dc.animator.Play("Melee Left Attack 01");
            demonicSkillCoroutine = StartCoroutine(DemonicSkill());
            GameManager.Instance.dc.animator.SetBool("Run", true);
        }
        else
        {
            GameManager.Instance.dc.animator.Play("Melee Left Attack 01");
            demonicSkillCoroutine = StartCoroutine(DemonicSkill());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DemonicSkill()
    {
        yield return new WaitForSeconds(0.5f);

        enemyArray = GameObject.FindGameObjectsWithTag("Enemy");

        int saveNumber = 0;
        float maxHp = enemyArray[0].GetComponent<EnemyController>().enemyHp;
        for (int i = 1; i < enemyArray.Length; ++i)
        {
            if (maxHp < enemyArray[i].GetComponent<EnemyController>().enemyHp)
            {
                maxHp = enemyArray[i].GetComponent<EnemyController>().enemyHp;
                saveNumber = i;
            }
        }

        // DemonicSkillForFog
        // DemonicSkillForBats
        // DemonicSkillForHit

        // 여기부터 05.04 10:14
        ObjectPool.Instance.PopFromPool("AngelSkillForGround").transform.position = enemyArray[saveNumber].transform.position;
        ObjectPool.Instance.PopFromPool("AngelSkillForAir").transform.position = enemyArray[saveNumber].transform.position + new Vector3(0f, 1f, 0f);
        SkillEffect(saveNumber);
    }

    private void SkillEffect(int idx)
    {
        enemyArray[idx].GetComponent<EnemyController>().TakeDamage(demonicSkillDamage);
    }
}
