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
                    break;

                case Habilidade.Dash:
                    PLayer.GetComponent<Dash>().enabled = true;
                    break;

                case Habilidade.DoubleJump:
                    PLayer.GetComponent<PlayerMovement>().haveDoubleJump = true;
                    break;

                case Habilidade.Blast:
                    PLayer.GetComponent<Blast>().enabled = true;
                    break;
            }
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
