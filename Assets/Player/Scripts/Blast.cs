using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : MonoBehaviour
{
    public Transform ShootPoint;
    public GameObject BlastPrefab;
    public Animator efeito;

    public float FireRate = 15f;
    public float NextTimeToFire = 0f;

    [Header("Estamina")]
    public int EstaminaDamage;
    private Estamina estaminaScript;

 
    private Animator PlayerAnimator;
    
    private void Start()
    {
        estaminaScript = this.GetComponent<Estamina>();
        PlayerAnimator = this.GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Blast") && estaminaScript.CurrentEstamina > 0)
        {
            efeito.SetBool("efeito", true);
            PlayerAnimator.SetTrigger("Conjura");
        }
        if (Input.GetButtonDown("Blast") && Time.time >= NextTimeToFire && estaminaScript.CurrentEstamina > 0)
        {
           
            NextTimeToFire = Time.time + 1f / FireRate;
            Debug.Log("shoot");
            Shoot();
        }
        if(NextTimeToFire > Time.time)
        {
            efeito.SetBool("efeito", false);
        }
    }
    private void Shoot()
    {
        Instantiate(BlastPrefab, ShootPoint.position, ShootPoint.rotation);
        estaminaScript.Damage(EstaminaDamage);
        AudioManager.instance.PlayOneShot(FMODEvents.instance.Blast, transform.position);
    }
}
