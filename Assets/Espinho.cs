using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espinho : MonoBehaviour
{
    public Transform TransportPosition;

    public float TeleportTimeCoolDown = 0;
    public float TeleportTime = 0;

    [SerializeField]private bool Detected;

    private GameObject Player;
    private void Update()
    {
        if(Detected)
        {
            
            if (Time.time > TeleportTime)
            {
               Player.transform.position = new Vector2(TransportPosition.position.x, TransportPosition.position.y);
                Detected = false;
            }


        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TeleportTime = Time.time + TeleportTimeCoolDown;
            Player = collision.gameObject;
            collision.gameObject.GetComponent<Health>().Damage(1);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.position.x, 15f);
            Detected= true;

        }
    }
}
