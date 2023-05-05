using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarangueijoDano : MonoBehaviour
{
    [SerializeField] private CarangueijoBehaviour caranga;
    [SerializeField] private string playerTag;
    [SerializeField] private int dano;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            collision.GetComponent<Health>().Damage(dano);
        }
    }
}
