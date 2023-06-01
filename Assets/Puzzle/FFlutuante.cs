using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FFlutuante : MonoBehaviour
{
    [SerializeField] private GameObject f;

    [SerializeField] private string tag;

    private void Start()
    {
        f.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(tag))
        {
            f.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(tag))
        {
            f.SetActive(false);
        }
    }
}
