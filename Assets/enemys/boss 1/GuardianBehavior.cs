using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

enum GuardianStatus { desativado, Attack, DisableAttack, Jump };
public class GuardianBehavior : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] private GuardianStatus status = GuardianStatus.desativado;

    [Header("StartFight")]
    public bool StartFight = false;

    private Rigidbody2D rb;
    [Space]
    public bool PlayerClose = false;// detecte if a Player was inside


    [Header("LookAt")]
    [HideInInspector] public GameObject PlayerTransform;
    [Space]

    [Header("Jump")]
    public float jumpHeight;
    public bool jumped;
    public float LandJumpCoolDown;

    [Space]
    [Header("Attack")]
    public GameObject AttackTrigger;
    public bool Attacked = false;
    public float WaitToAttackTime;
    public float AttackDuration;
    public float CoolDownToNextAttack;

    [Header("Ground")]
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    public bool m_Grounded;            // Whether or not the player is grounded.

    [Header("Events")]
    public UnityEvent OnLandEvent;

   

    // Start is called before the first frame update
    void Start()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player");
        rb = this.GetComponent<Rigidbody2D>();

    }

    private void Awake()
    {
        status = GuardianStatus.desativado;
    }
    // Update is called once per frame
    void Update()
    {

        switch (status)
        {

            case GuardianStatus.Jump:
                JumpAtPlayer();
                break;

            case GuardianStatus.Attack:
                if(!Attacked)
                {
                    //StartCoroutine(AttackPlayer(WaitToAttackTime));
                }
                break;
            case GuardianStatus.DisableAttack:
                StartCoroutine(DisableAttack(AttackDuration, CoolDownToNextAttack));
                break;

            case GuardianStatus.desativado:
                break;
        }

        LookAtPlayer();


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

    public IEnumerator DisableAttack( float AttackDuration, float CoolDownToDesactivated)
    {
        AttackTrigger.SetActive(false);
        yield return new WaitForSeconds(CoolDownToDesactivated);
        status = GuardianStatus.desativado;
        Attacked = false;

    }
    public void JumpAtPlayer()
    {
        if (m_Grounded && !jumped)
        {
            float distanceFromPlayer = PlayerTransform.transform.position.x - transform.position.x;
            rb.AddForce(new Vector2(distanceFromPlayer, jumpHeight), ForceMode2D.Impulse);
            jumped = true;

        }
    }
    public IEnumerator DisableJump(float jumpCoolDown)
    {
        yield return new WaitForSeconds(jumpCoolDown);

        status = GuardianStatus.desativado;
        jumped = false;


    }

    public void LookAtPlayer()
    {
        if(status == GuardianStatus.desativado && Attacked == false || Attacked == false)
        {
            Vector3 look = transform.InverseTransformPoint(PlayerTransform.transform.position);
            float angle = Mathf.Atan2(0f, look.x) * Mathf.Rad2Deg;

            transform.Rotate(0f, angle, 0f);
        }

    }

}
