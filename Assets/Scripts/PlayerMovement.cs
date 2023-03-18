using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D CharacterController2D;
    [HideInInspector] public Rigidbody2D m_Rigidbody2D;
    [HideInInspector] public  bool m_Grounded;

    public float RunSpeed = 40f;
    private float HorizontalMove = 0f;

    public bool crouch = false;
    public bool OnAir = false;


    [Header("JumpVariables")]
    private bool jumpInput;
    private bool jumpInputReleased;
    public float _yVelJumpRealeasedMod = 2f;
    public float jumpVel = 20f;


    private void Start()
    {
        m_Rigidbody2D = CharacterController2D.m_Rigidbody2D;
        
    }

    private void Update()
    {
        HorizontalMove = Input.GetAxis("Horizontal") * RunSpeed;

        m_Grounded = CharacterController2D.m_Grounded;

        //jump
        jumpInput = Input.GetButtonDown("Jump");
        jumpInputReleased = Input.GetButtonUp("Jump");

        if (m_Grounded && jumpInput)
        {
            // Add a vertical force to the player.

            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, jumpVel);

        }
        if (jumpInputReleased && m_Rigidbody2D.velocity.y > 0)
        {
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_Rigidbody2D.velocity.x / _yVelJumpRealeasedMod);
        }
        //


        /*
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if(Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
        */

     // Debug.LogWarning(jump);
    }

    private void FixedUpdate()
    {
        CharacterController2D.Move(HorizontalMove * Time.fixedDeltaTime, false, jumpVel);
      
    }
}
