using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public float DashForce;

    private CharacterController2D characterController;

    [HideInInspector]
    public Rigidbody2D rb;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        characterController = this.GetComponent<CharacterController2D>();
      
    }


    private void Update()
    {
        if(Input.GetAxis("Dash") > 0f && characterController.m_FacingRight)
        {
            rb.velocity = new Vector2(DashForce ,1);
        }
        if(Input.GetAxis("Dash") > 0f && !characterController.m_FacingRight)
        {
            rb.velocity = new Vector2((DashForce * -1), 1);
        }
    }


}
