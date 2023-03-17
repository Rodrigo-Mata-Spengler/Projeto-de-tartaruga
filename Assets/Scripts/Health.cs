using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float MaxHealth;

    [SerializeField] private Slider HealthSlider;

    [Header("Current health")]
    public float CurrentHealth;


    private void Start()
    {
        CurrentHealth = MaxHealth;
    }



    // Do damage
    public void Damage(float GiveDamageAmount)
    {
        CurrentHealth -= GiveDamageAmount;
    }

    //give heath
    public void GiveHealth(float GiveHealthAmount)
    {
        CurrentHealth += GiveHealthAmount;
    }
}
