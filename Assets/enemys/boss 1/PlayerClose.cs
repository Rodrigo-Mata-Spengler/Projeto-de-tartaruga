using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClose : BTnode
{
    public override IEnumerator Run(BehaviorTree bt)
    {
        status = Status.RUNNING;
        Print();

        bool PlayerClose = bt.GetComponent<BTFirslBossGuardian>().PlayerClose;


        if (PlayerClose)
        {
            status = Status.SUCCESS;

        }
        if(!PlayerClose)
        {
            status = Status.FAILURE;
        }



        Print();
        yield break;
    }
}
