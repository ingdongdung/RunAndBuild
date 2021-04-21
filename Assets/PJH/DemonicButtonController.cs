using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DemonicButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private DemonicController dc;

    // Start is called before the first frame update
    void Start()
    {
        dc = GameObject.Find("Demonic").GetComponent<DemonicController>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!GameManager.Instance.dc.GetCharacterState())
        {
            GameManager.Instance.dc.animator.SetBool("Run", false);
            GameManager.Instance.dc.animator.Play("Melee Left Attack 01");
            GameManager.Instance.dc.animator.SetBool("Run", true);
        }
        else
        {
            GameManager.Instance.dc.animator.Play("Melee Left Attack 01");
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
