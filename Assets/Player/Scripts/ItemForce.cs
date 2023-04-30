using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemForce : MonoBehaviour
{
    public float forceY;

    private float forceX;

    private void Start()
    {
        forceX = Random.Range(0f, 10f);
    }
    private void Awake()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(forceX, forceY);
    }
}
