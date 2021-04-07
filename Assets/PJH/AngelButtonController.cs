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
        ac.animator.SetBool("Run", false);
        ac.animator.Play("Jump Right Attack 01");
        ac.animator.SetBool("Run", true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int randNum = Random.RandomRange(0, 3);

            string treeName = "Tree";
            
            switch(randNum)
            {
                case 0:
                    treeName += "01";
                    break;
                case 1:
                    treeName += "02";
                    break;
                case 2:
                    treeName += "03";
                    break;
            }

            GameObject tree = ObjectPool.Instance.PopFromPool(treeName);
            tree.transform.position = new Vector3(-3f, 1f, 30f);
            tree.SetActive(true);
        }
    }
}
