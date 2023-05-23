using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptPorta : MonoBehaviour
{
    [SerializeField] private PuzzleSequanciaMain puzzle;
    public Animator PortaAbrindo;

    private void Update()
    {
        if (puzzle.puzzleFeito)
        {
            PortaAbrindo.SetBool("Abriu", true);
        }
    }
    public void AlertObservers(string message)
    {
        if (message.Equals("Abriu"))
        {
            Destroy(gameObject);
        }
    }
}
