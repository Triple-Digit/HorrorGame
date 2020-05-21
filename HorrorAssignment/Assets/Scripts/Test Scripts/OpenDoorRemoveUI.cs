using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorRemoveUI : MonoBehaviour
{
    public Component uITrigger;

    public bool inRange;

    // Start is called before the first frame update
    void Start()
    {
        inRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(inRange == true && Input.GetKeyDown("e"))
        {
            Destroy(uITrigger);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        inRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        inRange = false;
    }
}
