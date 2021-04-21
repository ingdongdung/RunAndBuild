using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AngelButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!GameManager.Instance.ac.GetCharacterState())
        {
            GameManager.Instance.ac.animator.SetBool("Run", false);
            GameManager.Instance.ac.animator.Play("Jump Right Attack 01");
            GameManager.Instance.ac.animator.SetBool("Run", true);
        }
        else
        {
            GameManager.Instance.ac.animator.Play("Jump Right Attack 01");
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
