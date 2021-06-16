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
        
    }
}
