using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaBossSombra : MonoBehaviour
{
    [Header("Vida do Boss")]
    [SerializeField] public static float vidaAtual = 0;

    public static void TomarDano(float Dano)
    {
        if (Dano-vidaAtual <= 0)
        {
            Morreu();
        }
        else
        {
            vidaAtual = Dano - vidaAtual;
        }
    }

    private static void Morreu()
    {

    }
}
