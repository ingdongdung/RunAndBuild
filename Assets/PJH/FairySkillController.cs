using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairySkillController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnDisable()
    {
        ObjectPool.Instance.PushToPool(gameObject.name, gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
