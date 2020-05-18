using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTrigger : MonoBehaviour
{
    public static EndLevelTrigger instance;

    public void Awake()
    {
        instance = this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            
            GameManager.instance.FinishedLevel();
            EnemyBehavior.instance.canMove = false;
            PlayerMovement.instance.canMove = false;
        }
    }
}
