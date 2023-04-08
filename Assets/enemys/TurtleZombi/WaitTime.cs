using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class WaitTime : BTnode
{
    public override IEnumerator Run(BehaviorTree bt)
    {
        status = Status.RUNNING;
        Print();
        float WaitTimeToAttack = bt.GetComponent<BTZombiTurtle>().WaitTimeToAttack;

        float currentTime = 0f;


        while (currentTime < WaitTimeToAttack)
        {
            currentTime += Time.deltaTime;

            Debug.LogWarning(currentTime);
            bt.GetComponent<BTZombiTurtle>().lookAt = false;

            if (currentTime > WaitTimeToAttack)
            {
                if (bt.GetComponent<BTZombiTurtle>().lookingRight == false) 
                {
                    bt.GetComponent<BTZombiTurtle>().rb.velocity = new Vector2(-5f, 0f);
                }
                else
                {
                    bt.GetComponent<BTZombiTurtle>().rb.velocity = new Vector2(5f, 0f);
                }
                bt.GetComponent<BTZombiTurtle>().CloseTrigger.SetActive(false);
                status = Status.SUCCESS;
                break;
            }

            yield return null;
        }



        Print();
        yield break;
    }
}
