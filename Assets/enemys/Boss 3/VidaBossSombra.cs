using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VidaBossSombra : MonoBehaviour
{
    [Header("Vida do Boss")]
    [SerializeField] public static float vidaAtual = 0;

    [SerializeField] private static string cenaVitoria = "Tela Vitoria temp";

    //[SerializeField] private GameObject tileFinal;

    private void Start()
    {
        //tileFinal.SetActive(false);
    }
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
        SceneManager.LoadScene(cenaVitoria);
        //ativar quando tiver o chão
        /*
        tileFinal.SetActive(true);
         */
    }
}
