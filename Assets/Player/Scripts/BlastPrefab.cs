using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastPrefab : MonoBehaviour
{



    public Rigidbody2D rb;
    public float speed;

    public bool Detected = false; // detecte if a enemy was inside
    public float EnemyimpulseForce;
    [HideInInspector] public int HitIndex = 0;
    public float DamageAmount;
    private void Start()
    {
        rb.velocity = transform.right * speed;
    }
    private void Update()
    {

        Destroy(gameObject, 1f);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Zombi"))
        {
            collision.transform.GetComponent<Rigidbody2D>().AddForce(this.transform.right * EnemyimpulseForce);
            collision.transform.GetComponent<EnemyHitFeedback>().wasHit = true;
            collision.transform.GetComponent<EnemyHealth>().Damage(DamageAmount);
            AudioManager.instance.PlayOneShot(FMODEvents.instance.FeedBackDanoZombi, collision.transform.position);
            Detected = true;
        }
        if (collision.CompareTag("Guardiao"))
        {
            collision.transform.GetComponent<Rigidbody2D>().AddForce(this.transform.right * EnemyimpulseForce);
            collision.transform.GetComponent<EnemyHitFeedback>().wasHit = true;
            collision.transform.GetComponent<EnemyHealth>().Damage(DamageAmount);
            AudioManager.instance.PlayOneShot(FMODEvents.instance.FeedBackDanoGuardiao, collision.transform.position);
            Detected = true;
        }
        if (collision.CompareTag("Mosca"))
        {
            //collision.transform.GetComponent<Rigidbody2D>().AddForce(this.transform.right * EnemyimpulseForce);
            collision.transform.GetComponent<EnemyHitFeedback>().wasHit = true;
            collision.transform.GetComponent<EnemyHealth>().Damage(DamageAmount);
            AudioManager.instance.PlayOneShot(FMODEvents.instance.FeedBackDanoMosca, collision.transform.position);
            Detected = true;
        }
        if (collision.CompareTag("Planta"))
        {
            collision.transform.GetComponent<EnemyHitFeedback>().wasHit = true;
            collision.transform.GetComponent<EnemyHealth>().Damage(DamageAmount);
            AudioManager.instance.PlayOneShot(FMODEvents.instance.FeedBackDanoPlanta, collision.transform.position);
            Detected = true;
        }
        if (collision.CompareTag("Caranguejo"))
        {
            collision.transform.GetComponent<EnemyHitFeedback>().wasHit = true;
            collision.transform.GetComponent<EnemyHealth>().Damage(DamageAmount);
            AudioManager.instance.PlayOneShot(FMODEvents.instance.FeedBackDanoCaranguejo, collision.transform.position);
            Detected = true;
        }
        if (collision.CompareTag("ourico"))
        {
            collision.transform.GetComponent<EnemyHitFeedback>().wasHit = true;
            collision.transform.GetComponent<EnemyHealth>().Damage(DamageAmount);
            //AudioManager.instance.PlayOneShot(FMODEvents.instance., collision.transform.position);
            Detected = true;
        }
        if (collision.CompareTag("BlastWall"))
        {
            Destroy(collision.gameObject);
        }


    }
}
