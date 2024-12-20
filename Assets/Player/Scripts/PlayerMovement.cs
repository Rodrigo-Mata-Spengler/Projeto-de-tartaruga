using System.Collections;
using UnityEngine;
using UnityEditor;
using FMOD.Studio;
using UnityEngine.VFX;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{
    

    [SerializeField] public bool HaveMagicTrident = false;
    [Space]
    public bool HaveArmor = false;
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
    private bool jumped = false;

    [SerializeField]private int JumpTimes = 0; 
    [SerializeField] private int JumpReleasedTimes = 0;

    [SerializeField] private VisualEffect FallEffect;

    [Header("Ataque")]

    public GameObject AtaqueHitBox;
    public GameObject AtaqueUpHitBox;
    public GameObject AtaqueDownHitBox;

    public float AttackTimeAmount;///The time that the attack will be enabled
    public bool AttackEnd = true;
    [Space]
    public float AtaqueRate = 15f;
    public float NextTimeToAtaque = 0f;
    private bool rightAttack = false;

    [HideInInspector] public bool moving = false; // variable to player lookUporDown
    [HideInInspector] public bool AtaqueInput;

    [Header("Wall Jump")]
    public bool haveWallJump = false;
    public Transform wallCheck;/// get's a transform of a child from the player and to do a raycast
    public float WallCheckDistance;/// the size of the ray
    public LayerMask WhatIsGround;
    public bool IsTouchingWall = false;
    public bool IsTouchingWall2 = false;
    public bool IsWallSliding;
    public float WallSlideSpeed;///the speed the the player will slide down while grabbing the wall and not moving 
    [Space]
    public float wallJumpForce; 
    public Vector2 wallJumpDirection; ///the direction that the player will go while on wall
    private int facingDirection;

    [Space]

    [Header("Dash")]
    [SerializeField] private Transform[] WallChecksDash;

    private bool canMove = true;

    private Estamina estaminaScript;

    private EventInstance PlayerFootstep;
    private void Start()
    {
        jumped = false;

        m_Rigidbody2D = CharacterController2D.m_Rigidbody2D;

        m_Animator = this.GetComponent<Animator>();

        estaminaScript=this.GetComponent<Estamina>();

        PlayerFootstep = AudioManager.instance.CreateEventInstance(FMODEvents.instance.PlayerFootstep);


        if (HaveArmor)
        {
            m_Animator.SetBool("Armadura",true);

            AtaqueHitBox.GetComponent<Ataque>().HaveMagicTrident = true;
            AtaqueUpHitBox.GetComponent<Ataque>().HaveMagicTrident = true;
            AtaqueDownHitBox.GetComponent<Ataque>().HaveMagicTrident = true;
        }
        else if (HaveMagicTrident)
        {
            m_Animator.SetBool("Magico", true);

            AtaqueHitBox.GetComponent<Ataque>().HaveMagicTrident = true;
            AtaqueUpHitBox.GetComponent<Ataque>().HaveMagicTrident= true;
            AtaqueDownHitBox.GetComponent<Ataque>().HaveMagicTrident = true;
        }
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
            m_Animator.ResetTrigger("Dano");

            if(jumped)
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.Fall, transform.position);
                FallEffect.Play();
                
            }
            jumped = false;

        }
        else
        {
            OnAir = true;
            jumped = true;
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
           //AttackSound();

        }
        else if(AtaqueInput && NextTimeToAtaque > Time.time )
        {
            
            m_Animator.SetInteger("Ataque normal index", 2);
            AttackTimeAmount = 0.5f;
            //AttackSound();
        }

        // Debug.LogWarning(jump);
    }

    public void CheckSurroundings()
    {
        /// Creates the raycast for the walljump
        IsTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, WallCheckDistance, WhatIsGround);
      

        if (gameObject.GetComponent<Dash>().canDash)
        {
            foreach (Transform WallCheck in WallChecksDash)
            {
                RaycastHit2D hitWall = Physics2D.Raycast(WallCheck.position, transform.right, 7, WhatIsGround);

                if (hitWall.collider != null)
                {
                    float distance = Mathf.Abs(hitWall.point.x - transform.position.x);
                    gameObject.GetComponent<Dash>().m_DashDist = (distance - 0.5f);
                    gameObject.GetComponent<Dash>().dashTime = 0.15f;
                }
                else
                {
                    gameObject.GetComponent<Dash>().m_DashDist = 7;
                    gameObject.GetComponent<Dash>().dashTime = 0.25f;
                }
            }
        }


    }
    
    private void CheckWallSliding()
    {
        if (IsTouchingWall && !CharacterController2D.m_Grounded && m_Rigidbody2D.velocity.y <= 0)
        {
            
            IsWallSliding = true;
            JumpReleasedTimes = 1;

            m_Animator.SetBool("Parede", true);
        }
        else
        {
            IsWallSliding = false;
            m_Animator.SetBool("Parede", false);
        }

        /*
        if (HaveMagicTrident)
        {
            if (IsTouchingWall && !CharacterController2D.m_Grounded && m_Rigidbody2D.velocity.y < 0 && HorizontalMove != 0)
            {
                IsWallSliding = true;
                JumpReleasedTimes = 1;

                m_Animator.SetBool("Parede", true);
            }
            if (!IsTouchingWall)
            {
                IsWallSliding = false;
                m_Animator.SetBool("Parede", false);
            }
        }
        else
        {
            if (IsTouchingWall && !CharacterController2D.m_Grounded && m_Rigidbody2D.velocity.y < 0 && HorizontalMove != 0)
            {

                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, -7);
                m_Animator.SetBool("Parede", true);
            }
            if (!IsTouchingWall)
            {
                IsWallSliding = false;
                m_Animator.SetBool("Parede", false);
            }
            
        }
        */
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

            m_Animator.SetBool("Pulo", true);
            AudioManager.instance.PlayOneShot(FMODEvents.instance.Jump, this.transform.position);

            
        }
        // if he released fall smoothly
        if (jumpInputReleased && m_Rigidbody2D.velocity.y > 0 && !IsTouchingWall)
        {
            JumpReleasedTimes += 1;
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_Rigidbody2D.velocity.x / _yVelJumpRealeasedMod);
            m_Animator.SetBool("Pulo", false);
        }
        //Do the second jump
        if (jumpInput && JumpReleasedTimes == 1 && !IsTouchingWall && haveDoubleJump) 
        {
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, jumpVel);
            JumpTimes = 2;
            m_Animator.SetBool("Pulo", true);
            AudioManager.instance.PlayOneShot(FMODEvents.instance.Jump, this.transform.position);

            
        }
        if(IsTouchingWall && jumpInput && haveWallJump && !m_Grounded)
        {
            IsWallSliding = false;
            //m_Rigidbody2D.velocity = new Vector2(wallJumpForce * wallJumpDirection.x * -facingDirection, jumpVel);
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
        //m_Rigidbody2D.velocity = Vector2.zero;
        //enables the attack hitbox to right and left
        if (Input.GetAxis("Vertical") == 0)
        {
            rightAttack = true;
            m_Animator.SetBool("Ataque normal", true); // player animation


            AtaqueHitBox.SetActive(true);

            StartCoroutine(AttackTime(AttackTimeAmount, AtaqueHitBox));
            

        }
        //enables the up hitBox
        if (Input.GetAxis("Vertical") > 0) 
        {
            rightAttack = false;
            m_Animator.SetBool("Ataque cima", true); // player animation

            AtaqueUpHitBox.SetActive(true);
            StartCoroutine(AttackTime(AttackTimeAmount, AtaqueUpHitBox));
            


        }
        //enables the bottom hitBox
        if (Input.GetAxis("Vertical") < 0 && !m_Grounded ) 
        {
            rightAttack = false;
            m_Animator.SetBool("Ataque baixo", true); // player animation
            AtaqueDownHitBox.SetActive(true);
            StartCoroutine(AttackTime(AttackTimeAmount, AtaqueDownHitBox));

            
        }


        
        AttackEnd = false;

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



    }
          
    public void AttackSound()
    {
        if (HaveMagicTrident)
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.AttackSoundWater, this.transform.position);
        }
        else
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.AttackSound, this.transform.position);
        }
    }
 
    public void GiveArmorAnimation()
    {
        m_Animator.SetBool("Armadura", true);
    }
   /*
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
   */
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


