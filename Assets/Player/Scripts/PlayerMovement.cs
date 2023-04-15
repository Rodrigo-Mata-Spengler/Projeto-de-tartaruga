using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using System.Threading;
using UnityEditor;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movment")]
    public CharacterController2D CharacterController2D;
    [HideInInspector] public Rigidbody2D m_Rigidbody2D;
    [HideInInspector]public bool m_Grounded;

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

    [HideInInspector] public bool moving = false; // variable to player lookUporDown


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
    }

    private void Update()
    {
        m_Grounded = CharacterController2D.m_Grounded;
        jumpInput = Input.GetButtonDown("Jump");
        jumpInputReleased = Input.GetButtonUp("Jump");
        HorizontalMove = Input.GetAxis("Horizontal") * RunSpeed;
        facingDirection = CharacterController2D.facingDirection;

        ///check if the player is moving
        if(HorizontalMove == 0f)
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

        CheckSurroundings();
        CheckWallSliding();
        ///check if the player have the ability to wall jump
        ///Check's id the player is slifing
        if (IsWallSliding)
        {
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, -WallSlideSpeed);

        }
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }



        if (m_Grounded && OnAir)
        {
            OnAir = false;
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
        if (m_Grounded && jumpInput && !IsTouchingWall)
        {
            JumpReleasedTimes = 0;
            // Add a vertical force to the player.

            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, jumpVel);
            JumpTimes = 1;
            trailRender.emitting = true;
            
        }
        // if he released fall smoothly
        if (jumpInputReleased && m_Rigidbody2D.velocity.y > 0 && !IsTouchingWall)
        {
            JumpReleasedTimes += 1;
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_Rigidbody2D.velocity.x / _yVelJumpRealeasedMod);
            trailRender.emitting = false;
        }
        //Do the second jump
        if (jumpInput && JumpReleasedTimes == 1 && !IsTouchingWall && haveDoubleJump) 
        {
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, jumpVel);
            JumpTimes = 2;
            trailRender.emitting = true;
        }
        if(IsTouchingWall && jumpInput && haveWallJump)
        {
            IsWallSliding = false;
            //m_Rigidbody2D.velocity = new Vector2(wallJumpForce * wallJumpDirection.x * -facingDirection, jumpVel);
            trailRender.emitting = true;
            Vector2 force = new Vector2(wallJumpForce * wallJumpDirection.x *-facingDirection, wallJumpForce * wallJumpDirection.y);
            StartCoroutine(StopMove());
            //m_Rigidbody2D.velocity = Vector2.zero;

            m_Rigidbody2D.AddForce(force, ForceMode2D.Impulse);
        }
        if(m_Grounded)
        {
            OnAir = false;
        }
        else
        {
            OnAir = true;
        }
    }
    public void Attack()
    {
        //enables the attack hitbox to right and left
        if (Input.GetAxis("Vertical") == 0)
        {
            AttackEnd= false;
            AtaqueHitBox.SetActive(true);
            AtaqueHitBox.GetComponent<Ataque>().right = true;

            StartCoroutine(AttackTime(AttackTimeAmount,AtaqueHitBox));
        }
        //enables the up hitBox
        if (Input.GetAxis("Vertical") > 0) 
        {
            AttackEnd = false;
            AtaqueUpHitBox.SetActive(true);
            AtaqueUpHitBox.GetComponent<Ataque>().down = true; 

            StartCoroutine(AttackTime(AttackTimeAmount, AtaqueUpHitBox));

        }
        //enables the bottom hitBox
        if (Input.GetAxis("Vertical") < 0 && !m_Grounded ) 
        {
            AttackEnd = false;
            AtaqueDownHitBox.SetActive(true);
            AtaqueDownHitBox.GetComponent<Ataque>().up= true;
 
            StartCoroutine(AttackTime(AttackTimeAmount,AtaqueDownHitBox));
        }

    }
    //disable the current enabled hitbox
    IEnumerator AttackTime(float AttackTimeAmount, GameObject HitBox)
    {
        yield return new WaitForSeconds(AttackTimeAmount);
        HitBox.SetActive(false);
        AttackEnd = true;
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

[CustomEditor(typeof(PlayerMovement))]
public class MyScriptEditor : Editor
{
    PlayerMovement playermovment;

    private void OnEnable()
    {
        playermovment = (PlayerMovement)target;
    }
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
    }
}
[System.Serializable]

public class WallJump
{

}
