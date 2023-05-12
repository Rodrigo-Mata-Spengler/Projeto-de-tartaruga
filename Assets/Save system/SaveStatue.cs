using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveStatue : MonoBehaviour
{
    [SerializeField] private string player;
    private bool interacao;
    private GameObject playerGO;
    private SpriteRenderer rend;
    public Sprite EstatuaAcessa;
    public Animator Vaso1;
    public Animator Vaso2;

    private void Start()
    {
        interacao = false;
        rend = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetButton("Interacao")  && interacao)
        {
            SaveSystem.SavePlayer(playerGO);
            this.GetComponent<SpriteRenderer>().sprite = EstatuaAcessa;
            Vaso1.SetBool("Salvo", true);
            Vaso2.SetBool("Salvo", true);
            playerGO.GetComponent<Animator>().SetBool("Rezar", true);
            //rend.color = Color.green;
        }
    }

    //detectar se o player esta na area de interação da estatua
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(player))
        {
            interacao = true;
            playerGO = collision.gameObject;
            //rend.color = Color.red;
        }
    }

    /*private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(player))
        {
            interacao = false;
            playerGO = null;
            rend.color = Color.white;
        }
    }*/
}
