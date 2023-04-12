using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

enum GuardianStatus { desativado, Attack, DisableAttack, JumpAtPlayer, JumpAtEdge ,Dash, CheckPlayerDistance };
public class GuardianBehavior : MonoBehaviour
{
    private EnemyHealth EnemyHealth;

    [Header("Status")]
    [SerializeField] private GuardianStatus status = GuardianStatus.desativado;

    [Header("StartFight")]
    public bool StartFight = false;

    private Rigidbody2D rb;
    [Space]
    public bool PlayerClose = false;// detecte if a Player was inside
    public bool lookingRight = false;

    [SerializeField]private bool ActionChosed = false;

    [Header("LookAt")]
    [HideInInspector] public GameObject PlayerObj;
    private bool Look = true;
    [Space]

    [Header("Jump")]
    public float jumpHeight;
    public bool jumped;
    public float TempoPular = 0;
    [SerializeField] private float tempoParaPular;

    [Space]
    [Header("Attack")]
    public GameObject AttackTrigger;
    [SerializeField] private BoxCollider2D AttackCollider;
    public bool Attacked = false;
    public float AttackImpulse;


    [SerializeField] private float DuracaoDeAtaque= 0;
    public float tempoDeAtaque = 0f;

    [Header("Ground")]
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    public bool m_Grounded;            // Whether or not the player is grounded.


    [Header("Dash")]
    private int EdgeIndex;
    [SerializeField] private GameObject[] EdgeRooms;
    [SerializeField] private bool dash = false;
    private bool dashed = false;
    public float CurrentTimeDash = 0;
    [SerializeField] private float TimeToDoDash;
    public float DashImpulse;

    [HideInInspector]public UnityEvent OnLandEvent;

    [Header("Tonto")]
    public float tempoTonto = 0;
    [SerializeField] private float TempoEsperaTonto = 0;

    // Start is called before the first frame update
    void Start()
    {
        PlayerObj = GameObject.FindGameObjectWithTag("Player");
        rb = this.GetComponent<Rigidbody2D>();
        EnemyHealth = this.GetComponent<EnemyHealth>();

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

            case GuardianStatus.JumpAtPlayer:
                JumpAtPlayer();
                break;

            case GuardianStatus.JumpAtEdge:
                JumpAtEdge();
                break;

            case GuardianStatus.Dash:
                Dash();
                break;

             case GuardianStatus.Attack:
                 AttackPlayer();
                 break;

        }


         LookAtPlayer();
        
        


    }
    private void PlayerDistance()
    {
        if(EnemyHealth.currentHealth > EnemyHealth.MaxHealth/2)
        {
            if (PlayerClose)
            {
                status = GuardianStatus.Attack;
                tempoDeAtaque = 0;
                AttackCollider.enabled = true;
            }
            else
            {
                status = GuardianStatus.JumpAtPlayer;
                TempoPular = 0;
            }
        }
        else
        {
            
            if (!PlayerClose)
            {
                
                int i = Random.Range(0, 101);
                Debug.Log(i);

                if (i >= 0 && i < 50)
                {
                    status = GuardianStatus.JumpAtPlayer;
                    TempoPular = 0;
                    ActionChosed = true;
                }
                else
                {
                    EdgeIndex = Random.Range(0, 2);
                    Debug.Log(EdgeIndex);
                    status = GuardianStatus.JumpAtEdge;
                    TempoPular = 0;
                    ActionChosed = true;
                }
            }
            if(PlayerClose)
            {
                status = GuardianStatus.Attack;
                tempoDeAtaque = 0;
                AttackCollider.enabled = true;

                ActionChosed = false;
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
    public void AttackPlayer()
    {
        tempoDeAtaque += Time.deltaTime;

        if(tempoDeAtaque < DuracaoDeAtaque)
        {
            if (lookingRight)
            {
                //rb.AddForce(new Vector2(AttackImpulse, transform.position.y), ForceMode2D.Impulse);
                rb.velocity = new Vector2(AttackImpulse, 0f);
            }
            if (!lookingRight)
            {
                //rb.AddForce(new Vector2(-AttackImpulse, transform.position.y), ForceMode2D.Impulse);
                rb.velocity = new Vector2(-AttackImpulse, 0f);
            }
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
        if (tempoTonto < TempoEsperaTonto)
        {
                
        }
        else if (dash && tempoTonto > TempoEsperaTonto)
        {
            Dash();
        }
        else
        {
            status = GuardianStatus.CheckPlayerDistance;
            
        }
    }
    public void JumpAtPlayer()
    {
        Look = false;
        TempoPular += Time.deltaTime;
        if (TempoPular < tempoParaPular)
        {

        }
        else if(jumped ==false)
        {
            float distanceFromPlayer = PlayerObj.transform.position.x - transform.position.x;
            if(m_Grounded)
            {
                rb.AddForce(new Vector2(distanceFromPlayer, jumpHeight), ForceMode2D.Impulse);
                jumped = true;               
            }
        }

        
        if (m_Grounded && jumped)
        {
            TempoPular = 0;
            status = GuardianStatus.desativado;
            tempoTonto = 0;
            jumped = false;

        }
        
    }
    public void JumpAtEdge()
    {
        TempoPular += Time.deltaTime;
        if (TempoPular < tempoParaPular)
        {

        }
        else if (jumped == false)
        {
            float distanceFromPlayer = EdgeRooms[EdgeIndex].transform.position.x - transform.position.x;
            if (m_Grounded)
            {
                rb.AddForce(new Vector2(distanceFromPlayer, jumpHeight), ForceMode2D.Impulse);
                jumped = true;
            }
        }

        if (m_Grounded == true && jumped == true)
        {
            CurrentTimeDash = 0;
            TempoPular = 0;
            status = GuardianStatus.desativado;
            tempoTonto = 0;
            jumped = false;
            dash = true;
        }

    }
    private void Dash()
    {
  
        CurrentTimeDash += Time.deltaTime;
        
        if(CurrentTimeDash >= TimeToDoDash)
        {
            
        }
        else if(dashed == false)
        {
            AttackTrigger.SetActive(true);
            if (lookingRight)
            {
                rb.AddForce(new Vector2(DashImpulse, transform.position.y), ForceMode2D.Impulse);
            }
            if (!lookingRight)
            {
                rb.AddForce(new Vector2(-DashImpulse, transform.position.y), ForceMode2D.Impulse);
            }
            dashed = true;
        }
        else if(dashed)
        {
            tempoTonto = 0;
            status = GuardianStatus.desativado;
            dash = false;
            dashed = false;
            AttackTrigger.SetActive(false);
        }
    }
    public void LookAtPlayer()
    {
        if(status == GuardianStatus.desativado && Attacked == false || Attacked == false)
        {
            Vector3 look = transform.InverseTransformPoint(PlayerObj.transform.position);
            float angle = Mathf.Atan2(0f, look.x) * Mathf.Rad2Deg;

            transform.Rotate(0f, angle, 0f);

            
        }
        if (transform.eulerAngles.y > 100)
        {
            lookingRight = false;
        }
        else
        {
            lookingRight = true;
        }

    }

}
