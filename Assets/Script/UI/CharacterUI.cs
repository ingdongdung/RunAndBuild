using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUI : MonoBehaviour
{
    // Start is called before the first frame update
    public void ActionClear()
    {
        this.animator.SetBool("Victory", true);
    }

    public void ActionFail()
    {
        this.animator.SetBool("Crying", true);
    }

    [SerializeField] Animator animator;
}
