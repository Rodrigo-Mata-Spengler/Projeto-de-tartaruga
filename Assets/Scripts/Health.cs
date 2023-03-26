using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float MaxHealth; // the maximum of life

    [SerializeField] private Slider HealthSlider;

    [Header("Current health")]
    public float CurrentHealth; // current life


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
