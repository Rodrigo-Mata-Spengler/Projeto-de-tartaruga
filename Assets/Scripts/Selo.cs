using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Habilidade { Nada, WallJump, DoubleJump, Blast, Dash}
public class Selo : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] private Habilidade status = Habilidade.Nada;

    private GameObject PLayer;

    public bool Detected = false;

    [SerializeField]private GameObject Ativado;
    [SerializeField] private GameObject quebrado;
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
                    
                    break;

                case Habilidade.DoubleJump:
                    PLayer.GetComponent<PlayerMovement>().haveDoubleJump = true;
                    PLayer.GetComponent<Estamina>().enabled = true;
                    break;

                case Habilidade.Blast:
                    PLayer.GetComponent<Blast>().enabled = true;
                    PLayer.GetComponent<Estamina>().enabled = true;
                    break;
            }
            Ativado.SetActive(false);
            quebrado.SetActive(true);
        }

    }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Detected = true;
            }

        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Detected = false;
            }

        }
    
}

