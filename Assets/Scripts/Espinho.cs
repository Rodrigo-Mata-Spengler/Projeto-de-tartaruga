using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Espinho : MonoBehaviour
{
    public Transform TransportPosition;

    public float TeleportTimeCoolDown = 0;
    [HideInInspector]public float TeleportTime = 0;


    public float RespawnTimeCoolDown = 0;
    [HideInInspector] public float RespawnTime = 0;

    private bool Detected;
    private bool teleported = false;

    private GameObject Player;

    private Animation animationEspinho;

    private void Start()
    {
        animationEspinho = GameObject.FindGameObjectWithTag("EspinhoPanel").GetComponent<Animation>();
    }
    private void Update()
    {
        if(Detected)
        {
            
            if (Time.time > TeleportTime)
            {
               Player.transform.position = new Vector2(TransportPosition.position.x, TransportPosition.position.y);
                Detected = false;
                teleported = true;
                RespawnTime = Time.time + RespawnTimeCoolDown;

            }


        }

        if (teleported)
        {

            if (Time.time > RespawnTime)
            {
                animationEspinho.Play("epinhoTelaApagar");
                teleported= false;
                Player.GetComponent<PlayerMovement>().enabled = true;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            TeleportTime = Time.time + TeleportTimeCoolDown;
            Player = collision.gameObject;
            Player.GetComponent<Health>().Damage(1);
            Player.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.position.y, 15f);
            Player.GetComponent<PlayerMovement>().enabled = false;
            Detected= true;
            animationEspinho.Play("espinhoAnimatio");
  
        }
    }
}
