using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivarHorda : MonoBehaviour
{
    public HordaManager HordaManager;

    public GameObject Wall;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HordaManager.enabled = true;
            Wall.SetActive(true);
        }
    }
}
