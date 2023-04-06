using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JumpAtPlayer : BTnode
{
    public override IEnumerator Run(BehaviorTree bt)
    {
        status = Status.RUNNING;
        Print();

        bool PlayerClose = bt.GetComponent<BTFirslBossGuardian>().PlayerClose;
        bool Attacked = bt.GetComponent<BTFirslBossGuardian>().Attacked;
        bool grounded = bt.GetComponent<BTFirslBossGuardian>().m_Grounded;

        if (!PlayerClose && !Attacked && grounded )
        {
            //do the jump
            bt.GetComponent<BTFirslBossGuardian>().jumped = true;
            status = Status.SUCCESS;
            yield break;

        }
        else
        {
            status = Status.FAILURE;
            yield break;
        }

        Print();
    }
}
