using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BTZombiTurtle : MonoBehaviour
{
    private Rigidbody2D rb;
    [Space]
    [Header("Circle Cast Variables")]
    public float radius;//trigger area
    public bool AwakeWhenPlayerClose = false;// detecte if a Player was inside
    Vector2 direction;
    public LayerMask PlayerLayer;

    [Header("LookAt")]
    [HideInInspector]public GameObject PlayerTransform;
    public float ChaseSpeed;
    [Space]
    public bool lookAt;
    public bool AttackPlayer;
    public bool Chase;
    private void Start()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player");
        rb= this.GetComponent<Rigidbody2D>();  
        StartCoroutine(FindTargetsWithDelay(0.01f));

        BTsequence Sequence1 = new BTsequence();
        Sequence1.children.Add(new IsPlayerCloseOrOnGround());
        Sequence1.children.Add(new ChasePlayer());

        BehaviorTree bt = GetComponent<BehaviorTree>();
        bt.root = Sequence1;

        StartCoroutine(bt.Execute());
    }



    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            AreaToAwakeEnemy();

            if(lookAt)
            {
                LookAtPlayer();
            }
        }
    }
    public void LookAtPlayer()
    {
        Vector3 look = transform.InverseTransformPoint(PlayerTransform.transform.position);
        float angle = Mathf.Atan2(0f,look.x)*Mathf.Rad2Deg;

        transform.Rotate(0f,angle,0f);
    }
    public void AttackPlayerMethod()
    {

    }
    public void AreaToAwakeEnemy()
    {
        direction = Vector2.zero;

        //creates the box cast(trigger)
        RaycastHit2D CircleInfo = Physics2D.CircleCast(gameObject.GetComponent<Renderer>().bounds.center, radius, direction);

        //checks if the player is inside the area
        if (CircleInfo.collider.CompareTag("Player") && !AwakeWhenPlayerClose)
        {
            AwakeWhenPlayerClose = true;
            lookAt = true;

        }
        if(CircleInfo.collider.gameObject.tag =="Player" && AwakeWhenPlayerClose)
        {

            AttackPlayer = true;
            AttackPlayerMethod();
            Chase = false;
        }
        if(CircleInfo.collider.gameObject.tag != "Player" && AwakeWhenPlayerClose && AttackPlayer == true)
        {
            Chase = true;
            AttackPlayer = false;
        }
    }
    public void OnDrawGizmosSelected()
    {
        //Draw the box on unity
        Gizmos.DrawWireSphere(gameObject.GetComponent<Renderer>().bounds.center, radius);

    }
}
