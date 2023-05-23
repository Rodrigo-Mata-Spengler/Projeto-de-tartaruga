using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptPorta : MonoBehaviour
{
    [SerializeField] private PuzzleSequanciaMain puzzle;
    [SerializeField] private GameObject Porta;
    [SerializeField] private GameObject Poca;
    public Animator PortaAbrindo;
    public void AlertObservers(string message)
    {
        if (message.Equals("terminou"))
        {
            Poca.SetActive(true);
        }
    }
    private void Update()
    {
        if (puzzle.puzzleFeito)
        {
            PortaAbrindo.SetBool("Abriu", true);
            Porta.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
