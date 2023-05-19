using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCloseTrigger : MonoBehaviour
{
    public GuardianBehavior BossBehaviourScript;

    [SerializeField] GameObject Wall;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            BossBehaviourScript.PlayerClose = true;
            Wall.SetActive(true); 
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
