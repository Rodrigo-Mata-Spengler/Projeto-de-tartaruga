using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCloseTrigger : MonoBehaviour
{
    public GuardianBehavior BossBehaviourScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            BossBehaviourScript.PlayerClose = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BossBehaviourScript.PlayerClose = false;
        }
    }

}
