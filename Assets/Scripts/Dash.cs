using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public float dashTime = 0.1f;
    public float m_DashDist;
    private float _currentDashTime = 0f;
    private bool _isDashing = false;
    public bool canDash;
    private Vector2 _dashStart, _dashEnd;

    private Rigidbody2D rb;
    public float _yVelJumpRealeasedMod = 2f;

    private CharacterController2D characterController2D;
    private bool Grounded;
    private bool facingRight;

    private TrailRenderer trailRender;

    private void Start()
    {
        characterController2D = this.GetComponent<CharacterController2D>();
        trailRender = this.GetComponent<TrailRenderer>();

        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Grounded = characterController2D.m_Grounded;
        facingRight = characterController2D.m_FacingRight;

        if (Input.GetButtonDown("Dash") && canDash == true)
        {
            if (_isDashing == false && facingRight)
            {
                // dash starts
                _isDashing = true;
                _currentDashTime = 0;
                _dashStart = transform.position;
                _dashEnd = new Vector2(_dashStart.x + m_DashDist, _dashStart.y);
                
            }

            if (_isDashing == false && !facingRight)
            {
                // dash starts
                _isDashing = true;
                _currentDashTime = 0;
                _dashStart = transform.position;
                _dashEnd = new Vector2(_dashStart.x - m_DashDist, _dashStart.y);
            }
            trailRender.emitting= true;
        }

        if (_isDashing)
        {

            canDash = false;
            // incrementing time
            _currentDashTime += Time.deltaTime;

            // a value between 0 and 1
            float perc = Mathf.Clamp01(_currentDashTime / dashTime);

            // updating position
            transform.position = Vector2.Lerp(_dashStart, _dashEnd, perc);

            if (_currentDashTime >= dashTime)
            {
                // dash finished
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.x / _yVelJumpRealeasedMod);
                transform.position = _dashEnd;
                trailRender.emitting= false;
                _isDashing = false;

            }

        }


        if(Grounded)
        {
            canDash = true;
            
        }

    }
}
