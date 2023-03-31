using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MoscaStatus {patrulha, alerta, kamikase, tonto, recuperacao, desativado};
public class MoscaBehavior : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] private MoscaStatus status = MoscaStatus.desativado;

    [Header("Patrulha")]
    //define limite de movimentação da mosca
    [SerializeField] private Transform limiteMax;
    [SerializeField] private Transform limiteMin;
    private float maxX = 0;
    private float maxY = 0;
    private float minX = 0;
    private float minY = 0;
    [SerializeField] private float velocidade = 0;//velocidade de movimetnação da mosca
    [SerializeField] private float tempoEspera = 0;//tempo de espera que a mosca faz entre os caminhos
    private float tempoProximoPonto = 0;
    [SerializeField] private Vector3 pontoPatrulha;
    private Vector3 centroOriginal;

    [Header("Detecção Player")]
    [SerializeField] private float raioDetecção;
    [SerializeField] private string playerTag;
    private Transform player;

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
                break;
            case MoscaStatus.kamikase:
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
            //criar ponto aleatório para masca ir
            CriarPontoPatrulha();
        }
        else
        {
            //movimentar a mosca até o ponto
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
        GetComponent<CircleCollider2D>().radius = raioDetecção;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            player = collision.transform;
        }
    }


    //mosca se atira em direção ao player
    //apos acertar o player mosca fica tonta
    //apos alguns segundos mosca volta ao normal
    //mosca avua para cima um pouco
    //mosca se atira de player de novo
}
