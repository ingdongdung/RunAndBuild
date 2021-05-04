using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingdomPlayer : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime, Space.World);
        animator.SetFloat("Walk", 1f, 0.1f, Time.deltaTime);
        
    }
}
