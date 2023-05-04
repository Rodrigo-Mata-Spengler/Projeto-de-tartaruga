using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BTZombiTurtle : MonoBehaviour
{
    [HideInInspector]public Animator m_Animator;
    [HideInInspector]public Rigidbody2D rb;
    [Space]

    public bool PlayerClose = false;
    public bool lookingRight;

    [Header("LookAt")]
    [HideInInspector]public GameObject PlayerTransform;
    public float ChaseSpeed;
    [Space]
    public bool lookAt = true;

    [Header("Attack")]
    public GameObject CloseTrigger;
    public GameObject AttackHitBoxEnemy;
    public float WaitTimeToAttack;
    public float AttackDuration;

    private void Start()
    {
        m_Animator = this.GetComponent<Animator>();
        PlayerTransform = GameObject.FindGameObjectWithTag("Player");
        rb= this.GetComponent<Rigidbody2D>();  
        StartCoroutine(FindTargetsWithDelay(0.01f));

        BTsequence Sequence1 = new BTsequence();
        Sequence1.children.Add(new ChasePlayer());
        Sequence1.children.Add(new WaitTime());
        Sequence1.children.Add(new Attack());

        BehaviorTree bt = GetComponent<BehaviorTree>();
        bt.root = Sequence1;

        StartCoroutine(bt.Execute());
    }
 
    

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);

            if(lookAt)
            {
                LookAtPlayer();
            }

            if(transform.eulerAngles.y > 100)
            {
                lookingRight= false;
            }
            else
            {
                lookingRight= true;
            }
        }
    }
    public void LookAtPlayer()
    {
        Vector3 look = transform.InverseTransformPoint(PlayerTransform.transform.position);
        float angle = Mathf.Atan2(0f,look.x)*Mathf.Rad2Deg;

        transform.Rotate(0f,angle,0f);
    }

}
