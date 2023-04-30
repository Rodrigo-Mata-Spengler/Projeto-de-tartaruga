using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveStatue : MonoBehaviour
{
    [SerializeField] private string player;
    private bool interacao;
    private GameObject playerGO;

    private void Start()
    {
        interacao = false;
    }

    private void Update()
    {
        if (Input.GetButton("Interacao")  && interacao)
        {
            SaveSystem.SavePlayer(playerGO);
        }
    }

    //detectar se o player esta na area de interação da estatua
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(player))
        {
            interacao = true;
            playerGO = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(player))
        {
            interacao = false;
            playerGO = null;
        }
    }
}
