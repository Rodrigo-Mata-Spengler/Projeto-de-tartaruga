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
    [SerializeField]private float tempo = 0;
    private float tempotemp = 0;


    private void Start()
    {
        botaoFlutuante.SetActive(false);
        puzzle.enabled = false;
    }
    private void Update()
    {
        if (podeUsar && Input.GetButton("Interacao") && tempotemp < Time.time)
        {
            if (isPlay == false)
            {
                puzzle.enabled = true;
                player.GetComponent<PlayerMovement>().enabled = false;

                isPlay = true;
                botaoFlutuante.SetActive(false);

                tempotemp = Time.time + tempo;
            }
            else
            {
                puzzle.enabled = false;
                player.GetComponent<PlayerMovement>().enabled = true;

                isPlay = false;
                botaoFlutuante.SetActive(true);

                tempotemp = Time.time + tempo;
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
