using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleQuadrado : MonoBehaviour
{
    [Header("pe�as do puzzle")]
    [SerializeField] private GameObject[] pecasPuzzle;
    private Vector3[] pecasPosicaoOriginal;
    private int[] posiRandom;
    

    private void Start()
    {
        LocPeca();    
    }

    private void LocPeca()
    {
        for (int i = 0; i < pecasPuzzle.Length; i++)
        {
            pecasPosicaoOriginal[i] = pecasPuzzle[i].transform.position;
        }
    }

    private void RandomizePeca()
    {

    }
    //embaralhar as pe�as
    //definir qual pe�a estar� faltando
    //caso o player movimente as pe�as usando wasd o puzzle reagi de acordo
    //toda vez que o player movimenhta uma pe�a, veifica se o puzzle esta correto
}
