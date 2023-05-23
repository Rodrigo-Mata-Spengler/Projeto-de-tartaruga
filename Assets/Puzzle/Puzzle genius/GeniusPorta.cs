using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeniusPorta : MonoBehaviour
{
    [SerializeField] private GeniusControler puzzle;
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
        if (puzzle.jogoFinalizado)
        {
            PortaAbrindo.SetBool("Abriu", true);
            Porta.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
