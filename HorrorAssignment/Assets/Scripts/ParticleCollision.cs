using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Enemy heard the player");
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyBehavior>().playerSpotted();
            other.gameObject.GetComponent<EnemyBehavior>().ChasePlayer();
        }
    }
    
}
