using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteragirObjeto : MonoBehaviour
{
    [Header("Genius Controntroler")]
    [SerializeField] private GeniusControler ctrl;

    [SerializeField] private int numeroBloco;

    [SerializeField] private Color cor;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetButton("Interacao"))
        {
            ctrl.InteracaoObjeto(numeroBloco);

            this.GetComponent<SpriteRenderer>().color = cor;
        }
    }
}
