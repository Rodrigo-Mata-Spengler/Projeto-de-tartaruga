using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BTFirslBossGuardian : MonoBehaviour
{
    [Header("Ground")]
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    public bool m_Grounded;            // Whether or not the player is grounded.

    [Header("Events")]
    public UnityEvent OnLandEvent;

    private Rigidbody2D rb;
    public bool PlayerClose = false;// detecte if a Player was inside

    [Header("LookAt")]
    [HideInInspector] public GameObject PlayerTransform;
    [Space]
    public bool lookAt;

  
    [Space]
    [Header("Attack")]
    public bool AttackPlayer;
    public GameObject AttackTrigger;
    public float AttackDelay;
    public bool Attacked = false;
    public float AttackDuration;
    public bool Attacking = false;  

    [Space]
    [Header("Jump")]
    public float jumpHeight;
    public float jumpAgainTime;
    public bool jumped = false;
    public float JumpDelay;
    public bool WaitToJumpAgain = false;



    private void Start()
    {
    
        PlayerTransform = GameObject.FindGameObjectWithTag("Player");
        rb = this.GetComponent<Rigidbody2D>();

        StartCoroutine(FindTargetsWithDelay(0.01f));

        BTselector Selector1 = new BTselector();
        Selector1.children.Add(new JumpAtPlayer());
        Selector1.children.Add(new AttackPlayer());


        BehaviorTree bt = GetComponent<BehaviorTree>();
        bt.root = Selector1;

        StartCoroutine(bt.Execute());
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            
            if (lookAt)
            {
                LookAtPlayer();
            }
            if (Attacked)
            {
                StartCoroutine(Attack(AttackDelay));
            }
            else if(Attacking) 
            {
                StartCoroutine(DisableAttackTrigger(AttackDuration));
            }
            if(jumped)
            {
                StartCoroutine(Jump(JumpDelay));
            }
            else if(WaitToJumpAgain)
            {
                StartCoroutine(CanJumpAgainDelay(jumpAgainTime));
            }

        }
    }
    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }
    public void LookAtPlayer()
    {
        Vector3 look = transform.InverseTransformPoint(PlayerTransform.transform.position);
        float angle = Mathf.Atan2(0f, look.x) * Mathf.Rad2Deg;

        transform.Rotate(0f, angle, 0f);
    }
    public IEnumerator Attack(float AttackDelay)
    {
        Attacked = false;
        yield return new WaitForSeconds(AttackDelay);
        AttackTrigger.SetActive(true);
        Attacking= true;

    }
    private IEnumerator CanJumpAgainDelay(float JumpAgainTime)
    {
        yield return new WaitForSeconds(JumpAgainTime);
        jumped= false;
        WaitToJumpAgain = false;
    }
    public IEnumerator DisableAttackTrigger(float AttackDuration)
    {
        yield return new WaitForSeconds(AttackDuration);
        Attacked = false;
        Attacking = false;
        AttackTrigger.SetActive(false);

    }
    public IEnumerator Jump(float JumpDelay)
    {
        jumped = false;
        yield return new WaitForSeconds(JumpDelay);
        float distanceFromPlayer = PlayerTransform.transform.position.x - transform.position.x;
        rb.AddForce(new Vector2(distanceFromPlayer, jumpHeight), ForceMode2D.Impulse);
        WaitToJumpAgain = true;
        

    }
}
