using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientTrigger : MonoBehaviour
{
    public AudioSource sound;
    public bool alreadyPlayed;


    // Start is called before the first frame update
    void Start()
    {
        alreadyPlayed = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (alreadyPlayed == false)
        {
            sound.Play();
            alreadyPlayed = true;
        }
    }
}
