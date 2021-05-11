using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonCooltimeController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Image img;
    public Button btn;
    public float aCooltime;
    public float dCooltime;
    public float fCooltime;

    private float leftTime;
    private bool isClicked;
    private Coroutine CooltimeCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        if (img == null)
            img = gameObject.GetComponent<Image>();
        if (btn == null)
            btn = gameObject.GetComponent<Button>();

        aCooltime = 11f;
        dCooltime = 13f;
        fCooltime = 17f;
        leftTime = 0f;
        isClicked = true;
    }

    private void OnDisable()
    {
        StopCoroutine(CooltimeCoroutine);
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isClicked)
        {
            if (btn)
                btn.enabled = false;

            if (btn.name.Substring(0, 5) == "Angel")
            {
                print(btn.name.Substring(0, 5));
                leftTime = aCooltime;
                CooltimeCoroutine = StartCoroutine(StartCooltime(aCooltime));
            }
            else if (btn.name.Substring(0, 5) == "Demon")
            {
                print(btn.name.Substring(0, 5));
                leftTime = dCooltime;
                CooltimeCoroutine = StartCoroutine(StartCooltime(dCooltime));
            }
            else if (btn.name.Substring(0, 5) == "Fairy")
            {
                print(btn.name.Substring(0, 5));
                leftTime = fCooltime;
                CooltimeCoroutine = StartCoroutine(StartCooltime(fCooltime));
            }
        }
    }

    IEnumerator StartCooltime(float cooltime)
    {
        isClicked = false;

        while (leftTime > 0f)
        {
            leftTime -= Time.deltaTime;
            if (leftTime < 0f)
            {
                leftTime = 0f;
                if (btn)
                    btn.enabled = true;

                isClicked = false;
            }

            float ratio = 1.0f - (leftTime / cooltime);
            if (img)
                img.fillAmount = ratio;

            yield return null;
        }

        isClicked = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
