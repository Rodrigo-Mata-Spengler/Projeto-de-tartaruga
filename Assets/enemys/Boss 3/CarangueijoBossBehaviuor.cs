using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Boss3Status { idle, ataque1, ataque2, ataque3, ataque4, ataque5, ataque6};
//ataque 1: patadas em sequencia
//ataque 2: pata move de um lado para o outro
//ataque 3: as duas pinças atacão pelos dois lados
//ataque 4: carangueijo da uma patada no chão e cria ondas apartir das patadas
//ataque 5: tentaculos saindo do chão
//ataque 6: pinçada unica a onde o plyer estiver, a pinça fica presa no chão


public class CarangueijoBossBehaviuor : MonoBehaviour
{
    public bool tomarDescisoensProprias = true;
    
    [Header("Status")]
    [SerializeField] private Boss3Status status = Boss3Status.idle;
    private int num = 0;

    [Header("Idle")]
    [SerializeField] private float tempoIdle = 0;
    private float tempoIdleProximo = 0;

    [Header("Ataque 1")]
    [SerializeField] private GameObject[] pincas;
    [SerializeField] private float posInicial;
    [SerializeField] private float posFinal;
    [SerializeField] private float velocidadeAtaque1 = 0;
    [SerializeField] private float tempoAtaque1 = 0;
    private float tempoAtaque1Proximo = 0;

    private void Start()
    {
        tempoIdleProximo = tempoIdle + Time.time;
        
    }

    private void Update()
    {
        switch (status)
        {
            case Boss3Status.idle:
                IdleBoss();
                break;
            case Boss3Status.ataque1:
                
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
        
    }

    //Controla o idle do boss
    private void IdleBoss()
    {
        if (tempoIdleProximo <= Time.time)
        {
            BossBrain();
        }
    }

    

    /*private void Ataque1()
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
    }*/


}
