using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iconesalvando : MonoBehaviour
{
    [SerializeField] private static Animator anim;

    [SerializeField] private float timeToReset = 0;
     private float timeToReset2 = 0;


    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();

        timeToReset2 = timeToReset + Time.time;
    }

    private void Update()
    {
        if (timeToReset2 <= Time.time)
        {
            anim.ResetTrigger("Salvando");
            timeToReset2 = timeToReset + Time.time;
        }
    }

    public static void Mostraricone()
    {
        anim.SetTrigger("Salvando");

    }


}
