using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpOrChase : BTnode
{
    public override IEnumerator Run(BehaviorTree bt)
    {
        status = Status.RUNNING;
        Print();

        GameObject AttackTrigger = bt.GetComponent<BTFirslBossGuardian>().AttackTrigger;
        bool PlayerClose = bt.GetComponent<BTFirslBossGuardian>().PlayerClose;
        float TimeToAttack = bt.GetComponent<BTFirslBossGuardian>().TimeToAttackwhileClose;
        bool Attacked = bt.GetComponent<BTFirslBossGuardian>().Attacked;

        int Percentage = bt.GetComponent<BTFirslBossGuardian>().AttackOrJumpPorcentage;

        if (Percentage> 0 && Percentage< 30 && !PlayerClose)
        {
            //do the jump
            bt.GetComponent<BTFirslBossGuardian>().JumpInPlayer = true;
            status = Status.SUCCESS;

        }
        if(Percentage > 31 && Percentage<100 &&!PlayerClose)
        {
            bt.GetComponent<BTFirslBossGuardian>().Chase = true;
            status = Status.SUCCESS;
        }
        else
        {
            status = Status.SUCCESS;
        }


        Print();
        yield break;
    }
}
