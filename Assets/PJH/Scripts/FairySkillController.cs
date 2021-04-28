using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairySkillController : MonoBehaviour
{
    public Coroutine particleTimerCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        particleTimerCoroutine = StartCoroutine(particleTimer());
    }

    private void OnDisable()
    {
        StopCoroutine(particleTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator particleTimer()
    {
        ParticleSystem ps = this.GetComponent<ParticleSystem>();

        while (true && ps != null)
        {
            yield return new WaitForSeconds(0.5f);

            if (!ps.IsAlive(true))
            {
                ObjectPool.Instance.PushToPool(gameObject.name, gameObject);
            }
        }
    }
}
