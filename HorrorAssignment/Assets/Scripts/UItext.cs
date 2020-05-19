using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UItext : MonoBehaviour
{
    public GameObject UI_trigger;

    void Start()
    {
       UI_trigger.SetActive(false);
    }

    private void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "Player")
        {
            UI_trigger.SetActive(true);
            StartCoroutine("WaitForSec");
        }
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(5);
        Destroy(UI_trigger);
        Destroy(gameObject);
    }
 
}
