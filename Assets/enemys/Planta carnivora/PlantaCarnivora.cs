using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum StatusPlanta { dormindo, atacando, recuo, esperando};
public class PlantaCarnivora : MonoBehaviour
{
    [Header("Status Planta")]
    [SerializeField] private StatusPlanta status;

    [Header("Corpo Planta")]
    [SerializeField] private GameObject cabeca;
    private Vector3 posOriginal;

    [Header("detecção player")]
    [SerializeField] private GameObject player;
    [SerializeField] private string playerTag;
    [SerializeField] private float areaDeteccao = 0;
    [SerializeField]private CircleCollider2D colisorDeteccao;

    [Header("Recuo")]
    [SerializeField] private Vector3 distanciaRecuo;
    private Vector3 recuoPos;
    [SerializeField] private float tempoRecuo = 0;
    private float velocidadeTempoRecuo = 0;

    [Header("Ataque")]
    [SerializeField] private float distanciaMaximaAtaque = 0;
    private Vector3 posAtaque;
    [SerializeField] private float tempoEspera = 0;
    private float tempoEsperaProximo = 0;
    [SerializeField] private float velocidadeAtaque = 0;
    private float velocidadeAtaquetempo = 0;
    [SerializeField] private float quantidadeAtaques = 0;
    private float quantidadeAtaqueAtual = 0;

    private void Start()
    {
        posOriginal = cabeca.transform.position;
        colisorDeteccao.radius = areaDeteccao;
        status = StatusPlanta.dormindo;
    }

    private void Update()
    {
        switch (status)
        {
            case StatusPlanta.dormindo:
                IfPlayer();
                break;
            case StatusPlanta.atacando:
                PlantaAtaque();
                break;
            case StatusPlanta.recuo:
                PlantaRecuo();
                break;
            case StatusPlanta.esperando:
                EsperaAtaque();
                break;
        }
    }

    //esperar detectar o player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            player = collision.gameObject;
        }
    }

    //detectando o player ela entra em modo de ataque 
    private void IfPlayer()
    {
        if (player)
        {
            status = StatusPlanta.esperando;
            tempoEsperaProximo = Time.time + tempoEspera;
        }
    }

    //Espera um tempo até atacar de novo
    private void EsperaAtaque()
    {
        if (tempoEsperaProximo < Time.time)
        {
            recuoPos.x = cabeca.transform.position.x - distanciaRecuo.x;
            recuoPos.y = cabeca.transform.position.y - distanciaRecuo.y;
            recuoPos.z = 0;
            status = StatusPlanta.recuo;
            velocidadeTempoRecuo = Time.time + tempoRecuo;
        }
    }

    //em modo te ataque ela tenta atacar o player varias vezez consecutivas
    private void PlantaRecuo()
    {
        cabeca.transform.position = Vector3.MoveTowards(cabeca.transform.position, recuoPos, tempoRecuo * Time.deltaTime);

        if (velocidadeTempoRecuo < Time.time)
        {
            status = StatusPlanta.atacando;

            velocidadeAtaquetempo = velocidadeAtaque + Time.time;
        }
    }

    private void PosAtaque()
    {
        if(Vector3.Distance(cabeca.transform.position, player.transform.position) > distanciaMaximaAtaque)
        {
            posAtaque.x = posOriginal.x + distanciaMaximaAtaque;
            posAtaque.y = posOriginal.y + distanciaMaximaAtaque;
            posAtaque.z = 0;
            //não funciona
        }
        else
        {
            posAtaque.x = player.transform.position.x;
            posAtaque.y = player.transform.position.y;
            posAtaque.z = 0;
        }
    }

    private void PlantaAtaque()
    {
        cabeca.transform.position = Vector3.MoveTowards(cabeca.transform.position, posAtaque, velocidadeAtaque * Time.deltaTime);

        if (velocidadeAtaquetempo < Time.time)
        {
            if (quantidadeAtaqueAtual < quantidadeAtaques)
            {
                PosAtaque();
                recuoPos.x = cabeca.transform.position.x - distanciaRecuo.x;
                recuoPos.y = cabeca.transform.position.y - distanciaRecuo.y;
                recuoPos.z = 0;
                status = StatusPlanta.recuo;
                velocidadeTempoRecuo = Time.time + tempoRecuo;
                quantidadeAtaqueAtual++;
            }
            else
            {
                quantidadeAtaqueAtual = 0;
                player = null;
                status = StatusPlanta.dormindo;
            }
        }
    }

    //depois desses ataques ela volta a dormir esperando detectar o player de novo
}
