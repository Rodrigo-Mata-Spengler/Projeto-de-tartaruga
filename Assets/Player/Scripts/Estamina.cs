using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Estamina : MonoBehaviour
{
    public float MaxEstamina; // the maximum of stamina

    [SerializeField] private Slider EstaminaSlider;

    [Header("Current Estamina")]
    public float CurrentEstamina;


    private void Start()
    {
        CurrentEstamina = MaxEstamina;
        EstaminaSlider.maxValue = MaxEstamina;
        EstaminaSlider.value = MaxEstamina;
    }



    // Do damage
    public void Damage(float GiveEstaminaDamageAmount)
    {
        CurrentEstamina -= GiveEstaminaDamageAmount;
        EstaminaSlider.value -= 5;
    }

    //give Estamina
    public void GiveHealth(float GiveEstaminaAmount)
    {
        CurrentEstamina += GiveEstaminaAmount;
        EstaminaSlider.value += 5;
    }
}
