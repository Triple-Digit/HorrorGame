﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSound2 : MonoBehaviour
{
    public AudioSource sFX;
    public bool inRange;

    // Start is called before the first frame update
    void Start()
    {
        inRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(inRange && Input.GetKeyDown("e"))
        {
            sFX.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            inRange = false;
        }
    }
}