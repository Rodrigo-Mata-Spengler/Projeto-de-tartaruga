using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    //public float MaxHealth; // the maximum of life

    [SerializeField] private Slider HealthSlider;
    //public float CurrentHealth; // current life
    public int lifeBar;


    private void Start()
    {
        //CurrentHealth = MaxHealth;


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
        }
    }

    // Do damage
    public void Damage(int GiveDamageAmount)
    {
        lifeBar -= GiveDamageAmount;
    }

    //give heath
    public void GiveHealth(int GiveHealthAmount)
    {
        lifeBar += GiveHealthAmount;
    }
}
