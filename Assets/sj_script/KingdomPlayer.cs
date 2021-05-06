using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingdomPlayer : MonoBehaviour
{
    Animator animator;
    static int tagSize = 9;
    string[] animTagArr = new string[tagSize];
    string currentAnim;
    string beforeAnim;
    int randomTag;
    bool isStartCoroutine;
    int randomRotate;
    float coroutineTime;
    bool isCollision;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        randomTag = 0;
        isStartCoroutine = true;
        randomRotate = 0;
        coroutineTime = 1f;
        isCollision = false;

        animTagArr[0] = "Walk";
        animTagArr[1] = "Idle";
        animTagArr[2] = "Run";
        animTagArr[3] = "rotate";
        animTagArr[4] = "Relax";
        animTagArr[5] = "Jump";
        animTagArr[6] = "Shake Head";
        animTagArr[7] = "Clapping";
        animTagArr[8] = "Crying";

    }

    // Update is called once per frame
    void Update()
    {       
        if (isStartCoroutine)
        {
            isStartCoroutine = false;
            randomTag = Random.Range(0, tagSize);
            randomRotate = Random.Range(0, 2);
            
            StartCoroutine("PlayerAnimationBehavior", animTagArr[randomTag]);
        }

        if(transform.position.y < -50f)
        {
            transform.position = new Vector3(6f, 1f, 1f);
            transform.rotation = new Quaternion(0f, 0f, 0f, 1f);
        }

        MovePlayer(animTagArr[randomTag]);


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
        else if(animationTag == "rotate")
        {
            if (randomRotate == 0)
                transform.Rotate(Vector3.up * Time.deltaTime * 100f);
            else if(randomRotate == 1)
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
        Debug.Log(animationTag);
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
        else if(animationTag == "rotate")
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
        isCollision = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Tile")
        {
            Debug.Log(other.tag);
            isStartCoroutine = true;
            randomTag = 3;
            isCollision = true;
        }

    }
}
