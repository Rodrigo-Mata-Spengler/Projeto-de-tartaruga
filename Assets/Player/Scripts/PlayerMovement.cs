using System.Collections;
using UnityEngine;
using UnityEditor;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] public bool HaveMagicTrident = false;
    [Space]
    private Animator m_Animator;

    [Header("Movment")]
    public CharacterController2D CharacterController2D;
    [HideInInspector] public Rigidbody2D m_Rigidbody2D;
    [HideInInspector]public bool m_Grounded;
    private float RunAnimator;

    public float RunSpeed = 40f; // speed velocity
    [HideInInspector]public float HorizontalMove = 0f;

    public bool crouch = false;
    public bool OnAir = false;


    [Header("jump")]
    public float _yVelJumpRealeasedMod = 2f; //variable to smooth when falling
    public float jumpVel = 20f; //jump velocity

    private bool jumpInput;
    private bool jumpInputReleased;
    public bool haveDoubleJump = false;

    [SerializeField]private int JumpTimes = 0; 
    [SerializeField] private int JumpReleasedTimes = 0;

    [Header("Ataque")]
    public GameObject AtaqueHitBox;
    public GameObject AtaqueUpHitBox;
    public GameObject AtaqueDownHitBox;
    public float AttackTimeAmount;///The time that the attack will be enabled
    public bool AttackEnd = true;
    [Space]
    public float AtaqueRate = 15f;
    public float NextTimeToAtaque = 0f;

    [HideInInspector] public bool moving = false; // variable to player lookUporDown
    [HideInInspector] public bool AtaqueInput;

    [Header("Wall Jump")]
    public bool haveWallJump = false;
    public Transform wallCheck;/// get's a transform of a child from the player and to do a raycast
    public float WallCheckDistance;/// the size of the ray
    public LayerMask WhatIsGround;
    public bool IsTouchingWall = false;
    public bool IsWallSliding;
    public float WallSlideSpeed;///the speed the the player will slide down while grabbing the wall and not moving 
    [Space]
    public float wallJumpForce; 
    public Vector2 wallJumpDirection; ///the direction that the player will go while on wall
    private int facingDirection;

    private TrailRenderer trailRender;

    private bool canMove = true;
    private void Start()
    {
        m_Rigidbody2D = CharacterController2D.m_Rigidbody2D;
        trailRender = this.GetComponent<TrailRenderer>();

        m_Animator = this.GetComponent<Animator>();
    }

    private void Update()
    {
        m_Grounded = CharacterController2D.m_Grounded;
        jumpInput = Input.GetButtonDown("Jump");
        jumpInputReleased = Input.GetButtonUp("Jump");

        HorizontalMove = Input.GetAxis("Horizontal") * RunSpeed;
        m_Animator.SetFloat("Run", HorizontalMove);//pass the value to the animator

        facingDirection = CharacterController2D.facingDirection;
        AtaqueInput = Input.GetButtonDown("Fire1");

        ///check if the player is moving
        if (HorizontalMove == 0f)
        { 
            moving= false;
        }
        if(HorizontalMove != 0f)
        {
            moving= true;
        }

        if (jumpInput || jumpInputReleased)
        {
            jump();
        }

        if (m_Grounded)
        {
            OnAir = false;
            
        }
        else
        {
            OnAir = true;
        }

        CheckSurroundings();
        CheckWallSliding();
        ///check if the player have the ability to wall jump
        ///Check's id the player is slifing
        if (IsWallSliding)
        {
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, -WallSlideSpeed);

        }
        if (AtaqueInput && Time.time > NextTimeToAtaque)
        {
           
           NextTimeToAtaque = Time.time + 1 / AtaqueRate;
           Attack();
        }

        // Debug.LogWarning(jump);
    }

    public void CheckSurroundings()
    {
        /// Creates the raycast for the walljump
        IsTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, WallCheckDistance, WhatIsGround);

    }
    private void CheckWallSliding()
    {
        
        if (IsTouchingWall && !CharacterController2D.m_Grounded && m_Rigidbody2D.velocity.y < 0 && HorizontalMove != 0)
        {
            IsWallSliding = true;
            JumpReleasedTimes = 1;
        }
        if(!IsTouchingWall)
        {
            IsWallSliding = false;
        }

    }
    public void AtivarCheat()
    {
        haveWallJump = true;
        haveDoubleJump = true;
    }
    private void OnDrawGizmos()
    {
        /// draw the wall jump ray cast
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + WallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }
    private void FixedUpdate()
    {
        if(canMove)
        {
            CharacterController2D.Move(HorizontalMove * Time.fixedDeltaTime, false, jumpVel);
        }
    }
    IEnumerator StopMove()
    {
        canMove = false;
        transform.localScale = transform.localScale.x == 1 ? new Vector2(-1, 1) : Vector2.one;

        yield return new WaitForSeconds(.1f);

        transform.localScale = Vector2.one;
        canMove = true;
    }
    public void jump()
    {
        //jump

        //checks if player is on ground and pressed the jump input
        if (m_Grounded && jumpInput)
        {
            JumpReleasedTimes = 0;
            // Add a vertical force to the player.

            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, jumpVel);
            JumpTimes = 1;
            trailRender.emitting = true;

            m_Animator.SetBool("Pulo", jumpInput);

        }
        // if he released fall smoothly
        if (jumpInputReleased && m_Rigidbody2D.velocity.y > 0 && !IsTouchingWall)
        {
            JumpReleasedTimes += 1;
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_Rigidbody2D.velocity.x / _yVelJumpRealeasedMod);
            trailRender.emitting = false;
            m_Animator.SetBool("Pulo", jumpInput);
        }
        //Do the second jump
        if (jumpInput && JumpReleasedTimes == 1 && !IsTouchingWall && haveDoubleJump) 
        {
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, jumpVel);
            JumpTimes = 2;
            trailRender.emitting = true;
            m_Animator.SetBool("Pulo", jumpInput);
        }
        if(IsTouchingWall && jumpInput && haveWallJump && !m_Grounded)
        {
            IsWallSliding = false;
            //m_Rigidbody2D.velocity = new Vector2(wallJumpForce * wallJumpDirection.x * -facingDirection, jumpVel);
            trailRender.emitting = true;
            Vector2 force = new Vector2(wallJumpForce * wallJumpDirection.x *-facingDirection, wallJumpForce * wallJumpDirection.y);
            StartCoroutine(StopMove());
            //m_Rigidbody2D.velocity = Vector2.zero;

            m_Rigidbody2D.AddForce(force, ForceMode2D.Impulse);
            m_Animator.SetBool("Pulo", jumpInput);
        }

    }
    public void Attack()
    {
        m_Rigidbody2D.velocity = Vector2.one;
        //enables the attack hitbox to right and left
        if (Input.GetAxis("Vertical") == 0)
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.AttackSound, this.transform.position);
            AttackEnd= false;
            AtaqueHitBox.SetActive(true);
            AtaqueHitBox.GetComponent<Ataque>().right = true;

            m_Animator.SetBool("Ataque normal", true);

            //Ataque animation
            if(m_Animator.GetInteger("Ataque normal index") == 0)
            {
                m_Animator.SetInteger("Ataque normal index", 1);
            }
            
            else if (m_Animator.GetInteger("Ataque normal index") == 1)
            {
                m_Animator.SetInteger("Ataque normal index", 2);
            }
            else if (m_Animator.GetInteger("Ataque normal index") == 2)
            {
                m_Animator.SetInteger("Ataque normal index", 0);
            }
            
            StartCoroutine(AttackTime(AttackTimeAmount,AtaqueHitBox));
        }
        //enables the up hitBox
        if (Input.GetAxis("Vertical") > 0) 
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.AttackSound, this.transform.position);
            AttackEnd = false;
            AtaqueUpHitBox.SetActive(true);
            AtaqueUpHitBox.GetComponent<Ataque>().down = true;

            m_Animator.SetBool("Ataque cima", true);
            StartCoroutine(AttackTime(AttackTimeAmount, AtaqueUpHitBox));

        }
        //enables the bottom hitBox
        if (Input.GetAxis("Vertical") < 0 && !m_Grounded ) 
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.AttackSound, this.transform.position);
            AttackEnd = false;
            AtaqueDownHitBox.SetActive(true);
            AtaqueDownHitBox.GetComponent<Ataque>().up= true;

            m_Animator.SetBool("Ataque baixo", true);
            StartCoroutine(AttackTime(AttackTimeAmount,AtaqueDownHitBox));
        }

    }
    //disable the current enabled hitbox
    IEnumerator AttackTime(float AttackTimeAmount, GameObject HitBox)
    {
        yield return new WaitForSeconds(AttackTimeAmount);
        HitBox.GetComponent<Ataque>().Detected = false;
        HitBox.GetComponent<Ataque>().HitIndex = 0;
        HitBox.SetActive(false);
        AttackEnd = true;

        m_Animator.SetBool("Ataque baixo", false);
        m_Animator.SetBool("Ataque cima", false);

        m_Animator.SetBool("Ataque normal", false);


    }

    /*public void Crouch()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
    }*/
}


