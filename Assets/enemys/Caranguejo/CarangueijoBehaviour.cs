using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum CarangueijoStatus {dormindo, caindo, espera, pulando};
public class CarangueijoBehaviour : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] private CarangueijoStatus status = CarangueijoStatus.dormindo;
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private GameObject player;
    private Rigidbody2D rb;

    [Header("espera queda player")]
    [SerializeField] private GameObject fonte;

    [Header("Queda")]
    [SerializeField] private Vector3 chao;
    [SerializeField] private float velocidadeQueda;

    [Header("Espera")]
    [SerializeField] private float tempoDeEspera = 0;
    private float tempoFimEspera = 0;

    [Header("Pular")]
    [SerializeField] private Vector3 mira;
    [SerializeField] private float forca = 0;
    [SerializeField] private float jumpHeight = 0;
    public bool jump = false;
    [SerializeField] private float tempoPulo = 0;
    private float proximoPulo = 0;

    [Header("Dano")]
    [SerializeField] private GameObject dano;

    [Header("Anima��o")]
    [SerializeField] private Animator anim;
    [SerializeField] private float chaoOffSet = 0;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        dano.SetActive(false);
    }

    private void Update()
    {
        switch (status)
        {
            case CarangueijoStatus.dormindo:
                RayHitDormindo();
                break;
            case CarangueijoStatus.caindo:
                Cair();
                RayHitAnim();
                break;
            case CarangueijoStatus.espera:
                Espera();
                RayHitAnim();
                break;
            case CarangueijoStatus.pulando:
                Pulo();
                RayHitAnim();
                break;
        }

        if (GetComponent<EnemyHealth>().currentHealth <= 0)
        {
            anim.SetTrigger("Morte");
        }
    }

    //Controla a anima��o
    private void RayHitAnim()
    {
        RaycastHit2D hit = Physics2D.Raycast(fonte.transform.position, Vector2.down);

        if (hit.collider != null)
        {
            Debug.DrawRay(fonte.transform.position, Vector2.down * hit.distance);
           
            if (hit.distance >= chaoOffSet)
            {
                anim.SetBool("OnAir", true);
            }
            else
            {
                anim.SetBool("OnAir", false);
            }
        }
    }

    //espera o player passar por baixo 
    private void RayHitDormindo()
    {
        RaycastHit2D hit = Physics2D.Raycast(fonte.transform.position, Vector2.down);

        if (hit.collider != null)
        {
            Debug.DrawRay(fonte.transform.position, Vector2.down * hit.distance);
            if (hit.collider.gameObject.CompareTag(playerTag))
            {
                player = hit.collider.gameObject;
                chao = hit.collider.transform.position;

                status = CarangueijoStatus.caindo;
            }
        }
    }

    //caso o plyer passe por baixo ele se atira diretamente para baixo
    private void Cair()
    {
        if (transform.position != chao)
        {
            transform.position = Vector3.MoveTowards(transform.position, chao, velocidadeQueda * Time.deltaTime);
        }
        else
        {
            tempoFimEspera = Time.time + tempoDeEspera;
            status = CarangueijoStatus.espera;
            rb.gravityScale = 1;
        }
    }

    //ao cair no ch�o ele acorda
    private void Espera()
    {
        dano.SetActive(false);
        if(tempoFimEspera <= Time.time)
        {
            mira = player.GetComponentInParent<Transform>().position;
            status = CarangueijoStatus.pulando;
        }
    }

    //ele se atira em dire�ao ao player
    private void Pulo()
    {
        if(transform.position != mira && jump == false)
        {
            //transform.position = Vector3.Slerp(transform.position, mira, velocidadePulo * Time.deltaTime);

            float distanceFromPlayer = mira.x - transform.position.x;
            rb.AddForce(new Vector2(DirecaoAtaque(), jumpHeight), ForceMode2D.Impulse);
            jump = true;
            proximoPulo = tempoPulo + Time.time;
            dano.SetActive(true);
        }
        else if(jump && proximoPulo <= Time.time)
        {
            tempoFimEspera = Time.time + tempoDeEspera;
            status = CarangueijoStatus.espera;
            jump = false;
        }
    }

    private float DirecaoAtaque()
    {
        float dist = mira.x - transform.position.x;

        if (dist >= 0)
        {
            return forca;
        }
        else
        {
            return forca * -1;
        }
    }
}

