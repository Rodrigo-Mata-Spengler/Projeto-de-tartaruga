using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeAllZombies : MonoBehaviour
{
    public GameObject[] ZombiesInScene;
    public bool PlayerHitTheGround = false;
    public bool AlreadyAwake = false;

    private bool Awaked = false;

    private float JumpVelocity;
    private GameObject Player;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        JumpVelocity = Player.GetComponent<PlayerMovement>().jumpVel;
    }

    private void Update()
    {
        if(PlayerHitTheGround && !AlreadyAwake && Awaked == false)
        {
            foreach(GameObject zombie in ZombiesInScene) 
            {
                zombie.GetComponent<BTZombiTurtle>().enabled = true;
            }
            Awaked = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHitTheGround = true;
            Player.GetComponent<PlayerMovement>().jumpVel = 10;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            Player.GetComponent<PlayerMovement>().jumpVel = JumpVelocity;
        }
    }
}

