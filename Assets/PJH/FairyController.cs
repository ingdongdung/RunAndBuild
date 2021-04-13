using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyController : MonoBehaviour
{
    public Animator animator;

    bool meetEnemy;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        animator.SetBool("Fly Idle", true);
        animator.SetBool("Fly Forward", true);

        meetEnemy = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MeetEnemy()
    {
        meetEnemy = true;
    }

    public bool GetCharacterState()
    {
        return meetEnemy;
    }
}
