using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Habilidade { Nada, WallJump, DoubleJump, Blast, Dash}
public class Selo : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] private Habilidade status = Habilidade.Nada;

    [SerializeField] private HabilidadesPlayer habiPlayer;

    private GameObject PLayer;

    public bool Detected = false;

  
    [Space]
    [SerializeField]private GameObject Ativado;
    [SerializeField] private GameObject quebrado;

    public bool tridente = false;

    public Animator Poca_E;
    public Animator Poca_D;

    private void Start()
    {
        PLayer = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if(Input.GetButtonDown("Interacao") && Detected)
        {
            switch (status)
            {
                case Habilidade.WallJump:
                    PLayer.GetComponent<PlayerMovement>().haveWallJump = true;
                    PLayer.GetComponent<Estamina>().enabled = true;
                    break;

                case Habilidade.Dash:
                    PLayer.GetComponent<Dash>().enabled = true;
                    habiPlayer.ShowHabilidade(0);
                    break;

                case Habilidade.DoubleJump:
                    PLayer.GetComponent<PlayerMovement>().haveDoubleJump = true;
                    PLayer.GetComponent<Estamina>().enabled = true;
                    habiPlayer.ShowHabilidade(2);
                    break;

                case Habilidade.Blast:
                    PLayer.GetComponent<Blast>().enabled = true;
                    PLayer.GetComponent<Estamina>().enabled = true;
                    habiPlayer.ShowHabilidade(1);
                    break;
            }

            Ativado.SetActive(false);
            quebrado.SetActive(true);
            PLayer.GetComponent<Health>().RezarEffect.Play();
            PLayer.GetComponent<Animator>().SetBool("Rezar", true);
        }

        SaveSystem.SavePlayer(PLayer);
        Iconesalvando.Mostraricone();

        if (quebrado == true)
        {
            Poca_E.SetBool("Caiu", true);
            Poca_D.SetBool("Caiu", true);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                PLayer = collision.gameObject;
                Detected = true;
            }

        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {

                PLayer = null;
                Detected = false;
                collision.GetComponent<Health>().RezarEffect.Stop();
                collision.GetComponent<Animator>().SetBool("Rezar", false);
            }

        }
    
}

