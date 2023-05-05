using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleRodaManager : MonoBehaviour
{
    public int CirclesRight = 0;
    public bool locked = true;

    private bool DoOnce = false;

    public Transform DropItemTransform;
    //[SerializeField] private GameObject DoorToOpen;
    [SerializeField] private GameObject ItemToDrop;
    private void Update()
    {
        if(CirclesRight == 4 && locked)
        {
            locked= false;
        }

        if(!locked && DoOnce == false)
        {
            //DoorToOpen.SetActive(false);
            Instantiate(ItemToDrop, DropItemTransform.position, DropItemTransform.rotation);
            DoOnce = true;
        }
    }

}
