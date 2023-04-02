using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : BTnode
{
    public override IEnumerator Run(BehaviorTree bt)
    {
        status = Status.RUNNING;
        Print();

        GameObject AttackTrigger = bt.GetComponent<BTFirslBossGuardian>().AttackTrigger;
        bool PlayerClose = bt.GetComponent<BTFirslBossGuardian>().PlayerClose;
        float TimeToAttack = bt.GetComponent<BTFirslBossGuardian>().TimeToAttackwhileClose;
        bool Attacked = bt.GetComponent<BTFirslBossGuardian>().Attacked;


        while (PlayerClose && Attacked == false)
        {
            WaitToAttack(TimeToAttack);
            AttackTrigger.SetActive(true);
            status = Status.SUCCESS;

        }


        Print();
        yield break;
    }
    IEnumerator WaitToAttack(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
