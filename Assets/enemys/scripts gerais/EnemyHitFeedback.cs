using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitFeedback : MonoBehaviour
{
    private Rigidbody2D rb;
    [Space]
    [Header("Hit feedback")]
    public bool wasHit = false;
    public float impulseForce;
    public float secondsToDisable;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (wasHit)
        {
            rb.AddForce((transform.right * -1) * impulseForce);
            StartCoroutine(DisableHitFeedback(secondsToDisable));
        }
    }
    private IEnumerator DisableHitFeedback(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        wasHit = false;
    }
}
