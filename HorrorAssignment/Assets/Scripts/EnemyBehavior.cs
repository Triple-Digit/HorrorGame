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
    public bool spottedPlayer;

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
            if(hitInfo.collider.tag != "Player")
            {
                
                GoBack();

            }
            
        }
        if(hitInfo.collider == null)
        {
            Debug.DrawLine(transform.position, transform.position + transform.right * distance, Color.green);
        }

        if(spottedPlayer)
        {
            ChasePlayer();
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
        Debug.Log("Enemy heard the player");
        spottedPlayer = true;
    }
}
