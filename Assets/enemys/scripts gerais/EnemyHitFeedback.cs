using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EnemyHitFeedback : MonoBehaviour
{
    private Rigidbody2D rb;
    [Space]
    [Header("Hit feedback")]
    public bool wasHit = false;
    public float secondsToDisable;

    public float ForceUp;
    public float ForceRight;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (wasHit)
        {
            rb.AddForce(transform.right * ForceRight, ForceMode2D.Impulse);
            rb.AddForce(transform.up * ForceUp, ForceMode2D.Impulse);
            wasHit = false;
            Debug.Log("Aquii");
        }
    }

        
   
}
