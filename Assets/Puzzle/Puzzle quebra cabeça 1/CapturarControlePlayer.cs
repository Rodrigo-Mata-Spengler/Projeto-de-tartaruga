using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapturarControlePlayer : MonoBehaviour
{
    private bool podeUsar = false;
    [SerializeField] private GameObject botaoFlutuante;
    [SerializeField] private PuzzleQuadrado puzzle;
    private GameObject player;
    private bool isPlay = false;

    private void Start()
    {
        botaoFlutuante.SetActive(false);
        //puzzle.enabled = false;
    }
    private void Update()
    {
        if (podeUsar && Input.GetButton("Interacao"))
        {
            if (isPlay == false)
            {
                //puzzle.enabled = true;
                player.GetComponent<PlayerMovement>().enabled = false;

                isPlay = true;
            }
            else
            {
               // puzzle.enabled = false;
                player.GetComponent<PlayerMovement>().enabled = true;

                isPlay = false;
            }
        }
    }
    //se o player entra na area selecionada, aparece a op��o de interagir com o puzzle
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            podeUsar = true;
            botaoFlutuante.SetActive(true);
            player = collision.gameObject;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            podeUsar = false;
            botaoFlutuante.SetActive(false);
        }
        
    }

    //se ele apertar o bot�o de interagir, os controles de movimenta��o para de funcionar
    //se ele apertar de novo o bot�o de intera��o, a movimenta��o volta
}
