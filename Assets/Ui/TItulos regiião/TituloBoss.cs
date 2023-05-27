using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TituloBoss : MonoBehaviour
{
    [SerializeField] private string nome;

    [SerializeField]private TItuloRegiao titulo;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        titulo.Show(nome);
    }
}
