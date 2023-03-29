using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteragirObjeto : MonoBehaviour
{
    [Header("Genius Controntroler")]
    [SerializeField] private GeniusControler ctrl;

    [SerializeField] private int numeroBloco;

    [SerializeField] private Color cor;

    private bool PlayerIn = false;

    private void Update()
    {
        if (Input.GetButton("Interacao") && PlayerIn)
        {
            ctrl.InteracaoObjeto(numeroBloco);

            this.GetComponent<SpriteRenderer>().color = cor;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerIn = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerIn = false;
    }
}
