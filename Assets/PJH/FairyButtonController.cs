using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FairyButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!GameManager.Instance.fc.GetCharacterState())
        {
            GameManager.Instance.fc.animator.SetBool("Fly Forward", false);
            GameManager.Instance.fc.animator.Play("Fly Cast Spell 02");
            GameManager.Instance.fc.animator.SetBool("Fly Forward", true);
        }
        else
        {
            GameManager.Instance.fc.animator.Play("Fly Cast Spell 02");
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
