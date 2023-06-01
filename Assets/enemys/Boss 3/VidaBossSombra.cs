using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VidaBossSombra : MonoBehaviour
{
    [Header("Vida do Boss")]
    [SerializeField] public static float vidaAtual = 0;

    [SerializeField] private static string cenaVitoria = "Tela Vitoria temp";

    [SerializeField] private static Transform cabeca;

    [SerializeField] private Transform cabecaRef;

    [SerializeField] private GameObject tileFinal;

    private static bool morreu = false;

    private void Start()
    {
        //tileFinal.SetActive(false);

        cabeca = cabecaRef;
    }
    public static void TomarDano(float Dano)
    {
        if ((vidaAtual - Dano) <= 0)
        {
            vidaAtual = vidaAtual - Dano;

            morreu = true;
        }
        else
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.SombraFeedBackDanoSombra, cabeca.position);
            vidaAtual = vidaAtual - Dano;
        }
    }

    private void Update()
    {
        Debug.Log("vida sombra:"+vidaAtual);
    }

    public float GetVidaAtual()
    {
        return vidaAtual;
    }
}
