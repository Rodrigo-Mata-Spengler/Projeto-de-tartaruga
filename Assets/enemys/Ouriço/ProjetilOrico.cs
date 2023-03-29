using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjetilOrico : MonoBehaviour
{
    [SerializeField] private string playerTag;
    [SerializeField] private float Dano;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            collision.GetComponent<Health>().Damage(Dano);

        }
    }
}
