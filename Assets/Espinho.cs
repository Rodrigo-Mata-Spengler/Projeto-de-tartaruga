using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espinho : MonoBehaviour
{
    public Transform TransportPosition;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().Damage(1);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.position.x, 15f);

            collision.gameObject.transform.position = new Vector2(TransportPosition.position.x, TransportPosition.position.y);

        }
    }
}
