using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Puzzle {dormente,ameacado,atacar,dormindo};
public class Ouriço : MonoBehaviour
{
    [Header("Perimetro")]
    [SerializeField] private CircleCollider2D perimetro;
    [SerializeField] private float raioPerimetro = 8f;
    [SerializeField] private float raioExplosao = 1f;

    [Header("Status Player")]
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private GameObject player;
    [SerializeField] private float distancia = 0;

    [Header("ouriço estado temp")]
    [SerializeField] private SpriteRenderer ouricoRenderer;
    [SerializeField] private float tempBlink = 0f;
    private float tempNextBlink;
    private bool estado = true;

    [Header("projeteis")]
    [SerializeField] private GameObject[] projetil;
    [SerializeField] private float forca = 1f;

    [Header("Status Ouriço")]
    [SerializeField] private bool status = true;
    [SerializeField] private Puzzle modo;

    //set up do ouriço
    private void Start()
    {
        ouricoRenderer.color = Color.blue;
        perimetro.radius = raioPerimetro;
        modo = Puzzle.dormente;
    }
    private void Update()
    {
        if (player && status)
        {
            DistancePlayer();
            if (distancia >= raioPerimetro)
            {
                ouricoRenderer.color = Color.blue;
                modo = Puzzle.dormente;
            }
            else if (distancia < raioExplosao)
            {
                Explosao();
                modo = Puzzle.atacar;
            }
            else if (distancia < raioPerimetro && distancia > raioExplosao)
            {
                OuricoBlink();
                modo = Puzzle.ameacado;
            }
        }
    }
    //localizar o player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            player = collision.gameObject;
        }
    }

    //ver se o player esta perto
    private void DistancePlayer()
    {
        distancia = Vector2.Distance(player.transform.position, this.transform.position);
        
    }

    //mostrar mudança de estado quando o player começar a chegar muito perto
    private void OuricoBlink()
    {
        if(tempNextBlink <= Time.time)
        {
            tempNextBlink = Time.time + tempBlink;
            if (estado)
            {
                ouricoRenderer.color = Color.red;
                estado = false;
            }
            else
            {
                ouricoRenderer.color = Color.blue;
                estado = true;
            }
        }
    }

    //caso esteja muito perto estourar
    private void Explosao()
    {
        LancarProjeteis();
        OuricoTurnOff();
    }

    //quando estourar, lancar projéteis
    private void LancarProjeteis()
    {
        foreach (var proj in projetil)
        {
            proj.GetComponent<Rigidbody2D>().AddForce(proj.transform.up * forca);
            AudioManager.instance.PlayOneShot(FMODEvents.instance.DisparoOurico, transform.position);
        }

    }

    //desativa ouriço
    private void OuricoTurnOff()
    {
        status = false;
        modo = Puzzle.dormindo;
    }
}
