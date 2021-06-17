using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingdomPlayer : KingdomPlayers
{
    Animator animator;
    static int tagSize = 9;
    string[] animTagArr = new string[tagSize];
    string currentAnim;
    string beforeAnim;
    int randomTag;
    public bool isStartCoroutine;
    int randomRotate;
    float coroutineTime;
    bool isCollision;
    public enum BUILDING { beforeBuild, building, buildingEnd };
    public BUILDING eBuilding;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        randomTag = 0;
        isStartCoroutine = true;
        randomRotate = 0;
        coroutineTime = 1f;
        isCollision = false;
        eBuilding = BUILDING.beforeBuild;

        animTagArr[0] = "Walk";
        animTagArr[1] = "Idle";
        animTagArr[2] = "Run";
        animTagArr[3] = "rotate";
        animTagArr[4] = "Relax";
        animTagArr[5] = "Jump";
        animTagArr[6] = "Shake Head";
        animTagArr[7] = "Clapping";
        animTagArr[8] = "Crying";

        tiles = GameObject.FindGameObjectsWithTag("Tile"); // 0 ~ 659

    }

    // Update is called once per frame
    void Update()
    {
        if (isStartCoroutine)
        {
            isStartCoroutine = false;
            if (isCollision == false)
                randomTag = Random.Range(0, tagSize);
            else
            {
                randomTag = 3;
                isCollision = false;
            }
            randomRotate = Random.Range(0, 2);

            StartCoroutine("PlayerAnimationBehavior", animTagArr[randomTag]);
        }

        if (transform.position.y < -50f)
        {
            transform.position = new Vector3(6f, 1f, 1f);
            transform.rotation = new Quaternion(0f, 0f, 0f, 1f);
        }

        MovePlayer(animTagArr[randomTag]);

        //if (eBuilding == BUILDING.buildingEnd)
        //{
        //    Physics.
        //}

        //if (transform.localRotation.eulerAngles.x >= 90f || transform.localRotation.eulerAngles.z >= 90f ||
        //    transform.localRotation.eulerAngles.x <= -90f || transform.localRotation.eulerAngles.z <= -90f)
        //{
        //    Debug.Log("�������");
        //    Vector3 rot = new Vector3(0f, 0f, 0f);
        //    transform.localRotation = Quaternion.Euler(rot);
        //}

    }

    void MovePlayer(string animationTag)
    {
        if (animationTag == "Walk")
        {
            transform.Translate(Vector3.forward * Time.deltaTime, Space.Self);
        }
        else if (animationTag == "Idle")
        {

        }
        else if (animationTag == "Run")
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 1.5f, Space.Self);
        }
        else if (animationTag == "rotate")
        {
            if (randomRotate == 0)
                transform.Rotate(Vector3.up * Time.deltaTime * 100f);
            else if (randomRotate == 1)
                transform.Rotate(Vector3.up * -Time.deltaTime * 100f);
        }
        else if (animationTag == "Relax")
        {

        }
        else if (animationTag == "Jump")
        {
            transform.Translate(Vector3.forward * Time.deltaTime, Space.Self);
        }
        else if (animationTag == "Shake Head")
        {

        }
        else if (animationTag == "Clapping")
        {

        }
        else if (animationTag == "Crying")
        {

        }

    }

    IEnumerator PlayerAnimationBehavior(string animationTag)
    {
        beforeAnim = currentAnim;
        //Debug.Log(animationTag);
        if (animationTag == "Walk")
        {
            currentAnim = "Walk";
            animator.SetBool(beforeAnim, false);
            animator.SetBool("Walk", true);
            coroutineTime = 3f;
        }
        else if (animationTag == "Idle")
        {
            currentAnim = "Idle";
            animator.SetBool(beforeAnim, false);
            animator.SetBool("Idle", true);
            coroutineTime = 5f;
        }
        else if (animationTag == "Run")
        {
            currentAnim = "Run";
            animator.SetBool(beforeAnim, false);
            animator.SetBool("Run", true);
            coroutineTime = 2f;
        }
        else if (animationTag == "rotate")
        {
            currentAnim = "Walk";
            animator.SetBool(beforeAnim, false);
            animator.SetBool("Walk", true);
            coroutineTime = 1f;
        }
        else if (animationTag == "Relax")
        {
            currentAnim = "Relax";
            animator.SetBool(beforeAnim, false);
            animator.SetBool("Relax", true);
            coroutineTime = 3f;
        }
        else if (animationTag == "Jump")
        {
            currentAnim = "Jump";
            animator.SetBool(beforeAnim, false);
            animator.SetBool("Jump", true);
            coroutineTime = 0.8f;
        }
        else if (animationTag == "Shake Head")
        {
            currentAnim = "Shake Head";
            animator.SetBool(beforeAnim, false);
            animator.SetBool("Shake Head", true);
            coroutineTime = 2f;
        }
        else if (animationTag == "Clapping")
        {
            currentAnim = "Clapping";
            animator.SetBool(beforeAnim, false);
            animator.SetBool("Clapping", true);
            coroutineTime = 3f;
        }
        else if (animationTag == "Crying")
        {
            currentAnim = "Crying";
            animator.SetBool(beforeAnim, false);
            animator.SetBool("Crying", true);
            coroutineTime = 5f;
        }


        yield return new WaitForSeconds(coroutineTime);

        isStartCoroutine = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Debug.Log(collision.collider.tag);
        if (collision.collider.tag != "Tile")
        {
            //Debug.Log(other.tag);
            isStartCoroutine = true;
            randomTag = 3;
            isCollision = true;
        }

        if (collision.collider.tag == "kingdomBuilding" && eBuilding == BUILDING.buildingEnd)
        {

            int randomIndex = Random.Range(0, tiles.Length);
            Debug.Log("�ǹ��̶� �浹��");
            Vector3 pos = new Vector3(tiles[randomIndex].transform.position.x,
               0.9f, tiles[randomIndex].transform.position.z);

            transform.position = pos;

            eBuilding = BUILDING.beforeBuild;

        }

        if (collision.collider.tag == "Tile" && transform.position.y < -2.2f)
        {
            //Debug.Log("Ÿ���̶� �浹��");
            Vector3 pos = new Vector3(transform.position.x, 0.9f, transform.position.z);
            transform.position = pos;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        //Debug.Log(other.tag);

        //Debug.Log(transform.position.y);
        if (collision.collider.tag == "Tile" && transform.position.y < 0.9f)
        {
            Debug.Log("Ÿ���̶� �浹��");
            Vector3 pos = new Vector3(transform.position.x, 1f, transform.position.z);
            transform.position = pos;
        }


    }

    private void OnCollisionExit(Collision collision)
    {
        if (eBuilding == BUILDING.buildingEnd)
        {
            if (collision.collider.tag == "kingdomBuilding")
            {
                eBuilding = BUILDING.beforeBuild;
            }
        }
    }

}
