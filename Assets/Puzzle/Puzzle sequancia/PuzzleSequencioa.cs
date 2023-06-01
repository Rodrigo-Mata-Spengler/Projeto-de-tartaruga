using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSequencioa : MonoBehaviour
{
    [SerializeField] public bool state = false;
    [SerializeField] private bool playerIn = false;
    [SerializeField] private float coolDown = 0;
    private float tempo;

    [SerializeField] private Sprite aceso;
    [SerializeField] private Sprite apagado;
    [SerializeField] private Sprite acertou;

    [SerializeField] private GameObject blocoDireita;
    [SerializeField] private GameObject blocoEsquerda;

    private bool doOnce = false;
    
    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = apagado;
        state = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerIn = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerIn = false;
    }

    private void Update()
    {
        if (Input.GetButton("Interacao") && playerIn)
        {
            ChangeState();
            ChangeStates();


            if (doOnce)
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.EstatuaGeniusA, this.transform.position);

                doOnce = false;
            }
            
        }
    }

    public void ChangeState()
    {
        if (tempo < Time.time)
        {
            tempo = Time.time + coolDown;
            if (state == false)
            {
                state = true;
                GetComponent<SpriteRenderer>().sprite = aceso;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = apagado;
                state = false;
            }
        }
        doOnce = false;
    }

    public void Aceto()
    {
        GetComponent<SpriteRenderer>().sprite = acertou;
    }

    private void ChangeStates()
    {
        if (blocoDireita)
        {
            blocoDireita.GetComponent<PuzzleSequencioa>().ChangeState();
        }
        if (blocoEsquerda)
        {
            blocoEsquerda.GetComponent<PuzzleSequencioa>().ChangeState();
        }
    }
 }
