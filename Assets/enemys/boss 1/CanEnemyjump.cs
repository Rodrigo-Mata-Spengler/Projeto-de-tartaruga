using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanEnemyJump : BTnode
{
    public override IEnumerator Run(BehaviorTree bt)
    {
        bool PlayerClose = bt.GetComponent<BTFirslBossGuardian>().PlayerClose;
        bool Attacked = bt.GetComponent<BTFirslBossGuardian>().Attacked;
        bool grounded = bt.GetComponent<BTFirslBossGuardian>().m_Grounded;

        if(!PlayerClose && Attacked && grounded)
        {
            status = Status.FAILURE;
            yield break;
        }
        else
        {
            status = Status.SUCCESS;
            yield break;
        }


        Print();
        yield break;
    }
}
