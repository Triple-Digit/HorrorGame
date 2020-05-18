using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public static EnemyBehavior instance;
    public float walkSpeed, runSpeed;
    public float speed;
    public float distance;
    Transform playerPosition;
    Vector3 startPosition;
    Vector2 movement;
    Rigidbody2D body;
    public bool spottedPlayer, chasedPlayer, canMove;


    public WaypointManager waypoints;
    int waypointIndex;
    bool goingbackwardsthroughWaypoints;


    public Transform spawnPoint;
    public GameObject[] particles;
    public float steppingSpeed;
    float timeToSpawnFootStepSoundParticle;

    //Audio
    public bool saidVoiceline;
    public AudioSource voiceLine;
    public AudioSource footsteps;


    private void Awake()
    {
        instance = this;
        canMove = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        body = GetComponent<Rigidbody2D>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        speed = walkSpeed;
        Physics2D.queriesStartInColliders = false;
        startPosition = transform.position;

        //Audio
        saidVoiceline = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distance);
        if (canMove)
        {
            if (hitInfo.collider != null)
            {
                Debug.DrawLine(transform.position, hitInfo.point, Color.red);
                if (chasedPlayer && hitInfo.collider.tag != "Player")
                {
                    spottedPlayer = false;
                }
            }
            SpawnParitcles();
            if (spottedPlayer)
            {
                ChasePlayer();

                //Audio
                if (saidVoiceline == false)
                {
                    voiceLine.Play();
                    saidVoiceline = true;
                }
            }
            else
            {
                if (waypoints != null)
                {
                    transform.position = Vector2.MoveTowards(transform.position, waypoints.waypoints[waypointIndex].position, walkSpeed * Time.deltaTime);
                    chasedPlayer = true;
                    if (Vector2.Distance(transform.position, waypoints.waypoints[waypointIndex].position) < 0.1f)
                    {
                        if (waypointIndex < waypoints.waypoints.Length - 1 && !goingbackwardsthroughWaypoints)
                        {
                            waypointIndex++;
                        }
                        else
                        {
                            waypointIndex--;
                            goingbackwardsthroughWaypoints = true;

                            if (waypointIndex <= 0)
                            {
                                goingbackwardsthroughWaypoints = false;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            return;
        }
    }

    public void ChasePlayer()
    {
        Vector3 direction = playerPosition.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        body.rotation = angle;
        direction.Normalize();
        movement = direction;
        body.MovePosition(transform.position + (direction * speed * Time.deltaTime));
        chasedPlayer = true;
    }

    public void GoBack()
    {
        Vector3 headBackdirection = startPosition - transform.position;
        float angle = Mathf.Atan2(headBackdirection.y, headBackdirection.x) * Mathf.Rad2Deg;
        body.rotation = angle;
        headBackdirection.Normalize();
        movement = headBackdirection;
        body.MovePosition(transform.position + (headBackdirection * speed * Time.deltaTime));
    }

    public void playerSpotted()
    {
        spottedPlayer = true;
    }

    public void SpawnParitcles()
    {
        if (Time.time >= timeToSpawnFootStepSoundParticle)
        {

            if (chasedPlayer)
            {
                if(spottedPlayer)
                {
                    Instantiate(particles[0], spawnPoint.transform.position, spawnPoint.rotation);
                    steppingSpeed = 3f;
                }
                else
                {
                    Instantiate(particles[2], spawnPoint.transform.position, spawnPoint.rotation);
                    steppingSpeed = 1f;
                }
            }
            timeToSpawnFootStepSoundParticle = Time.time + 1 / steppingSpeed;
            footsteps.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.instance.PlayerCaught();
            canMove = false;
        }

    }



}
