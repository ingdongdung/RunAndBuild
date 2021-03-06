using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FairyButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private float fairyHealAmount;

    public Coroutine fairySkillCoroutine;

    private void Awake()
    {
        fairyHealAmount = 70f;
    }

    // Start is called before the first frame update
    void Start()
    {
        fairySkillCoroutine = null;
    }

    private void OnDisable()
    {
        if (fairySkillCoroutine != null)
        {
            StopCoroutine(fairySkillCoroutine);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!GameManager.Instance.meetEnemy)
        {
            GameManager.Instance.fc.animator.SetBool("Fly Forward", false);
            GameManager.Instance.fc.animator.Play("Fly Cast Spell 02");
            fairySkillCoroutine = StartCoroutine(FairySkill());
            GameManager.Instance.fc.animator.SetBool("Fly Forward", true);
        }
        else
        {
            GameManager.Instance.fc.animator.Play("Fly Cast Spell 02");
            fairySkillCoroutine = StartCoroutine(FairySkill());
        }
    }

    IEnumerator FairySkill()
    {
        yield return new WaitForSeconds(0.5f);
        ObjectPool.Instance.PopFromPool("FairySkill").transform.position = GameManager.Instance.fc.transform.position;
        ObjectPool.Instance.PopFromPool("FairySkill").transform.position = GameManager.Instance.ac.transform.position;
        ObjectPool.Instance.PopFromPool("FairySkill").transform.position = GameManager.Instance.dc.transform.position;
        SkillEffect();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SkillEffect()
    {
        if (GameManager.Instance.ac.angelHp > 0f)
        {
            GameManager.Instance.ac.TakeDamage(-fairyHealAmount);
        }
        if (GameManager.Instance.dc.demonicHp > 0f)
        {
            GameManager.Instance.dc.TakeDamage(-fairyHealAmount);
        }
        if (GameManager.Instance.fc.fairyHp > 0f)
        {
            GameManager.Instance.fc.TakeDamage(-fairyHealAmount);
        }
    }
}
