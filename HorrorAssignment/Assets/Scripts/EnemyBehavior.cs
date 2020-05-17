using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public EnemyBehavior instance;
    public float walkSpeed, runSpeed;
    public float speed;
    public float distance;
    Transform playerPosition;
    Vector3 startPosition;
    Vector2 movement;
    Rigidbody2D body;
    public bool spottedPlayer, chasedPlayer;


    public WaypointManager waypoints;
    int waypointIndex;
    bool goingbackwardsthroughWaypoints;


    public Transform spawnPoint;
    public GameObject[] particles;
    public float steppingSpeed;
    float timeToSpawnFootStepSoundParticle;




    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        body = GetComponent<Rigidbody2D>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        speed = walkSpeed;
        Physics2D.queriesStartInColliders = false;
        startPosition = transform.position;       
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distance);
        if (hitInfo.collider != null)
        {
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);
            if(chasedPlayer && hitInfo.collider.tag != "Player")
            {
                spottedPlayer = false;
            }
        }
        SpawnParitcles();
        if (spottedPlayer)
        {
            ChasePlayer();
        }
        else
        {
            if(waypoints != null)
            {
                transform.position = Vector2.MoveTowards(transform.position, waypoints.waypoints[waypointIndex].position, speed * Time.deltaTime);
                if(Vector2.Distance(transform.position, waypoints.waypoints[waypointIndex].position) < 0.1f)
                {
                    if (waypointIndex < waypoints.waypoints.Length - 1 && !goingbackwardsthroughWaypoints)
                    {
                        waypointIndex++;
                    }
                    else
                    {
                        waypointIndex--;
                        goingbackwardsthroughWaypoints = true;

                        if(waypointIndex <= 0)
                        {
                            goingbackwardsthroughWaypoints = false;
                        }
                    }
                }
            }
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
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameManager.instance.PlayerCaught();
        }
    }



}
