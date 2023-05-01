using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleRodaManager : MonoBehaviour
{
    public int CirclesRight = 0;
    public bool locked = true;

    [SerializeField] private GameObject DoorToOpen;
    private void Update()
    {
        if(CirclesRight == 4 && locked)
        {
            locked= false;
        }

        if(!locked)
        {
            DoorToOpen.SetActive(false);
        }
    }

}
