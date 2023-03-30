using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSequencioa : MonoBehaviour
{
    [SerializeField] public bool state = false;
    [SerializeField] private bool playerIn = false;
    [SerializeField] private float coolDown = 0;
    private float tempo;

    [SerializeField] private GameObject blocoDireita;
    [SerializeField] private GameObject blocoEsquerda;
    
    private void Start()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
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
                GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.white;
                state = false;
            }
        }
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
