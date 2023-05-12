using System.Collections;
using UnityEngine;
using UnityEditor;
using FMOD.Studio;

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
    private Animator AtaqueHitBox_Animator;

    public GameObject AtaqueMagicoHitBox;
    private Animator AtaqueMagicoHitBox_Animator;
    [Space]

    public GameObject AtaqueUpHitBox;
    private Animator AtaqueUpHitBox_Animator;

    public GameObject AtaqueUpHitBoxMagico;
    private Animator AtaqueUpHitBoxMagico_Animator;
    [Space]

    public GameObject AtaqueDownHitBox;
    private Animator AtaqueDownHitBox_Animator;

    public GameObject AtaqueDownHitBoxMagico;
    private Animator AtaqueDownHitBoxMagico_Animator;

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

    private Estamina estaminaScript;

    private EventInstance PlayerFootstep;
    private void Start()
    {
        m_Rigidbody2D = CharacterController2D.m_Rigidbody2D;
        trailRender = this.GetComponent<TrailRenderer>();

        m_Animator = this.GetComponent<Animator>();

        estaminaScript=this.GetComponent<Estamina>();


        AtaqueHitBox_Animator = AtaqueHitBox.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
        AtaqueUpHitBox_Animator = AtaqueUpHitBox.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
        AtaqueDownHitBox_Animator = AtaqueDownHitBox.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();

        AtaqueMagicoHitBox_Animator = AtaqueMagicoHitBox.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
        AtaqueUpHitBoxMagico_Animator = AtaqueUpHitBoxMagico.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
        AtaqueDownHitBoxMagico_Animator = AtaqueDownHitBoxMagico.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();

        PlayerFootstep = AudioManager.instance.CreateEventInstance(FMODEvents.instance.PlayerFootstep);
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
            m_Animator.SetBool("Fall", false);

        }
        else
        {
            OnAir = true;
           

        }

        if(OnAir &&  m_Rigidbody2D.velocity.y > 0)
        {
            m_Animator.SetBool("Fall", true);
            m_Animator.SetBool("Pulo", false);
            
        }

        CheckSurroundings();
        CheckWallSliding();
        ///check if the player have the ability to wall jump
        ///Check's id the player is slifing
        if (IsWallSliding)
        {
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, -WallSlideSpeed);

        }
        if (AtaqueInput && Time.time > NextTimeToAtaque && m_Animator.GetBool("Ataque normal") == false)
        {
           
           AttackTimeAmount = 0.3f;
           NextTimeToAtaque = Time.time + 1 / AtaqueRate;
           Attack();
           m_Animator.SetInteger("Ataque normal index", 1);

        }
        else if(AtaqueInput && NextTimeToAtaque > Time.time )
        {

            m_Animator.SetInteger("Ataque normal index", 2);
            if (!HaveMagicTrident)
            {
                AtaqueHitBox_Animator.SetInteger("AtaqueNormalIndex", 2);
            }
            else
            {
                AtaqueMagicoHitBox_Animator.SetInteger("AtaqueIndex", 2);
            }
            AttackTimeAmount = 0.5f;
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
        //UpdateSound();
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

            m_Animator.SetBool("Pulo", true);
            AudioManager.instance.PlayOneShot(FMODEvents.instance.Jump, this.transform.position);
        }
        // if he released fall smoothly
        if (jumpInputReleased && m_Rigidbody2D.velocity.y > 0 && !IsTouchingWall)
        {
            JumpReleasedTimes += 1;
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_Rigidbody2D.velocity.x / _yVelJumpRealeasedMod);
            trailRender.emitting = false;
            m_Animator.SetBool("Pulo", false);
        }
        //Do the second jump
        if (jumpInput && JumpReleasedTimes == 1 && !IsTouchingWall && haveDoubleJump) 
        {
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, jumpVel);
            JumpTimes = 2;
            trailRender.emitting = true;
            m_Animator.SetBool("Pulo", true);
            AudioManager.instance.PlayOneShot(FMODEvents.instance.Jump, this.transform.position);
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
            m_Animator.SetBool("Pulo", true);
            AudioManager.instance.PlayOneShot(FMODEvents.instance.Jump, this.transform.position);
        }

    }
    public void Attack()
    {
        m_Rigidbody2D.velocity = Vector2.one;
        //enables the attack hitbox to right and left
        if (Input.GetAxis("Vertical") == 0)
        {
            m_Animator.SetBool("Ataque normal", true); // player animation
            
            AudioManager.instance.PlayOneShot(FMODEvents.instance.AttackSound, this.transform.position);
            AttackEnd = false;
           
            if (HaveMagicTrident)
            {
                AtaqueMagicoHitBox.SetActive(true);
                AtaqueMagicoHitBox.GetComponent<Ataque>().right = true;
                StartCoroutine(AttackTime(AttackTimeAmount, AtaqueMagicoHitBox));
                AtaqueMagicoHitBox_Animator.SetInteger("AtaqueIndex", 1);
            }
            else
            {
                AtaqueHitBox.SetActive(true);
                AtaqueHitBox.GetComponent<Ataque>().right = true;
                StartCoroutine(AttackTime(AttackTimeAmount, AtaqueHitBox));
                AtaqueHitBox_Animator.SetInteger("AtaqueNormalIndex", 1);

            }
            
        }
        //enables the up hitBox
        if (Input.GetAxis("Vertical") > 0) 
        {
            m_Animator.SetBool("Ataque cima", true); // player animation
            if (HaveMagicTrident)
            {
                AtaqueUpHitBoxMagico.SetActive(true);
                AtaqueUpHitBoxMagico.GetComponent<Ataque>().up = true;
                AtaqueUpHitBoxMagico_Animator.SetBool("Ataque", true);
                StartCoroutine(AttackTime(AttackTimeAmount, AtaqueUpHitBoxMagico));
            }
            else
            {
                AtaqueUpHitBox.SetActive(true);
                AtaqueUpHitBox.GetComponent<Ataque>().up = true;
                AtaqueUpHitBox_Animator.SetBool("Ataque", true);
                StartCoroutine(AttackTime(AttackTimeAmount, AtaqueUpHitBox));
                
            }
            AudioManager.instance.PlayOneShot(FMODEvents.instance.AttackSound, this.transform.position);
            AttackEnd = false;




        }
        //enables the bottom hitBox
        if (Input.GetAxis("Vertical") < 0 && !m_Grounded ) 
        {
            m_Animator.SetBool("Ataque baixo", true); // player animation
            if (HaveMagicTrident)
            {
                AtaqueDownHitBoxMagico.SetActive(true);
                AtaqueDownHitBoxMagico.GetComponent<Ataque>().down = true;
                AtaqueDownHitBoxMagico_Animator.SetBool("Ataque", true);
                StartCoroutine(AttackTime(AttackTimeAmount, AtaqueDownHitBoxMagico));
                
            }
            else
            {
                AtaqueDownHitBox.SetActive(true);
                AtaqueDownHitBox.GetComponent<Ataque>().down = true;
                AtaqueDownHitBox_Animator.SetBool("Ataque", true);
                StartCoroutine(AttackTime(AttackTimeAmount, AtaqueDownHitBox));
                

            }
            AudioManager.instance.PlayOneShot(FMODEvents.instance.AttackSound, this.transform.position);
            AttackEnd = false;

        }

    }
    //disable the current enabled hitbox
    IEnumerator AttackTime(float AttackDuration, GameObject HitBox)
    {
        yield return new WaitForSeconds(AttackDuration);
        HitBox.GetComponent<Ataque>().Detected = false;
        HitBox.GetComponent<Ataque>().HitIndex = 0;
        HitBox.SetActive(false);
        AttackEnd = true;

        m_Animator.SetBool("Ataque baixo", false);
        m_Animator.SetBool("Ataque cima", false);
        m_Animator.SetBool("Ataque normal", false);
        m_Animator.SetInteger("Ataque normal index", 0);

        AtaqueMagicoHitBox_Animator.SetInteger("AtaqueIndex", 0);
        AtaqueHitBox_Animator.SetInteger("AtaqueNormalIndex", 0);

        AtaqueDownHitBox.GetComponentInChildren<Animator>().SetBool("Ataque", false); 
        AtaqueDownHitBoxMagico.GetComponentInChildren<Animator>().SetBool("Ataque", false);

        AtaqueUpHitBoxMagico.GetComponentInChildren<Animator>().SetBool("Ataque", false);
        AtaqueUpHitBox.GetComponentInChildren<Animator>().SetBool("Ataque", false);
    }
          
 

    private void UpdateSound()
    {
        Debug.Log(HorizontalMove);
        if(HorizontalMove != 0 && m_Grounded)
        {
            PLAYBACK_STATE playbackState;
            PlayerFootstep.getPlaybackState(out playbackState);

            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                PlayerFootstep.start();
            }

        }
        else
        {
            PlayerFootstep.stop(STOP_MODE.IMMEDIATE);
        }
    }

    public void PlayFootstep()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.PlayerFootstep, this.transform.position);
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


