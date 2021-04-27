using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FairyButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Coroutine fairySkillCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnDisable()
    {
        StopCoroutine(fairySkillCoroutine);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!GameManager.Instance.fc.GetCharacterState())
        {
            GameManager.Instance.fc.animator.SetBool("Fly Forward", false);
            GameManager.Instance.fc.animator.Play("Fly Cast Spell 02");
            fairySkillCoroutine = StartCoroutine(FairySkill3());
            GameManager.Instance.fc.animator.SetBool("Fly Forward", true);
        }
        else
        {
            GameManager.Instance.fc.animator.Play("Fly Cast Spell 02");
        }
    }

    IEnumerator FairySkill3()
    {
        yield return new WaitForSeconds(0.5f);
        ObjectPool.Instance.PopFromPool("FairySkill").transform.position = GameManager.Instance.ac.transform.position;
        yield return new WaitForSeconds(0.1f);
        ObjectPool.Instance.PopFromPool("FairySkill").transform.position = GameManager.Instance.dc.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
