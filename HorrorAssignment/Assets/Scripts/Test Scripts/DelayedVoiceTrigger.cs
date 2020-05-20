using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedVoiceTrigger : MonoBehaviour
{
    public AudioSource voiceline;
    public bool hasPlayed;
    public float delayTime;

    // Start is called before the first frame update
    void Start()
    {
        hasPlayed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && hasPlayed == false)
        {
            voiceline.PlayDelayed(delayTime);
        }
    }
}
