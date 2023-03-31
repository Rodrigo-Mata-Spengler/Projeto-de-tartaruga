using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptPorta : MonoBehaviour
{
    [SerializeField] private PuzzleSequanciaMain puzzle;

    private void Update()
    {
        if (puzzle.puzzleFeito)
        {
            Destroy(gameObject);
        }
    }
}
