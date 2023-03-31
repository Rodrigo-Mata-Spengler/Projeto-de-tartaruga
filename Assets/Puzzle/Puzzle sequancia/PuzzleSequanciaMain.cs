using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSequanciaMain : MonoBehaviour
{
    [SerializeField] private GameObject[] blocos;
    [SerializeReference]public bool puzzleFeito = false;

    private void Update()
    {
        if (Verificar())
        {
            puzzleFeito = true;
            FeedBack();
        }
    }

    private bool Verificar()
    {
        for (int i = 0; i < blocos.Length; i++)
        {
            if (blocos[i].GetComponent<PuzzleSequencioa>().state == false)
            {
                return false;
                break;
            }

        }
        return true;
    }

    private void FeedBack()
    {
        for (int i = 0; i < blocos.Length; i++)
        {
            blocos[i].GetComponent<SpriteRenderer>().color = Color.green;
            blocos[i].GetComponent<PuzzleSequencioa>().enabled = false;

        }
    }
}
