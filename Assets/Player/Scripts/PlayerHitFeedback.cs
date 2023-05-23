using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerHitFeedback : MonoBehaviour
{
    private Rigidbody2D rb;
    [Space]
    [Header("Hit feedback")]
    public bool wasHit = false;

    public float secondsToDisable;

    private Health PlayerHealth;

    private Animator animatorPlayer;

    public bool DoOnce = false;


    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        PlayerHealth = gameObject.GetComponent<Health>();
        animatorPlayer = this.GetComponent<Animator>();
    }
    private void Update()
    {
        if (wasHit)
        {
            PlayerHealth.enabled= false; //Disables the Player Health script
            animatorPlayer.SetTrigger("Dano");
            StartCoroutine(DisableHitFeedback(secondsToDisable));
        }

    }
    private IEnumerator DisableHitFeedback(float seconds)
    {
        
        yield return new WaitForSeconds(seconds);
        wasHit = false;
        PlayerHealth.enabled = true;//Enables the Player Health script
        DoOnce = true;
        animatorPlayer.ResetTrigger("Dano");
    }
}
