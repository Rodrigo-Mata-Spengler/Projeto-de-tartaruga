using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastPrefab : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;

    private void Start()
    {
        rb.velocity = transform.right * speed;
    }
    private void Update()
    {
        Destroy(gameObject, 1f);
    }
}
