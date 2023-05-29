using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EnemyHitFeedback : MonoBehaviour
{
    public bool AddForce = true;
    private Rigidbody2D rb;
    [Space]
    [Header("Hit feedback")]
    public bool wasHit = false;

    public float ForceUp;
    public float ForceRight;

    [HideInInspector] public Vector2 Direction;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (wasHit && AddForce)
        {
            rb.AddForce(-Direction * ForceRight, ForceMode2D.Impulse);
            rb.AddForce(transform.up * ForceUp, ForceMode2D.Impulse);
            wasHit = false;
            
        }
    }

        
   
}
