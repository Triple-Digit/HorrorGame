using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBehavior : MonoBehaviour
{
    public ParticleBehavior instance;

    public float destroyTime;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
