using System.Collections;
using System.Collections.Generic;
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
    public int AttackOrJumpPorcentage;
    [Header("Jump")]
    public float jumpHeight;
    public bool jump;

    [Space]
    [Header("Hit feedback")]
    public bool wasHit = false;
    public float impulseForce;
    public float secondsToDisable;

    [Space]
    [Header("Attack")]
    public GameObject AttackTrigger;
    public bool Attacked = false;
    public float WaitToAttackTime;
    public float AttackDuration;

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
        if (wasHit)
        {
            rb.AddForce((transform.right * -1) * impulseForce);
            StartCoroutine(DisableHitFeedback(secondsToDisable));
        }
        CircleTrigger();

        LookAtPlayer();

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
                StartCoroutine(DisableAttack(AttackDuration));
                break;
        }
    }

    private int Porcentage()
    {
        AttackOrJumpPorcentage = Random.Range(0, 100);
        Debug.Log("euuuuu");
        return AttackOrJumpPorcentage;

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

    }
    public IEnumerator DisableAttack( float AttackDuration)
    {
        yield return new WaitForSeconds(AttackDuration);
        Attacked = false;
        AttackTrigger.SetActive(false);
        status = GuardianStatus.desativado;

    }
    public void JumpAtPlayer()
    {
        if (m_Grounded)
        {
            float distanceFromPlayer = PlayerTransform.transform.position.x - transform.position.x;
            rb.AddForce(new Vector2(distanceFromPlayer, jumpHeight), ForceMode2D.Impulse);
            jump = true;

        }
        else
        {
            rb.AddForce(new Vector2(0f, 0f));
        }
        status = GuardianStatus.desativado;


    }
    public void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, PlayerTransform.transform.position, ChaseSpeed * Time.deltaTime);
    }
    public void LookAtPlayer()
    {
        if(status == GuardianStatus.desativado || status == GuardianStatus.Chase)
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
    private IEnumerator DisableHitFeedback(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        wasHit = false;
        status = GuardianStatus.desativado;
    }

    public void OnDrawGizmosSelected()
    {
        //Draw the box on unity
        Gizmos.DrawWireSphere(gameObject.GetComponent<Renderer>().bounds.center, radius);

    }
}
