using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAtPlayer : BTnode
{
    public override IEnumerator Run(BehaviorTree bt)
    {
        status = Status.RUNNING;
        Print();

        bool jumpInPlayer = bt.GetComponent<BTFirslBossGuardian>().JumpInPlayer = true;
        bool Chase = bt.GetComponent<BTFirslBossGuardian>().Chase = true;

        if (jumpInPlayer)
        {
            //do the jump
            bt.GetComponent<BTFirslBossGuardian>().JumpAtPlayer();
            status = Status.SUCCESS;

        }
        if (Chase)
        {
            status = Status.SUCCESS;
        }
        Print();
        yield break;
    }
}
