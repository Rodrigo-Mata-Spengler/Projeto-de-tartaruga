using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivaHorda : MonoBehaviour
{
    public GameObject Wall;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.GetComponent<HordaManager>().enabled = true;
            Wall.SetActive(true);
        }
        
    }
}
