using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FairyButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private FairyController fc;

    // Start is called before the first frame update
    void Start()
    {
        fc = GameObject.Find("Fairy").GetComponent<FairyController>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!fc.GetCharacterState())
        {
            fc.animator.SetBool("Fly Forward", false);
            fc.animator.Play("Fly Cast Spell 01");
            fc.animator.SetBool("Fly Forward", true);
        }
        else
        {
            fc.animator.Play("Fly Cast Spell 01");
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
