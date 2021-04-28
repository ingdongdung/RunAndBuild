using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyBulletEffectLifeTimer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine(BulletLifeTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator BulletLifeTimer()
    {
        yield return new WaitForSeconds(1f);
        ObjectPool.Instance.PushToPool(gameObject.name, gameObject);
    }
}
