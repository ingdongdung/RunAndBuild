using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : MonoBehaviour
{
    public void OnClickBackground()
    {
        Debug.Log("Move KingdomScene");
        this.inputNamePop.gameObject.SetActive(true);
    }

    [SerializeField]
    InputNamePop inputNamePop;
}
