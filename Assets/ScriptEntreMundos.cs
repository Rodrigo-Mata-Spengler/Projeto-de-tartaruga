using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptEntreMundos : MonoBehaviour
{
    [SerializeField] private string proximaScena;

    [SerializeField] private string playerTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            SceneManager.LoadScene(proximaScena);
        }
    }

}
