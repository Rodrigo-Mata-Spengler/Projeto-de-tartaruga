using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

enum GuardianStatus { desativado, Attack, DisableAttack, Jump, CheckPlayerDistance };
public class GuardianBehavior : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] private GuardianStatus status = GuardianStatus.desativado;

    [Header("StartFight")]
    public bool StartFight = false;

    private Rigidbody2D rb;
    [Space]
    public bool PlayerClose = false;// detecte if a Player was inside
    public bool lookingRight = false;

    [Header("LookAt")]
    [HideInInspector] public GameObject PlayerTransform;
    [Space]

    [Header("Jump")]
    public float jumpHeight;
    public bool jumped;
    public float TempoPular = 0;
    [SerializeField] private float tempoParaPular;

    [Space]
    [Header("Attack")]
    public GameObject AttackTrigger;
    public bool Attacked = false;
    public float AttackImpulse;

    [SerializeField] private float DuracaoDeAtaque= 0;
    public float tempoDeAtaque = 0f;

    [Header("Ground")]
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    public bool m_Grounded;            // Whether or not the player is grounded.


    [HideInInspector]public UnityEvent OnLandEvent;

    public float tempoTonto = 0;
    [SerializeField] private float TempoEsperaTonto = 0;

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
            case GuardianStatus.desativado:
                Tonto();
                break;

            case GuardianStatus.CheckPlayerDistance:
                PlayerDistance();
                break;

            case GuardianStatus.Jump:
                JumpAtPlayer();
                break;

            case GuardianStatus.Attack:
                AttackPlayer();
                break;

        }

        LookAtPlayer();


    }
    private void PlayerDistance()
    {
        if(PlayerClose)
        {
            status = GuardianStatus.Attack;
            tempoDeAtaque = 0;

            if(lookingRight)
            {
                rb.AddForce(new Vector2(AttackImpulse, transform.position.y), ForceMode2D.Impulse);
            }
            if(!lookingRight)
            {
                rb.AddForce(new Vector2(-AttackImpulse, transform.position.y), ForceMode2D.Impulse);
            }


        }
        else
        {
            status = GuardianStatus.Jump;
            TempoPular = 0;
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
    public void AttackPlayer()
    {
        tempoDeAtaque += Time.deltaTime;

        if(tempoDeAtaque < DuracaoDeAtaque)
        {
            AttackTrigger.SetActive(true);
            Attacked = true;
   
        }
        else
        {
            Attacked= false;
            AttackTrigger.SetActive(false);
            tempoTonto = 0;
            status = GuardianStatus.desativado;
        }


    }
    private void Tonto()
    {
        tempoTonto += Time.deltaTime;
        if (tempoTonto< TempoEsperaTonto)
        {
            Debug.Log("Aquii");
            
        }
        else
        {
            status = GuardianStatus.CheckPlayerDistance;
            Debug.Log("eu");
        }
    }
    public void JumpAtPlayer()
    {
        TempoPular += Time.deltaTime;
        if (TempoPular < tempoParaPular)
        {


        }
        else if(jumped ==false)
        {
            float distanceFromPlayer = PlayerTransform.transform.position.x - transform.position.x;
            if(m_Grounded)
            {
                rb.AddForce(new Vector2(distanceFromPlayer, jumpHeight), ForceMode2D.Impulse);
                jumped = true;
            }


        }

        if(m_Grounded && jumped)
        {
            TempoPular = 0;
            status = GuardianStatus.desativado;
            tempoTonto = 0;
            jumped= false;
        }
    }

    public void LookAtPlayer()
    {
        if(status == GuardianStatus.desativado && Attacked == false || Attacked == false)
        {
            Vector3 look = transform.InverseTransformPoint(PlayerTransform.transform.position);
            float angle = Mathf.Atan2(0f, look.x) * Mathf.Rad2Deg;

            transform.Rotate(0f, angle, 0f);

            lookingRight = false;
        }
        else
        {
            lookingRight = true;
        }

    }

}
