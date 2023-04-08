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
        bool PlayerClose = bt.GetComponent<BTZombiTurtle>().PlayerClose;


        while (!PlayerClose)
        {
            bt.GetComponent<BTZombiTurtle>().lookAt = true;
            bt.transform.position = Vector2.MoveTowards(bt.transform.position, Player.transform.position, ChaseSpeed * Time.deltaTime);

            if (bt.GetComponent<BTZombiTurtle>().PlayerClose)   
            {
                status = Status.SUCCESS;
                Debug.LogWarning("aquii");
                break;
                
            }
            
            yield return null;
        }

        Print();
        yield break;
    }
}
