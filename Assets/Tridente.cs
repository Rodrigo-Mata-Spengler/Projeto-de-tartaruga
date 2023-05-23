using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tridente : MonoBehaviour
{
    public bool Detected = false;
    private GameObject Canvas;

    private GameObject PlayerObj;
    private void Start()
    {
        Canvas = GameObject.FindGameObjectWithTag("Canvas");
        
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interacao") && Detected)
        {
            Canvas.GetComponent<MenuPause>().Mana.SetActive(true);
            PlayerObj.GetComponent<PlayerMovement>().HaveMagicTrident = true;
            PlayerObj.GetComponent<Animator>().SetBool("Magico", true);

            Destroy(this.gameObject);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Detected = true;
            PlayerObj = collision.gameObject;
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
