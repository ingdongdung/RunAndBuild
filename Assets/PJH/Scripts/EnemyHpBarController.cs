using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpBarController : MonoBehaviour
{
    private Camera uiCamera;
    private Canvas canvas;
    private RectTransform rectParent;
    private RectTransform rectHp;

    public Vector3 offset = Vector3.zero;
    public Transform targetTransform;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        uiCamera = canvas.worldCamera;
        rectParent = canvas.GetComponent<RectTransform>();
        rectHp = gameObject.GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        gameObject.transform.position = GameManager.Instance.uiCanvas.transform.position;
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

        // ��ũ����ǥ���� ĵ������ǥ�� �����ϴ� ����.
        var localPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenPos, uiCamera, out localPos);

        rectHp.localPosition = localPos;
    }
}
