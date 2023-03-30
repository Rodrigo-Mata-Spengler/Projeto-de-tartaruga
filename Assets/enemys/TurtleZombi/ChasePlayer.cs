using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ChasePlayer : BTnode
{
    public override IEnumerator Run(BehaviorTree bt)
    {
        status = Status.RUNNING;
        Print();

        GameObject Player = bt.GetComponent<BTZombiTurtle>().PlayerTransform;
        float ChaseSpeed = bt.GetComponent<BTZombiTurtle>().ChaseSpeed;
        bool Chase = bt.GetComponent<BTZombiTurtle>().Chase;
        bool Attack = bt.GetComponent<BTZombiTurtle>().AttackPlayer;

        if (Attack)
        {
            status = Status.SUCCESS;
            
        }
        while (Chase && Attack == false)
        {

            bt.transform.position = Vector2.MoveTowards(bt.transform.position, Player.transform.position, ChaseSpeed * Time.deltaTime);
            Debug.LogWarning("aquii");

            yield return null;
        }
    


        Print();
        yield break;
    }
}
