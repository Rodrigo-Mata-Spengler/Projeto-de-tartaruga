using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteragirObjeto : MonoBehaviour
{
    [Header("Genius Controntroler")]
    [SerializeField] private GeniusControler ctrl;

    [SerializeField] private int numeroBloco;

    [SerializeField] private Sprite apagado;

    [SerializeField] private Sprite aceso;

    [SerializeField] private Color cor;

    private bool PlayerIn = false;

    public bool permitirInteracao = false;

    private SpriteRenderer rend;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();

        BlocoApagado();
    }

    private void Update()
    {
        if (Input.GetButton("Interacao") && PlayerIn && permitirInteracao)
        {
            ctrl.InteracaoObjeto(numeroBloco);

            // this.GetComponent<SpriteRenderer>().color = cor;
            BlocoAceso();
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

    public void BlocoApagado()
    {
        rend.sprite = apagado;
    }

    public void BlocoAceso()
    {
        rend.sprite = aceso;
    }
}
