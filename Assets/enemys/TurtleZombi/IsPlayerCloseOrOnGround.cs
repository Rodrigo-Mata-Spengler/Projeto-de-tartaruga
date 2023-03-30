using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IsPlayerCloseOrOnGround : BTnode
{
    public override IEnumerator Run(BehaviorTree bt)
    {
        status = Status.RUNNING;
        Print();

        GameObject npc = bt.gameObject;
        bool Awake = bt.GetComponent<BTZombiTurtle>().AwakeWhenPlayerClose;
        bool chase = bt.GetComponent<BTZombiTurtle>().Chase;
        bool Attack = bt.GetComponent<BTZombiTurtle>().AttackPlayer;

        if(Awake)
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
