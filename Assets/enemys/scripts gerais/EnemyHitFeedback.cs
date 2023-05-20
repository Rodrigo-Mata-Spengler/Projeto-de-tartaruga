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

    

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (wasHit)
        {
            StartCoroutine(DisableHitFeedback(secondsToDisable));
           // gameObject.GetComponent<Animator>().SetBool("Dano", true);
            
        }
    }
    private IEnumerator DisableHitFeedback(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        //gameObject.GetComponent<Animator>().SetBool("Dano", false);
        wasHit = false;
        
        
    }
}
