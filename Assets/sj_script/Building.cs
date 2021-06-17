using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : KingdomBuilding
{
    public GameObject target;
    public bool isClick;
    public bool isModifying;
    private KingdomEffect kingdomEffect;
    float coroutineTime;
    float coroutineTime2;
    float fTimer;
    Material[] material;
    int length;
    FollowPlayerCamera camera;

    Color[] colors;

    // Start is called before the first frame update
    void Start()
    {
        camera = FindObjectOfType<FollowPlayerCamera>();
        isClick = false;
        isModifying = false;
        kingdomEffect = FindObjectOfType<KingdomEffect>();
        coroutineTime = 1.4f;
        coroutineTime2 = 0.2f;
        fTimer = 1f;
        material = target.GetComponent<MeshRenderer>().materials;
        length = material.Length;

        colors = new Color[length];
        for (int i = 0;i<length;i++)
        {
            colors[i] = material[i].color;
            material[i].color = new Color(material[i].color.r*-0.5f, material[i].color.g*1.2f, material[i].color.b*1.8f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void LateUpdate()
    {
        if (!isClick)
        {
            Vector3 pos = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(pos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                pos = hit.transform.position;
                target.transform.position = pos;

                if (Input.GetMouseButtonDown(0) && !isModifying)
                {
                    target.GetComponent<BoxCollider>().enabled = true;
                    target.GetComponent<Renderer>().enabled = false;
                    isClick = true;

                    StartCoroutine("TimerBuilding");
                    StartCoroutine("TimerRenderer");
                }

                if(isModifying)
                {
                    for (int i = 0; i < length; i++)
                    {
                        colors[i] = material[i].color;
                        material[i].color = new Color(material[i].color.r * -0.5f, material[i].color.g * 1.2f, material[i].color.b * 1.8f);
                    }
                    isModifying = false;
                }

                if (Input.mouseScrollDelta.y > 0)
                {
                    target.transform.Rotate(0f, 90f, 0f);
                }
                else if (Input.mouseScrollDelta.y < 0)
                {
                    target.transform.Rotate(0f, -90f, 0f);
                }



            }
        }
    }

    IEnumerator TimerBuilding()
    {
        yield return new WaitForSeconds(coroutineTime);
       
        camera.isBuilding = false;
    }

    IEnumerator TimerRenderer()
    {
        yield return new WaitForSeconds(coroutineTime2);
        target.GetComponent<Renderer>().enabled = true;
        for (int i = 0; i < length; i++)
        {
            material[i].color = colors[i];
        }

        kingdomEffect.effect.transform.position = target.transform.position;
        kingdomEffect.particleSystem.Play();
        kingdomEffect.particleSystem.time = 0f;
    }
}
