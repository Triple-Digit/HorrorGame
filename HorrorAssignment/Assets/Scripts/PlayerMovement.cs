using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    //Variables for movement
    public Rigidbody2D body;
    public float walkSpeed, runSpeed;
    public float speed;
    Vector2 movement;
    bool running;
    public bool canMove;

    //Spawing Particles
    public Transform spawnPoint;
    public GameObject[] particles;
    public float steppingSpeed;
    float timeToSpawnFootStepSoundParticle;

    //Audio 
    public AudioSource clap;
    public AudioSource walk;

    private void Awake()
    {
        instance = this;
        canMove = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        speed = walkSpeed;
    }

    void Update()
    {
        if(canMove)
        {
            MovemnetInput();
            SpawnParticles();
        }
        else
        {
            return;
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            Movement();
        }
        else
        {
            movement.x = 0f;
            movement.x = 0f;
        }
        
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
                    walk.pitch = Random.Range(0.8f, 1.0f);
                    walk.volume = 0.6f;
                    walk.Play();
                }
                else if (!running)
                {
                    walk.pitch = Random.Range(0.8f, 1.0f);
                    walk.volume = 0.2f;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            canMove = false;
        }
    }
}
