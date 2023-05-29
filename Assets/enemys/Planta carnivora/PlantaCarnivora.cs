using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum StatusPlanta { dormindo, atacando, esperando};
public class PlantaCarnivora : MonoBehaviour
{
    [Header("Animação")]
    [SerializeField] private Animator anim;

    [Header("Status")]
    [SerializeField] private StatusPlanta status;

    [Header("player detection")]
    [SerializeField] private string playerTag;
    [SerializeField] private float detectionRadius = 0;
    private CircleCollider2D collider;
    private GameObject player;

    [Header("Planta Flip")]
    [SerializeField] private float anguloVirada = 180;

    [Header("ataque")]
    [SerializeField] private GameObject colliderAtaque;
    [SerializeField] private float ataqueDistancia = 0;
    [SerializeField] private float tempAtaque = 0;
    private float proximoAtaque = 0;
    private bool atacando = false;
    [SerializeField] private float tempDuracaoAtaque = 0;

    private void Start()
    {
        collider = GetComponent<CircleCollider2D>();
        collider.radius = detectionRadius;
        status = StatusPlanta.dormindo;
        colliderAtaque.SetActive(false);

    }

    private void Update()
    {
        switch (status)
        {
            case StatusPlanta.dormindo:
                PlayerDetected();
                break;
            case StatusPlanta.atacando:
                Ataque();
                PlayerRunAway();
                break;
            case StatusPlanta.esperando:
                PlantaPreparar();
                PlantaAtaque();
                break;
        }

        if (GetComponent<EnemyHealth>().currentHealth <= 0)
        {
            anim.SetTrigger("morte");
        }
    }

    //procura pelo player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            player = collision.gameObject;
        }
    }
    private void PlayerDetected()
    {
        if (player)
        {
            status = StatusPlanta.esperando;
        }
    }

    private void PlayerRunAway()
    {
        if (Vector3.Distance(transform.position, player.transform.position) >= detectionRadius)
        {
            status = StatusPlanta.dormindo;
        }
    }

    //se achar o player, vira na direção a qual ele esta
    private void PlantaPreparar()
    {
        Vector3 look = transform.InverseTransformPoint(player.transform.position);
        float angle = Mathf.Atan2(0f, look.x) * Mathf.Rad2Deg;

        transform.Rotate(0f, angle, 0f);
    }

    //se o player chegar muito proximo a planta ataca
    private void PlantaAtaque()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= ataqueDistancia)
        {
            status = StatusPlanta.atacando;
        }
    }
    //planta espera para proximo ataque
    private void Ataque()
    {
        if (proximoAtaque < Time.time && atacando == false)
        {
            proximoAtaque = Time.time + tempDuracaoAtaque;
            colliderAtaque.SetActive(true);
            //AudioManager.instance.PlayOneShot(FMODEvents.instance.MordidaPlanta, transform.position);
            atacando = true;
            anim.SetTrigger("Ataque");
        }
        else if(proximoAtaque < Time.time && atacando ==true)
        {
            proximoAtaque = Time.time + tempAtaque;
            colliderAtaque.SetActive(false);
            atacando = false;
        }
    }
}
