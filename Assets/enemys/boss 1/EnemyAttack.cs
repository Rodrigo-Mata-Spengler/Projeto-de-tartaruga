using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private BoxCollider2D collider;
    public bool Detected = false; // detecte if a enemy was inside
    public Rigidbody2D rb;
    [Space]
    public float impulseForce;
    public float ImpulseForceOnPlayer;
    private int HitIndex = 0;
    [Space]
    public int Damage;

    private void Start()
    {
        collider = this.GetComponent<BoxCollider2D>();
    }
    private void Update()
    {

        if(Detected)
        {
            if (HitIndex == 0)
            {
                HitIndex = 1;
                rb.AddForce(-transform.right * impulseForce);

            }

        }
        else
        {
            Detected = false;
            HitIndex = 0;
        }


    }
    //Draw the box on unity
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            //collision.transform.GetComponent<Rigidbody2D>().AddForce(this.transform.right * ImpulseForceOnPlayer);
            collision.transform.GetComponent<PlayerHitFeedback>().wasHit = true;
            collision.transform.GetComponent<Health>().Damage(Damage);
            Detected = true;

            collider.enabled = false;
        }
        
    }
}
