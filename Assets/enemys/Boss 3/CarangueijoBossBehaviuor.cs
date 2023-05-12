using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Boss3Status { idle, vuneravel, ataque1, ataque2, ataque3, ataque4, ataque5, ataque6};
//ataque 1: patas caem do ceu no chão e ficam presas no chão por um tempo
//ataque 2: as piças atacão pela direita ou pela esquerda
//ataque 3: as duas pinças atacão pelos dois lados
//ataque 4: taca uma das piças no chão e arrasta pelo mapa
//ataque 5: tentaculos saindo do chão
//ataque 6: Tentaculos daem do chão


public class CarangueijoBossBehaviuor : MonoBehaviour
{
    public bool tomarDescisoensProprias = true;
    
    [Header("Status")]
    [SerializeField] private Boss3Status status = Boss3Status.idle;
    private int num = 0;

    [Header("Cabeça Boss")]
    [SerializeField] private GameObject head;
    [SerializeField] private SpriteRenderer spriteRend;
    [SerializeField] private Color corEscondido = Color.grey;
    [SerializeField] private Color corVuneravel = Color.white;
    [SerializeField] private float tempoVuneravel = 0;
    private float tempoVuneravelProximo = 0;

    [Header("Idle")]
    [SerializeField] private float tempoIdle = 0;
    private float tempoIdleProximo = 0;

    [Header("Ataque 1")]
    [SerializeField] private GameObject[] patas;
    [SerializeField] private float posInicial;
    [SerializeField] private float posFinal;
    [SerializeField] private float velocidadeAtaque1 = 0;
    [SerializeField] private float tempoAtaque1 = 0;
    private float tempoAtaque1Proximo = 0;
    private bool descendo = true;
    private bool ataque1ctrl = true;

    private void Start()
    {
        tempoIdleProximo = tempoIdle + Time.time;

        PreparaVunerabilidade();
        PrearaAtaque1();


    }

    private void Update()
    {
        switch (status)
        {
            case Boss3Status.idle:
                IdleBoss();
                break;
            case Boss3Status.vuneravel:
                Vuneravel();
                break;
            case Boss3Status.ataque1:
                Ataque1();
                break;
            case Boss3Status.ataque2:
                break;
            case Boss3Status.ataque3:
                break;
            case Boss3Status.ataque4:
                break;
            case Boss3Status.ataque5:
                break;
            case Boss3Status.ataque6:
                break;
        }
    }

    //Toma decisão para oque fazer agora
    private void BossBrain()
    {
        if (status == Boss3Status.vuneravel && tomarDescisoensProprias)
        {
             num = Random.Range(1,6);

            switch (num)
            {
                case 1:
                    //Ataque 1:
                    ataque1ctrl = true;
                    status = Boss3Status.ataque1;

                    break;
                case 2:
                    //Ataque 2:
                    status = Boss3Status.ataque2;

                    break;
                case 3:
                    //Ataque 3:
                    status = Boss3Status.ataque3;

                    break;
                case 4:
                    //Ataque 4:
                    status = Boss3Status.ataque4;

                    break;
                case 5:
                    //Ataque 5:
                    status = Boss3Status.ataque5;

                    break;
                case 6:
                    //Ataque 6:
                    status = Boss3Status.ataque6;

                    break;
            }

            
        }
        else if(status == Boss3Status.idle && tomarDescisoensProprias == true)
        {
            //entra em modo vuneravel apos um ataque, mudar isso depois !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            tempoVuneravelProximo = tempoVuneravel + Time.time;
            spriteRend.color = corVuneravel;
            status = Boss3Status.vuneravel;
            head.GetComponent<CapsuleCollider2D>().enabled = true;
            
        }else if(tomarDescisoensProprias == true)
        {
            tempoIdleProximo = tempoIdle + Time.time;
            status = Boss3Status.idle;
        }
    }

    //Controla o idle do boss
    private void IdleBoss()
    {
        if (tempoIdleProximo <= Time.time)
        {
            BossBrain();
        }
    }

    //deixa o Boss vuneravel a ataques
    private void PreparaVunerabilidade()
    {
        head.GetComponent<CapsuleCollider2D>().enabled = false;
        spriteRend.color = corEscondido;
    }

    private void Vuneravel()
    {
        if (tempoVuneravelProximo <= Time.time)
        {
            spriteRend.color = corEscondido;
            BossBrain();
            head.GetComponent<CapsuleCollider2D>().enabled = false;
        }
    }

    //Ataque 1:patas caem do ceu no chão e ficam presas no chão por um tempo
    private void PrearaAtaque1()
    {
        for (int i = 0; i < patas.Length; i++)
        {
            if (patas[i] != null)
            {
                patas[i].transform.position = new Vector3(patas[i].transform.position.x, posInicial,  0);
            }
        }
    }

    private void Ataque1()
    {
        if (ataque1ctrl == true)
        {
            tempoAtaque1Proximo = tempoAtaque1 + Time.time;
            ataque1ctrl = false;
        }
        if(descendo == true)
        {
            Ataque1Desce();
            if (patas[3].transform.position.y == posFinal)
            {
                for (int i = 0; i < patas.Length; i++)
                {
                    if (patas[i] != null)
                    {
                        patas[i].transform.GetChild(0).gameObject.SetActive(false);
                    }
                }
            }
            if (tempoAtaque1Proximo <= Time.time)
            {
                descendo = false;
            }
        }
        else
        {
            Ataque1Sobe();
            if(patas[3].transform.position == new Vector3(patas[3].transform.position.x, posInicial,  0))
            {
                for (int i = 0; i < patas.Length; i++)
                {
                    if (patas[i] != null)
                    {
                        patas[i].transform.GetChild(0).gameObject.SetActive(true);
                    }
                }
                BossBrain();
            }
        }
    }

    private void Ataque1Desce()
    {
        for (int i = 0; i < patas.Length; i++)
        {
            if (patas[i] != null)
            {
                patas[i].transform.position = Vector3.MoveTowards(patas[i].transform.position,new Vector3(patas[i].transform.position.x, posFinal,0), velocidadeAtaque1 * Time.deltaTime);
            }
        }
    }
    private void Ataque1Sobe()
    {
        for (int i = 0; i < patas.Length; i++)
        {
            if (patas[i] != null)
            {
                patas[i].transform.position = Vector3.MoveTowards(patas[i].transform.position, new Vector3(patas[i].transform.position.x, posInicial, 0), velocidadeAtaque1 * Time.deltaTime);
            }
        }
    }


}
