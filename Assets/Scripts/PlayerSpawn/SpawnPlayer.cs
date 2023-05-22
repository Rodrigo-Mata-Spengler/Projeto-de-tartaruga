using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");

        foreach (var item in player)
        {
            item.transform.position = transform.position;
        }
    }
}
