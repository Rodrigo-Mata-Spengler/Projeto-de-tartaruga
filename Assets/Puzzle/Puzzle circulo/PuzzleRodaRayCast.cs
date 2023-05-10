using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuzzleRodaRayCast : MonoBehaviour
{
    public float Distance; // raycast distance;

    private bool rightPosition = false;
    private bool hit = false;

    public LayerMask PuzzleLayer;

    public PuzzleRodaManager PuzzleRodaManager;

    // Update is called once per frame
    void Update()
    {
        rightPosition = Physics2D.Raycast(transform.position, transform.up, Distance, PuzzleLayer);

        if(rightPosition && !hit)
        {
            PuzzleRodaManager.CirclesRight += 1;
     
            hit= true;
           
        }
        if(rightPosition == false && hit)
        {
            PuzzleRodaManager.CirclesRight -= 1;
            hit= false;
        }
    }
}
