using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeniusPorta : MonoBehaviour
{
    [SerializeField] private GeniusControler puzzle;

    private void Update()
    {
        if (puzzle.jogoFinalizado)
        {
            Destroy(gameObject);
        }
    }
}
