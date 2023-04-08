using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ataque : MonoBehaviour
{
    public bool Detected = false; // detecte if a enemy was inside
    public Rigidbody2D rb;

    public float DamageAmount;

    [HideInInspector] public bool up, down, right;
    public float impulseForce;
    public float EnemyimpulseForce;
    private int HitIndex = 0;

    private void Update()
    {
        //checks if hit a enemy
        if (Detected)
        {
            if (HitIndex == 0)
            {
                HitIndex = 1;
            }

            if (up)
            {
                rb.AddForce(transform.up * impulseForce);

                up = false;
            }
            if (down)
            {
                rb.AddForce(transform.up * -impulseForce);
                down = false;
            }
            if (right)
            {
                rb.AddForce(transform.right * -impulseForce);
                right = false;
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
        if (collision.CompareTag("Enemy"))
        {
            collision.transform.GetComponent<EnemyHitFeedback>().wasHit = true;
            collision.transform.GetComponent<EnemyHealth>().Damage(DamageAmount);
            Detected = true;
        }
        
    }
}
