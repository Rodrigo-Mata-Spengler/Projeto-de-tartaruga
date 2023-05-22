using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class Estamina : MonoBehaviour
{
    public float MaxEstamina; // the maximum of stamina

    private GameObject EstaminaSlider;

    [Header("Current Estamina")]
    [Range(0,10)]
    public float CurrentEstamina;

    public int AttackToGetEstamina = 0;

   [HideInInspector] public int amountOfHitToGiveEstamina = 0;
    private void Start()
    {
        //CurrentEstamina = MaxEstamina;
        //EstaminaSlider.maxValue = MaxEstamina;
        amountOfHitToGiveEstamina = 3;
        EstaminaSlider = GameObject.FindGameObjectWithTag("EstaminaSlider");
    }

    private void Update()
    {
        
        
    }
    private void Awake()
    {

        EstaminaSlider.GetComponent<Slider>().value = CurrentEstamina;
    }
    // Do damage
    public void Damage(float GiveEstaminaDamageAmount)
    {
        
        if(CurrentEstamina > 0f)
        {
            CurrentEstamina -= GiveEstaminaDamageAmount;
            EstaminaSlider.GetComponent<Slider>().value -= GiveEstaminaDamageAmount;

            
        }
        
    }

    //give Estamina
    public void GiveEstamina(float GiveEstaminaAmount)
    {
        
        if(CurrentEstamina < MaxEstamina)
        {
            CurrentEstamina += GiveEstaminaAmount;
            EstaminaSlider.GetComponent<Slider>().value += GiveEstaminaAmount;
            amountOfHitToGiveEstamina = Random.Range(4, 7);
        }
        

    }
    private void OnEnable()
    {
        EstaminaSlider.gameObject.SetActive(true);
    }
}
