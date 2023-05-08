using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitFeedback : MonoBehaviour
{
    private Rigidbody2D rb;
    [Space]
    [Header("Hit feedback")]
    public bool wasHit = false;

    public float secondsToDisable;

    private Health PlayerHealth;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        PlayerHealth = gameObject.GetComponent<Health>();
    }
    private void Update()
    {
        if (wasHit)
        {
            PlayerHealth.enabled= false; //Disables the Player Health script
            StartCoroutine(DisableHitFeedback(secondsToDisable));
            AudioManager.instance.PlayOneShot(FMODEvents.instance.DamageFeedback, transform.position);
        }
    }
    private IEnumerator DisableHitFeedback(float seconds)
    {
        
        yield return new WaitForSeconds(seconds);
        wasHit = false;
        PlayerHealth.enabled = true;//Enables the Player Health script
    }
}
