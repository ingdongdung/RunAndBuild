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
        // update�� ��ġ�� hp�� �����ߵǴϱ� late update.

        // 3d ��ǥ�� ��ũ����ǥ�� �����ϴ� ����.
        var screenPos = Camera.main.WorldToScreenPoint(targetTransform.position + offset);

        // ���� ī�޶� ���� ui���� �� ȭ�鿡 ������ ���� �����ϱ� ����.
        if (screenPos.z < 0.0f)
        {
            screenPos *= -1.0f;
        }

        var localPos = Vector2.zero;
        // ��ũ����ǥ���� ĵ������ǥ�� �����ϴ� ����.
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenPos, uiCamera, out localPos);

        rectHp.localPosition = localPos;
    }
}
