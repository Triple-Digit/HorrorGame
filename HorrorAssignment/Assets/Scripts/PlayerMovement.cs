using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerMovement instance;

    //Variables for movement
    public Rigidbody2D body;
    public float walkSpeed, runSpeed;
    public float speed;
    Vector2 movement;
    bool running;

    //Spawing Particles
    public Transform spawnPoint;
    public GameObject[] particles;
    public float steppingSpeed;
    float timeToSpawnFootStepSoundParticle;

    //Audio 
    public AudioSource clap;
    public AudioSource walk;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        
        speed = walkSpeed;
    }

    void Update()
    {
        MovemnetInput();
        SpawnParticles();
    }

    private void FixedUpdate()
    {
        Movement();
    }


    public void MovemnetInput()
    {

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
            running = true;
        }
        else
        {
            speed = walkSpeed;
            running = false;
        }
    }

    public void Movement()
    {
        body.MovePosition(body.position + movement * speed * Time.fixedDeltaTime);
    }

    public void SpawnParticles()
    {
        if(movement.x != 0f || movement.y != 0f)
        {
            if(Time.time >= timeToSpawnFootStepSoundParticle)
            {
                if (running)
                {
                    Instantiate(particles[1], spawnPoint.transform.position, spawnPoint.rotation);
                }
                else
                {
                    Instantiate(particles[0], spawnPoint.transform.position, spawnPoint.rotation);
                }

                timeToSpawnFootStepSoundParticle = Time.time + 1 / steppingSpeed;

                if (running)
                {
                    walk.pitch = Random.Range(1.0f, 1.2f);
                    walk.volume = 0.8f;
                    walk.Play();
                }
                else if (!running)
                {
                    walk.pitch = Random.Range(1.0f, 1.2f);
                    walk.volume = 0.3f;
                    walk.Play();
                }
            }
            
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(particles[2], spawnPoint.transform.position, spawnPoint.rotation);
            clap.pitch = Random.Range(0.9f, 1.1f);
            clap.Play();
        }

    }
}
