using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTFirslBossGuardian : MonoBehaviour
{
    private Rigidbody2D rb;
    [Space]
    [Header("Circle Cast Variables")]
    public float radius;//trigger area
    public bool PlayerClose = false;// detecte if a Player was inside
    Vector2 direction;
    public LayerMask PlayerLayer;
    public bool JumpInPlayer = false;

    [Header("LookAt")]
    [HideInInspector] public GameObject PlayerTransform;
    public float ChaseSpeed;
    [Space]
    public bool lookAt;
    public bool AttackPlayer;
    public bool Chase;
    public bool jump;

    [Space]
    [Header("Hit feedback")]
    public bool wasHit = false;
    public float impulseForce;
    public float secondsToDisable;

    [Space]
    [Header("Attack")]
    public GameObject AttackTrigger;
    public float TimeToAttackwhileClose;
    public bool Attacked = false;
    public float AttackDuration;
    [Space]
    [HideInInspector]public int AttackOrJumpPorcentage;
    
    private void Start()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player");
        rb = this.GetComponent<Rigidbody2D>();
        StartCoroutine(FindTargetsWithDelay(0.01f));


        BTselector Selector2 = new BTselector();
        Selector2.children.Add(new PlayerClose());
        Selector2.children.Add(new ChasePlayerFirsBoss());

        BTsequence Sequence1 = new BTsequence();
        Sequence1.children.Add(new JumpOrChase());
        Sequence1.children.Add(Selector2);

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

            if (lookAt)
            {
                LookAtPlayer();
            }
            if (wasHit)
            {
                rb.AddForce((transform.right * -1) * impulseForce);
                StartCoroutine(DisableHitFeedback(secondsToDisable));
            }
            if(Attacked)
            {
                StartCoroutine(DisableAttackTrigger(AttackDuration));
            }
            if(PlayerClose == false && Chase == false)
            {
                AttackOrJumpPorcentage = Random.Range(0,100);
            }

        }
    }
    private IEnumerator DisableHitFeedback(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        wasHit = false;
    }
    public void LookAtPlayer()
    {
        Vector3 look = transform.InverseTransformPoint(PlayerTransform.transform.position);
        float angle = Mathf.Atan2(0f, look.x) * Mathf.Rad2Deg;

        transform.Rotate(0f, angle, 0f);
    }
    public IEnumerator DisableAttackTrigger(float AttackDuration)
    {
        yield return new WaitForSeconds(AttackDuration);
        Attacked = false;
        AttackTrigger.SetActive(false);
    }
    public void AreaToAwakeEnemy()
    {
        direction = Vector2.zero;

        //creates the box cast(trigger)
        RaycastHit2D CircleInfo = Physics2D.CircleCast(gameObject.GetComponent<Renderer>().bounds.center, radius, direction);

        //checks if the player is inside the area
        if (CircleInfo.collider.CompareTag("Player") && !PlayerClose)
        {
            PlayerClose = true;
            lookAt = true;

        }
        if (CircleInfo.collider.gameObject.tag == "Player" && PlayerClose)
        {

            AttackPlayer = true;
            Chase = false;
        }
        if (CircleInfo.collider.gameObject.tag != "Player" && PlayerClose && AttackPlayer == true)
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
