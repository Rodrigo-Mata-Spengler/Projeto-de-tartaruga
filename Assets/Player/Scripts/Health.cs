using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Health : MonoBehaviour
{
    private Rigidbody2D rb;
    //public float CurrentHealth; // current life
    public int lifeBar;

    //public float MaxHealth; // the maximum of life
    [SerializeField] private GameObject[]lifeImages;


    private CinemachineImpulseSource impulseSource;
    private void Start()
    {
        //CurrentHealth = MaxHealth;

        rb = gameObject.GetComponent<Rigidbody2D>();

        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void Update()
    {
        if(lifeBar <= 0f)
        {
            Debug.Log("morreu");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Damage(1);
            rb.AddForce(transform.up * 800);


        }
    }

    // Do damage
    public void Damage(int GiveDamageAmount)
    {
        CameraShakeManager.instance.CameraShake(impulseSource);
        lifeBar -= GiveDamageAmount;
        lifeImages[lifeBar].gameObject.SetActive(false);
    }

    //give heath
    public void GiveHealth(int GiveHealthAmount)
    {
        lifeBar += GiveHealthAmount;
        lifeImages[lifeBar].gameObject.SetActive(true);
    }
}
