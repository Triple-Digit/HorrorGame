using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject door;
    private Component doorCollider;

    public bool inRange;
    public bool isOpen;
    public AudioSource open;

    //Particle Test
    public Transform spawnPoint;
    public GameObject particles;

    // Start is called before the first frame update
    void Start()
    {
        inRange = false;
        isOpen = false;
        doorCollider = door.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isOpen == false && inRange && Input.GetKeyDown("e"))
        {
            open.Play();
            Destroy(doorCollider, 0.5f);
            Instantiate(particles, spawnPoint.transform.position, spawnPoint.rotation);
            isOpen = true;
            //Debug.Log("Particles!"); // <- That works... So why aren't the particles spawning...
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
