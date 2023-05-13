using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Estamina : MonoBehaviour
{
    public float MaxEstamina; // the maximum of stamina

    [SerializeField] private Slider EstaminaSlider;

    [Header("Current Estamina")]
    [Range(0,10)]
    public float CurrentEstamina;
    

    private void Start()
    {
        //CurrentEstamina = MaxEstamina;
        //EstaminaSlider.maxValue = MaxEstamina;
       
    }

    private void Update()
    {
        EstaminaSlider.value = CurrentEstamina;
    }
    private void Awake()
    {
        EstaminaSlider.value = CurrentEstamina;
    }
    // Do damage
    public void Damage(float GiveEstaminaDamageAmount)
    {
        
        if(CurrentEstamina > 0f)
        {
            CurrentEstamina -= GiveEstaminaDamageAmount;
            EstaminaSlider.value -= 1;
        }
        
    }

    //give Estamina
    public void GiveHealth(float GiveEstaminaAmount)
    {
        
        if(CurrentEstamina < MaxEstamina)
        {
            CurrentEstamina += GiveEstaminaAmount;
            EstaminaSlider.value += 1;
        }
        

    }
}
