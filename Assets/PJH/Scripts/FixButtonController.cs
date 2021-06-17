using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FixButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        KingdomBuilding.Instance.camera = FindObjectOfType<FollowPlayerCamera>();

        if (KingdomBuilding.Instance.camera.isBuilding)
        {
            KingdomBuilding.Instance.camera.isBuilding = false;
            Debug.Log("if ");
        }
        else
        {
            KingdomBuilding.Instance.camera.isBuilding = true;
            KingdomBuilding.Instance.bSelected = false;
            Debug.Log("else ");
        }
    }
}
