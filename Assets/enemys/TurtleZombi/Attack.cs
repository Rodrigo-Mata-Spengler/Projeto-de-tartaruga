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
        bool lookinRight = bt.GetComponent<BTZombiTurtle>().lookingRight;

        float currentTime = 0f;


        while (currentTime  < AttackDuration)
        {

            bt.GetComponent<BTZombiTurtle>().m_Animator.SetBool("Bater", true);
            currentTime += Time.deltaTime;
            bt.GetComponent<BTZombiTurtle>().AttackHitBoxEnemy.SetActive(true);

            if (lookinRight)
            {
                bt.GetComponent<Rigidbody2D>().velocity = new Vector2(2f, bt.gameObject.GetComponent<Rigidbody2D>().velocity.y);
            }
            else
            {
                bt.GetComponent<Rigidbody2D>().velocity = new Vector2(-2f, bt.gameObject.GetComponent<Rigidbody2D>().velocity.y);
            }

            bt.GetComponent<BTZombiTurtle>().lookAt = false;
            if (currentTime > AttackDuration)
            {
                bt.GetComponent<BTZombiTurtle>().m_Animator.SetBool("Bater", false);
                bt.GetComponent<BTZombiTurtle>().CloseTrigger.SetActive(true);
                bt.GetComponent<BTZombiTurtle>().AttackHitBoxEnemy.SetActive(false);
                bt.GetComponent<BTZombiTurtle>().LookAtPlayer();
                status = Status.SUCCESS;
                 break;
            }


            yield return null;
        }


        Print();
        yield break;
    }
}
