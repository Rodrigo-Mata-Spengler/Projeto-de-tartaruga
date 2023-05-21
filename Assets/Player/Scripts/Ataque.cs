using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class Ataque : MonoBehaviour
{
    [HideInInspector]public bool HaveMagicTrident =false;

    public bool Detected = false; // detecte if a enemy was inside
    public Rigidbody2D rb;

    public float DamageAmount;


    [HideInInspector]public bool up, down, right;
    public float impulseForce;
    public float EnemyimpulseForce;
    [HideInInspector]public int HitIndex = 0;

    private Estamina playerEstamina;

    

    public VisualEffect HitEffect;
    public VisualEffect HitEffectUp;
    public VisualEffect HitEffectDown;
    private void Start()
    {
        playerEstamina = GameObject.FindGameObjectWithTag("Player").GetComponent<Estamina>();
    }
    private void Update()
    {
        //checks if hit a enemy
        if (Detected)
        {
            rb.velocity= Vector3.one;
            if (HitIndex == 0)
            {
                HitIndex = 1;
            }

            if (up)
            {
                rb.AddForce(transform.up * -impulseForce);
                HitEffectUp.Play();
                up = false;
            }
            if (down)
            {
                rb.AddForce(transform.up * impulseForce);
                HitEffectDown.Play();
                down = false;
            }
            if (right)
            {
                rb.AddForce(transform.right * -impulseForce);
                right = false;
                HitEffect.Play();
            }
        }

    }
    //Draw the box on unity
    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Zombi"))
        {
            collision.transform.GetComponent<Rigidbody2D>().AddForce(collision.transform.right * EnemyimpulseForce);
            collision.transform.GetComponent<EnemyHitFeedback>().wasHit = true;
            collision.transform.GetComponent<EnemyHealth>().Damage(DamageAmount);
            AudioManager.instance.PlayOneShot(FMODEvents.instance.FeedBackDanoZombi, collision.transform.position);
            Detected = true;
        }
        else if (collision.CompareTag("Guardiao") )
        {
            //collision.transform.GetComponent<Rigidbody2D>().AddForce(collision.transform.right * EnemyimpulseForce);
            collision.transform.GetComponent<EnemyHitFeedback>().wasHit = true;
            collision.transform.GetComponent<GuardiaoHealth>().Damage(DamageAmount);
            AudioManager.instance.PlayOneShot(FMODEvents.instance.FeedBackDanoGuardiao, collision.transform.position);
            Detected = true;
        }
        else if (collision.CompareTag("Mosca") )
        {
            //collision.transform.GetComponent<Rigidbody2D>().AddForce(this.transform.right * EnemyimpulseForce);
            collision.transform.GetComponent<EnemyHitFeedback>().wasHit = true;
            collision.transform.GetComponent<EnemyHealth>().Damage(DamageAmount);
            AudioManager.instance.PlayOneShot(FMODEvents.instance.FeedBackDanoMosca, collision.transform.position);
            Detected = true;
        }
        else if (collision.CompareTag("Planta") )
        {
            collision.transform.GetComponent<EnemyHitFeedback>().wasHit = true;
            collision.transform.GetComponent<EnemyHealth>().Damage(DamageAmount);
            AudioManager.instance.PlayOneShot(FMODEvents.instance.FeedBackDanoPlanta, collision.transform.position);
            Detected = true;
        }
        else if (collision.CompareTag("Caranguejo"))
        {
            collision.transform.GetComponent<EnemyHitFeedback>().wasHit = true;
            collision.transform.GetComponent<EnemyHealth>().Damage(DamageAmount);
            AudioManager.instance.PlayOneShot(FMODEvents.instance.FeedBackDanoCaranguejo, collision.transform.position);
            Detected = true;
        }
        else if (collision.CompareTag("ourico"))
        {
            collision.transform.GetComponent<EnemyHitFeedback>().wasHit = true;
            collision.transform.GetComponent<EnemyHealth>().Damage(DamageAmount);
            //AudioManager.instance.PlayOneShot(FMODEvents.instance., collision.transform.position);
            Detected = true;
        }
        else if (collision.CompareTag("Sombra"))
        {
            VidaBossSombra.TomarDano(DamageAmount);
            Detected = true;
        }

        if (collision.gameObject.layer == 8  && playerEstamina.enabled == true && playerEstamina.CurrentEstamina < playerEstamina.MaxEstamina)
        {
            
            if(playerEstamina.AttackToGetEstamina >= playerEstamina.amountOfHitToGiveEstamina)
            {
                playerEstamina.GiveEstamina(1);
                playerEstamina.AttackToGetEstamina = 0;

            }
            else
            {
                playerEstamina.AttackToGetEstamina += 1;
            }

        }

    }
}
