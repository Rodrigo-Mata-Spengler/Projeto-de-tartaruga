using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeniusPorta : MonoBehaviour
{
    [SerializeField] private GeniusControler puzzle;
    public Animator PortaAbrindo;

    public void AlertObservers(string message)
    {
        if (message.Equals("Abriu"))
        {
            Destroy(this.gameObject);
        }
    }
    private void Update()
    {
        if (puzzle.jogoFinalizado)
        {
            PortaAbrindo.SetBool("Abriu", true);
        }
    }
}
