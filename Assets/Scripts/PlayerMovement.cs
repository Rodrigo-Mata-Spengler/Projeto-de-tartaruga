using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D CharacterController2D;

    public float RunSpeed = 40f;

    private float HorizontalMove = 0f;

    public float jump = 0;
    public bool crouch = false;
    public bool OnAir = false;

    private void Update()
    {
        HorizontalMove = Input.GetAxis("Horizontal") * RunSpeed;

        if (Input.GetButton("Jump") && Input.GetAxis("Jump") != 1)
        {
            OnAir = true;
            jump = Input.GetAxis("Jump");
            
        }
        else
        {
            jump = 0;
            OnAir = false;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if(Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }


        Debug.LogWarning(jump);
    }

    private void FixedUpdate()
    {
        CharacterController2D.Move(HorizontalMove * Time.fixedDeltaTime, false, jump, OnAir);
        jump = 0f;
    }
}
