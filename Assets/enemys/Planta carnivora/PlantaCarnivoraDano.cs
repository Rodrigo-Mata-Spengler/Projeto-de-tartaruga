using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantaCarnivoraDano : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float impulseForceOnPlayer;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            collision.transform.GetComponent<Rigidbody2D>().AddForce(this.transform.right * impulseForceOnPlayer);
            collision.transform.GetComponent<PlayerHitFeedback>().wasHit = true;
            collision.transform.GetComponent<Health>().Damage(damage);
        }

    }
}
