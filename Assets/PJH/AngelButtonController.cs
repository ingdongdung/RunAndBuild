using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AngelButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private AngelController ac;

    // Start is called before the first frame update
    void Start()
    {
        ac = GameObject.Find("Angel").GetComponent<AngelController>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!ac.GetCharacterState())
        {
            ac.animator.SetBool("Run", false);
            ac.animator.Play("Jump Right Attack 01");
            ac.animator.SetBool("Run", true);
        }
        else
        {
            ac.animator.Play("Jump Right Attack 01");
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
