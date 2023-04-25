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
        if (collision.CompareTag("Enemy"))
        {
            collision.transform.GetComponent<Rigidbody2D>().AddForce(this.transform.up* EnemyimpulseForce);
            collision.transform.GetComponent<EnemyHitFeedback>().wasHit = true;
            collision.transform.GetComponent<EnemyHealth>().Damage(DamageAmount);
            Detected = true;
        }

    }
}
