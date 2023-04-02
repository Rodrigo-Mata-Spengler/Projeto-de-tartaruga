using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayerFirsBoss : BTnode
{
    public override IEnumerator Run(BehaviorTree bt)
    {
        status = Status.RUNNING;
        Print();

        GameObject Player = bt.GetComponent<BTFirslBossGuardian>().PlayerTransform;
        float ChaseSpeed = bt.GetComponent<BTFirslBossGuardian>().ChaseSpeed;
        bool Chase = bt.GetComponent<BTFirslBossGuardian>().Chase;
        bool Attack = bt.GetComponent<BTFirslBossGuardian>().AttackPlayer;
        bool PlayerClose = bt.GetComponent<BTFirslBossGuardian>().PlayerClose;

        while (Chase && Attack == false)
        {

            bt.transform.position = Vector2.MoveTowards(bt.transform.position, Player.transform.position, ChaseSpeed * Time.deltaTime);
            Debug.LogWarning("aquii");
            if(PlayerClose)
            {
                status = Status.SUCCESS;
            }

            yield return null;
        }



        Print();
        yield break;
    }
}
