using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : BTnode
{
    public override IEnumerator Run(BehaviorTree bt)
    {
        status = Status.RUNNING;
        Print();

        bool PlayerClose = bt.GetComponent<BTFirslBossGuardian>().PlayerClose;
        bool Attacked = bt.GetComponent<BTFirslBossGuardian>().Attacked;
        bool grounded = bt.GetComponent<BTFirslBossGuardian>().m_Grounded;
        float AttackDelay = bt.GetComponent<BTFirslBossGuardian>().AttackDelay;
        bool jumped = bt.GetComponent<BTFirslBossGuardian>().jumped;


        if (PlayerClose && Attacked == false && grounded && !jumped)
        {
            bt.GetComponent<BTFirslBossGuardian>().Attacked = true;
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
