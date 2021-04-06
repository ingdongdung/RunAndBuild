using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelController : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        // TH Sword Run Without Root Motion
        //animator.SetBool("TH Run Without Root Motion", true);
        animator.Play("TH Run Without Root Motion");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
