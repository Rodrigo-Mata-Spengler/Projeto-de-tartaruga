using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaoSombraDestroi : MonoBehaviour
{
    [SerializeField] private string playerTag;
    [SerializeField] private float tempoAntesDestroicao;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            Destroy(gameObject,tempoAntesDestroicao);
        }
    }
}
