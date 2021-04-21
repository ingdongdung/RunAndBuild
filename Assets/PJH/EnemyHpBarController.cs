using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpBarController : MonoBehaviour
{
    Camera uiCamera;
    Canvas canvas;
    RectTransform rectParent;
    RectTransform rectHp;

    [HideInInspector]
    public Vector3 offset = Vector3.zero;
    [HideInInspector]
    public Transform targetTransform;


    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        uiCamera = canvas.worldCamera;
        rectParent = canvas.GetComponent<RectTransform>();
        rectHp = gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        // update된 위치에 hp가 찍혀야되니까 late update.

        // 3d 좌표를 스크린좌표로 변경하는 과정.
        var screenPos = Camera.main.WorldToScreenPoint(targetTransform.position + offset);

        // 메인 카메라 뒤의 ui까지 한 화면에 찍히는 것을 방지하기 위함.
        if (screenPos.z < 0.0f)
        {
            screenPos *= -1.0f;
        }

        var localPos = Vector2.zero;
        // 스크린좌표에서 캔버스좌표로 변경하는 과정.
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenPos, uiCamera, out localPos);

        rectHp.localPosition = localPos;
    }
}
