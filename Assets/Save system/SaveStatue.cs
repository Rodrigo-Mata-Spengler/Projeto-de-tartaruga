using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveStatue : MonoBehaviour
{
    [SerializeField] private string player;
    private bool interacao;
    private GameObject playerGO;
    private SpriteRenderer rend;

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
            rend.color = Color.green;
        }
    }

    //detectar se o player esta na area de intera��o da estatua
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(player))
        {
            interacao = true;
            playerGO = collision.gameObject;
            rend.color = Color.red;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(player))
        {
            interacao = false;
            playerGO = null;
            rend.color = Color.white;
        }
    }
}