using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingdomEffect : MonoBehaviour
{
    public GameObject effect;
    public ParticleSystem particleSystem;

    // Start is called before the first frame update
    void Start()
    {
        particleSystem = effect.GetComponent<ParticleSystem>();
        particleSystem.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        float time = particleSystem.time;
        // Debug.Log(time);

        if (time > 4.9f)
        {
            Vector3 pos = new Vector3(0f, +500f, 0f);
            effect.transform.position = pos;
        }
    }
}
