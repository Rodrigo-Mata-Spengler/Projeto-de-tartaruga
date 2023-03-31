using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MoscaStatus {patrulha, alerta, kamikase, tonto, recuperacao, desativado};
public class MoscaBehavior : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] private MoscaStatus status = MoscaStatus.desativado;

    [Header("Patrulha")]
    //define limite de movimenta��o da mosca
    [SerializeField] private Transform limiteMax;
    [SerializeField] private Transform limiteMin;
    private float maxX = 0;
    private float maxY = 0;
    private float minX = 0;
    private float minY = 0;
    [SerializeField] private float velocidade = 0;//velocidade de movimetna��o da mosca
    [SerializeField] private float tempoEspera = 0;//tempo de espera que a mosca faz entre os caminhos
    private float tempoProximoPonto = 0;
    [SerializeField] private Vector3 pontoPatrulha;
    private Vector3 centroOriginal;

    [Header("Detec��o Player")]
    [SerializeField] private float raioDetec��o;
    [SerializeField] private string playerTag;
    private Transform player;

    [Header("Ataque kamikase")]
    [SerializeField] private float alturaAtaque = 0;
    private Vector3 cordenadaAlturaAtaque;
    [SerializeField] private float velocidadeAtaque;
    [SerializeField] private float velocidadesubida;
    [SerializeField] private Vector3 miraAtaque;

    [SerializeField] private float TempoEsperaAtaque = 0;
    private float tempoAtaque = 0;

    private void Start()
    {
        centroOriginal = transform.position;

        PrepararPatrulha();
        PreparaDeteccao();

        status = MoscaStatus.patrulha;
    }

    private void Update()
    {
        switch (status)
        {
            case MoscaStatus.patrulha:
                MoscaPatrulha();
                break;
            case MoscaStatus.alerta:
                AlturaAtaqueMosca();
                break;
            case MoscaStatus.kamikase:
                AtaqueMosca();
                break;
            case MoscaStatus.tonto:
                break;
            case MoscaStatus.recuperacao:
                break;
            case MoscaStatus.desativado:
                break;
        }
    }

    //mosca em patrulha (andando de um lado para o outro)
    private void PrepararPatrulha()
    {
        maxX = limiteMax.position.x;
        maxY = limiteMax.position.y;
        minX = limiteMin.position.x;
        minY = limiteMin.position.y;
    }
    private void MoscaPatrulha()
    {
        if (transform.position.Equals(pontoPatrulha) && tempoProximoPonto<Time.time)
        {
            tempoProximoPonto = Time.time + tempoEspera;
            //criar ponto aleat�rio para masca ir
            CriarPontoPatrulha();
        }
        else
        {
            //movimentar a mosca at� o ponto
            MoverMoscaPatrulha();
        }

        if (player)
        {
            status = MoscaStatus.alerta;
        }
    }
    private void CriarPontoPatrulha()
    {
        pontoPatrulha = new Vector3(Random.Range(minX,maxX),Random.Range(minY,maxY),0);
        
    }
    private void OnDrawGizmos()
    {
        //desenha rpoximo ponto da patrulha da mosca
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(pontoPatrulha,.5f);

        //desenha area na qual a mosca pode voar em patrulha
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(centroOriginal,new Vector3(maxX - minX,maxY - minY,0));
    }

    private void MoverMoscaPatrulha()
    {
        transform.position = Vector3.MoveTowards(transform.position,pontoPatrulha,velocidade*Time.deltaTime);
    }

    //mosca detecta o player
    private void PreparaDeteccao()
    {
        GetComponent<CircleCollider2D>().radius = raioDetec��o;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            player = collision.transform;
            CriaCordenadaAtaque();
        }
    }

    //mosca sobe a uma altura para atacar
    private void CriaCordenadaAtaque()
    {
        cordenadaAlturaAtaque = new Vector3(transform.position.x,transform.position.y + alturaAtaque,0);
    }
    private void AlturaAtaqueMosca()
    {
        if (transform.position.Equals(cordenadaAlturaAtaque))
        {
            status = MoscaStatus.kamikase;
            tempoAtaque = Time.time + TempoEsperaAtaque;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, cordenadaAlturaAtaque, velocidadesubida * Time.deltaTime);
        }
    }

    //mosca se atira em dire��o ao player
    private void AtaqueMosca()
    {
        if (tempoAtaque < Time.time)
        {
            transform.position = Vector3.MoveTowards(transform.position, miraAtaque, velocidadeAtaque * Time.deltaTime);
        }
        else
        {
            miraAtaque = player.position;
        }

        if (transform.position.Equals(miraAtaque))
        {
            status = MoscaStatus.tonto;
        }
    }
    //apos acertar o player mosca fica tonta
    //apos alguns segundos mosca volta ao normal
    //mosca avua para cima um pouco
    //mosca se atira de player de novo
}
