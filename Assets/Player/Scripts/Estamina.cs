using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class Estamina : MonoBehaviour
{
    public float MaxEstamina; // the maximum of stamina

    public bool hasEstamina = false;
    public bool hasEstamina2 = false;

    [Header("Current Estamina")]
    public float CurrentEstamina;

    public int AttackToGetEstamina = 0;

   [HideInInspector] public int amountOfHitToGiveEstamina = 0;
    private void Start()
    {
        //CurrentEstamina = MaxEstamina;
        //EstaminaSlider.maxValue = MaxEstamina;
        amountOfHitToGiveEstamina = 3;
        HudControler.EnableMana(false);
        //EstaminaSlider = GameObject.FindGameObjectWithTag("EstaminaSlider");

        if (hasEstamina)
        {
            HudControler.EnableMana(true);
        }
        if (hasEstamina2)
        {
            HudControler.EnableMana2(true);
        }
    }
    // Do damage
    public void Damage(float GiveEstaminaDamageAmount)
    {
        
        if(CurrentEstamina > 0f)
        {
            CurrentEstamina -= GiveEstaminaDamageAmount;
            //EstaminaSlider.GetComponent<Slider>().value -= GiveEstaminaDamageAmount;
            HudControler.ChangeMana(GiveEstaminaDamageAmount * -1);
            
        }
        
    }

    //give Estamina
    public void GiveEstamina(float GiveEstaminaAmount)
    {
        
        if(CurrentEstamina < MaxEstamina)
        {
            CurrentEstamina += GiveEstaminaAmount;
            //EstaminaSlider.GetComponent<Slider>().value += GiveEstaminaAmount;
            HudControler.ChangeMana(GiveEstaminaAmount);
            amountOfHitToGiveEstamina = Random.Range(4, 7);
        }
        

    }
    private void OnEnable()
    {
        HudControler.EnableMana(true);
    }

    public void EnableMana()
    {
        HudControler.EnableMana(true);
        hasEstamina = true;
    }
}
