using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundValues : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private int Amount;


    public void ChangeVolume()
    {

        slider.value += Amount;
        
        
    }
}
