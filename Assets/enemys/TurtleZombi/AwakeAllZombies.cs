using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeAllZombies : MonoBehaviour
{
    public GameObject[] ZombiesInScene;
    public bool PlayerHitTheGround = false;
    public bool AlreadyAwake = false;

    private void Update()
    {
        if(PlayerHitTheGround && !AlreadyAwake)
        {
            foreach(GameObject zombie in ZombiesInScene) 
            {
                zombie.GetComponent<BTZombiTurtle>().enabled = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHitTheGround = true;
        }
    }
}

