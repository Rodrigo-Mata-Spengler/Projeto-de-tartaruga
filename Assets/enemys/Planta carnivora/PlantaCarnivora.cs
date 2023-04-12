using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum StatusPlanta { dormindo, atacando, esperando};
public class PlantaCarnivora : MonoBehaviour
{
    [Header("Status Planta")]
    [SerializeField] private StatusPlanta status;

    [Header("detecção player")]
    [SerializeField] private GameObject player;
    [SerializeField] private string playerTag;
    [SerializeField] private float areaDeteccao = 0;
    private CircleCollider2D colisorDeteccao;

    [Header("Ataque")]
    [SerializeField] private float distanciaMaximaAtaque = 0;
    [SerializeField] private float tempoEsperaAtaque = 0;
    private float tempoEsperaAtaqueProximo = 0;
    [SerializeField] private float velocidadeAtaque = 0;
    private float velocidadeAtaquetempo = 0;
    [SerializeField] private float quantidadeAtaques = 0;
    private float quantidadeAtaqueAtual = 0;

    private void Start()
    {
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
            tempoEsperaAtaqueProximo = Time.time + tempoEsperaAtaque;
        }
    }

    //Espera um tempo até atacar de novo
    private void EsperaAtaque()
    {
        if (tempoEsperaAtaqueProximo < Time.time)
        {
            status = StatusPlanta.atacando;
        }
    }

    //em modo te ataque ela tenta atacar o player varias vezez consecutivas


    //depois desses ataques ela volta a dormir esperando detectar o player de novo
}
