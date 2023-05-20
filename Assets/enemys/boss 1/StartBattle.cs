using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBattle : MonoBehaviour
{
    private BoxCollider2D trigger;
    public GuardianBehavior Boss;
    public GameObject Wall;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Wall.SetActive(true);
            Boss.enabled = true;
            this.gameObject.SetActive(false);
        }

    }

}
