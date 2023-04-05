using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

enum GuardianStatus { desativado, Chase, Attack, DisableAttack, Jump };
public class GuardianBehavior : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] private GuardianStatus status = GuardianStatus.desativado;

    [Header("StartFight")]
    public bool StartFight = false;

    private Rigidbody2D rb;
    [Space]
    [Header("Circle Cast Variables")]
    public float radius;//trigger area
    public bool PlayerClose = false;// detecte if a Player was inside
    Vector2 direction;
    public LayerMask PlayerLayer;

    [Header("LookAt")]
    [HideInInspector] public GameObject PlayerTransform;
    [Space]

    [Header("Chase")]
    public bool Chase;
    public float ChaseSpeed;
    private int ChaseWhenAwake = 0;
    [Space]
    public int AttacOrJump = 0;
    public int AttackOrJumpPorcentage;
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
        status = GuardianStatus.Chase;
    }
    // Update is called once per frame
    void Update()
    {

        switch (status)
        {
            case GuardianStatus.Chase:
                ChasePlayer();
                break;

            case GuardianStatus.Jump:
                JumpAtPlayer();
                break;

            case GuardianStatus.Attack:
                if(!Attacked)
                {
                    StartCoroutine(AttackPlayer(WaitToAttackTime));
                }
                break;
            case GuardianStatus.DisableAttack:
                StartCoroutine(DisableAttack(AttackDuration, CoolDownToNextAttack));
                break;

            case GuardianStatus.desativado:
                if(!PlayerClose)
                {
                    Porcentage();
                }
                break;
        }
        CircleTrigger();

        LookAtPlayer();

        if (PlayerClose)
        {
            status = GuardianStatus.Attack;
        }
        if (jumped && m_Grounded)
        {
            StartCoroutine(DisableJump(LandJumpCoolDown));
        }
        if (AttacOrJump == 0)
        {
            status = GuardianStatus.Jump;
        }
        if (AttacOrJump == 1)
        {
            status = GuardianStatus.Chase;
        }
    }

    private int Porcentage()
    {
        int i = 0;
        if(i ==0)
        {
            AttackOrJumpPorcentage = Random.Range(0, 100);
            i = 1;
        }
        

        if (AttackOrJumpPorcentage > 0 && AttackOrJumpPorcentage <= 30)
        {
            AttacOrJump = 0;
            i = 0;
            status = GuardianStatus.Jump;
            
        }
        if (AttackOrJumpPorcentage >= 31 && AttackOrJumpPorcentage <=100)
        {
            status = GuardianStatus.Chase;
            i = 0;
            AttacOrJump = 1;
        }
        return AttacOrJump;
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
    public IEnumerator AttackPlayer(float WaitToAttack)
    {
        yield return new WaitForSeconds(WaitToAttack);
        AttackTrigger.SetActive(true);
        Attacked = true;
        status = GuardianStatus.DisableAttack;
        yield break;

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

    public void ChasePlayer()
    {
        if(PlayerClose == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, PlayerTransform.transform.position, ChaseSpeed * Time.deltaTime);
        }
        else
        {
            StartCoroutine(DisableChase());
        }

    }
    public IEnumerator DisableChase()
    {
        yield return new WaitForSeconds(0.3f);
       status = GuardianStatus.desativado;

    }
    public void LookAtPlayer()
    {
        if(status == GuardianStatus.desativado && Attacked == false || status == GuardianStatus.Chase && Attacked == false)
        {
            Vector3 look = transform.InverseTransformPoint(PlayerTransform.transform.position);
            float angle = Mathf.Atan2(0f, look.x) * Mathf.Rad2Deg;

            transform.Rotate(0f, angle, 0f);
        }

    }
    public void CircleTrigger()
    {
        direction = Vector2.zero;

        //creates the box cast(trigger)
        RaycastHit2D CircleInfo = Physics2D.CircleCast(gameObject.GetComponent<Renderer>().bounds.center, radius, direction);

        //checks if the player is inside the area
        if (CircleInfo.collider.CompareTag("Player") && !PlayerClose)
        {
            PlayerClose = true;
        }
        if (CircleInfo.collider.gameObject.tag != "Player" && PlayerClose)
        {
            PlayerClose = false;
            status = GuardianStatus.desativado;
        }
    }

    public void OnDrawGizmosSelected()
    {
        //Draw the box on unity
        Gizmos.DrawWireSphere(gameObject.GetComponent<Renderer>().bounds.center, radius);

    }
}
