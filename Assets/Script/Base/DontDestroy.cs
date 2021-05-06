using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{   
    private void Awake()
    {
        // 여기서 gameObject는 DontDestroyObject
        DontDestroyOnLoad(this.gameObject);
    }
}
