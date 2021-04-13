using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelController : MonoBehaviour
{
    public Animator animator;

    bool meetEnemy;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        animator.SetBool("Run", true);

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
