using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iconesalvando : MonoBehaviour
{
    [SerializeField] private static Animator anim;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    public static void Mostraricone()
    {
        anim.SetTrigger("Salvando");
    }


}
