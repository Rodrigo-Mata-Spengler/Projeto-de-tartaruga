using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Estamina : MonoBehaviour
{
    public float MaxEstamina;

    [SerializeField] private Slider EstaminaSlider;

    [Header("Current Estamina")]
    public float CurrentEstamina;


    private void Start()
    {
        CurrentEstamina = MaxEstamina;
    }



    // Do damage
    public void Damage(float GiveEstaminaDamageAmount)
    {
        CurrentEstamina -= GiveEstaminaDamageAmount;
    }

    //give Estamina
    public void GiveHealth(float GiveEstaminaAmount)
    {
        CurrentEstamina += GiveEstaminaAmount;
    }
}
