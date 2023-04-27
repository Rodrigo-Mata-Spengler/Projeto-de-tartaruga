using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Player SFX")]
    [field: SerializeField] public EventReference PlayerFootstep { get; private set; }
    [field: SerializeField] public EventReference AttackSound { get; private set; }
    [field: SerializeField] public EventReference DamageFeedback { get; private set; }

    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found More than one Audio Manager in the scene");
        }
        instance = this;
    }
}
