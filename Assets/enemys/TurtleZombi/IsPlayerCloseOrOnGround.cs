using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPlayerCloseOrOnGround : BTnode
{
    public override IEnumerator Run(BehaviorTree bt)
    {
        status = Status.RUNNING;
        Print();

        GameObject npc = bt.gameObject;
        bool PlayerDetected = bt.GetComponent<BTZombiTurtle>().PlayerClose;

        if(PlayerDetected)
        {
            status = Status.SUCCESS;
        }
        else
        {
            status = Status.FAILURE;
        }
        
        Print();
        yield break;
    }
}
