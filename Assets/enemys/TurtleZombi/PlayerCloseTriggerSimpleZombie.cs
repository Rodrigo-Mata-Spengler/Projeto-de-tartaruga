using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCloseTriggerSimpleZombie : MonoBehaviour
{
    public BTZombiTurtle BTzombieTurtleBehaviour;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BTzombieTurtleBehaviour.enabled = true;
            BTzombieTurtleBehaviour.PlayerClose = true;
            BTzombieTurtleBehaviour.m_Animator.SetTrigger("Acordar");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BTzombieTurtleBehaviour.PlayerClose = false;
        }
    }
}
