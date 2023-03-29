using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    [SerializeField]private int JumpTimes = 0; 
    [SerializeField] private int JumpReleasedTimes = 0;

    [Header("Ataque")]
    public GameObject AtaqueHitBox;
    public GameObject AtaqueUpHitBox;
    public GameObject AtaqueDownHitBox;
    public float AttackTimeAmount;
    [HideInInspector] public bool AttackEnd = true;
    
    private TrailRenderer trailRender;
    private void Start()
    {
        m_Rigidbody2D = CharacterController2D.m_Rigidbody2D;
        trailRender = this.GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        HorizontalMove = Input.GetAxis("Horizontal") * RunSpeed;

        m_Grounded = CharacterController2D.m_Grounded;


        jump();

        Attack();


        // Debug.LogWarning(jump);
    }

    private void FixedUpdate()
    {
        CharacterController2D.Move(HorizontalMove * Time.fixedDeltaTime, false, jumpVel);
      
    }

    public void jump()
    {
        //jump
        jumpInput = Input.GetButtonDown("Jump");
        jumpInputReleased = Input.GetButtonUp("Jump");

        //checks if player is on ground and pressed the jump input
        if (m_Grounded && jumpInput)
        {
            JumpReleasedTimes = 0;
            // Add a vertical force to the player.

            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, jumpVel);
            JumpTimes = 1;
            trailRender.emitting = true;
            
        }
        // if he released fall smoothly
        if (jumpInputReleased && m_Rigidbody2D.velocity.y > 0)
        {
            JumpReleasedTimes += 1;
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_Rigidbody2D.velocity.x / _yVelJumpRealeasedMod);
            trailRender.emitting = false;
        }
        //Do the second jump
        if (jumpInput && JumpReleasedTimes == 1)
        {
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, jumpVel);
            JumpTimes = 2;
            trailRender.emitting = true;
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
        if (Input.GetButtonDown("Fire1") && Input.GetAxis("Vertical") == 0 && AttackEnd)
        {
            AttackEnd= false;
            AtaqueHitBox.SetActive(true);
            AtaqueHitBox.GetComponent<Ataque>().right = true;

            StartCoroutine(AttackTime(AttackTimeAmount,AtaqueHitBox));
        }
        //enables the up hitBox
        if (Input.GetButtonDown("Fire1") && Input.GetAxis("Vertical") > 0 && AttackEnd) 
        {
            AttackEnd = false;
            AtaqueUpHitBox.SetActive(true);
            AtaqueUpHitBox.GetComponent<Ataque>().down = true; 

            StartCoroutine(AttackTime(AttackTimeAmount, AtaqueUpHitBox));

        }
        //enables the bottom hitBox
        if (Input.GetButtonDown("Fire1") && Input.GetAxis("Vertical") < 0 && !m_Grounded && AttackEnd) 
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
