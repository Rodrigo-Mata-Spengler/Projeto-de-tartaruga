using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Attack : BTnode
{

    public override IEnumerator Run(BehaviorTree bt)
    {
        status = Status.RUNNING;
        Print();

        GameObject AttackHitBoxEnemy = bt.GetComponent<BTZombiTurtle>().AttackHitBoxEnemy;
        float AttackDuration = bt.GetComponent<BTZombiTurtle>().AttackDuration;

        float currentTime = 0f;


        while (currentTime  < AttackDuration)
        {
            currentTime += Time.deltaTime;
            bt.GetComponent<BTZombiTurtle>().AttackHitBoxEnemy.SetActive(true);

            Debug.LogWarning(currentTime);

            bt.GetComponent<BTZombiTurtle>().lookAt = false;
            if (currentTime > AttackDuration)
            {
                bt.GetComponent<BTZombiTurtle>().CloseTrigger.SetActive(true);
                bt.GetComponent<BTZombiTurtle>().AttackHitBoxEnemy.SetActive(false);
                status = Status.SUCCESS;
                 break;
            }


            yield return null;
        }


        Print();
        yield break;
    }
}
