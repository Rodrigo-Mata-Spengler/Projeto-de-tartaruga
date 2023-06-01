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

    private bool PlayerIn = false;

    public bool permitirInteracao = false;

    private SpriteRenderer rend;

    [SerializeField] private int som = 0;
    private bool doOnce = true;

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

            BlocoAceso();

            if (doOnce)
            {
                switch (som)
                {
                    case 0:
                        AudioManager.instance.PlayOneShot(FMODEvents.instance.EstatuaGeniusA, this.transform.position);
                        break;
                    case 1:
                        AudioManager.instance.PlayOneShot(FMODEvents.instance.EstatuaGeniusB, this.transform.position);
                        break;
                    case 2:
                        AudioManager.instance.PlayOneShot(FMODEvents.instance.EstatuaGeniusC, this.transform.position);
                        break;
                    case 3:
                        AudioManager.instance.PlayOneShot(FMODEvents.instance.EstatuaGeniusD, this.transform.position);
                        break;
                }

                doOnce = false;
            } 
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

        doOnce = true;
    }

    public void BlocoAceso()
    {
        rend.sprite = aceso;
    }
}
