using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceChangeTrigger : MonoBehaviour
{
    [SerializeField] private string paramaterName;
    [SerializeField] private float paramaterValue;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            AudioManager.instance.SetAmbienceParameter(paramaterName, paramaterValue);
        }
    }
}
