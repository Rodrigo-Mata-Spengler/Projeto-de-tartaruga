using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuedaPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private float drag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().drag = drag;
        }
    }
}
