using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform PlayerTransform;

    [Header("Flip rotation stats")]
    [SerializeField] private float FlipYRotationTime = 0.5f;

    private CharacterController2D PlayerCharacterController;

    private bool _isFacingRight;


    public float DesireLerpDuration = 3;
    public float LerpElapsedTime;


    [SerializeField]
    private AnimationCurve curve;

    public bool FollowPlayer;

    [Header("Look up and Down")]
    public float heightAmount = 0f;
    public bool LookedUpOrDown = false;
    public float LerpTimeDurationUpAndDown = 3f;
    public float LerpElapsedTimeUpAndDown = 0f;
    [SerializeField]
    private AnimationCurve curveUpAndDown;

    private float currentY;

    private void Awake()
    {
        PlayerCharacterController = PlayerTransform.GetComponent<CharacterController2D>();

        _isFacingRight = PlayerCharacterController.m_FacingRight;
        transform.position = PlayerTransform.position;
    }

    private void Update()
    {
        //make the object follow the player if he's not on a no move area
        if(FollowPlayer)
        {
            transform.position = PlayerTransform.position;
            currentY = transform.position.y;
        }

        if (Input.GetAxis("Vertical") >0f  && PlayerCharacterController.m_Grounded && LerpElapsedTimeUpAndDown < LerpTimeDurationUpAndDown)
        {
            LerpUp(currentY);
        }
        if (Input.GetAxis("Vertical") < 0f  && PlayerCharacterController.m_Grounded && LerpElapsedTimeUpAndDown < LerpTimeDurationUpAndDown)
        {
            LerpDown(currentY);
        }
        if (Input.GetAxis("Vertical") == 0 && LookedUpOrDown)
        {
            
            Lerp();
            
        }
    }



    public void CallTurn()
    {
        //smooth when falling
        LeanTween.rotateY(gameObject, DetermineEndRotation(), FlipYRotationTime).setEaseInOutSine();
    }

    private float DetermineEndRotation()
    {
        //rotates with the player
        _isFacingRight = !_isFacingRight;
        //to right
        if(_isFacingRight)
        {
            return 0f;
        }
        //to left
        else
        {
            return 180f;
        }
    }
    public void LerpUp(float currentY) 
    {
        LerpElapsedTime = 0f;
        FollowPlayer = false;
        LerpElapsedTimeUpAndDown += Time.deltaTime;

        float percentageComplete = LerpElapsedTimeUpAndDown / LerpTimeDurationUpAndDown;

        float Up = Mathf.Lerp(transform.position.y,(currentY + heightAmount), curve.Evaluate(percentageComplete));

        transform.position = new Vector3(transform.position.x, Up);

        LookedUpOrDown = true;

    }
    public void LerpDown(float currentY)
    {
        LerpElapsedTime = 0f;
        FollowPlayer = false;
        LerpElapsedTimeUpAndDown += Time.deltaTime;

        float percentageComplete = LerpElapsedTimeUpAndDown / LerpTimeDurationUpAndDown;

        float Up = Mathf.Lerp(transform.position.y, (currentY - heightAmount), curve.Evaluate(percentageComplete));

        transform.position = new Vector3(transform.position.x, Up);

        LookedUpOrDown = true;

    }

    //method to make the object follow player after leaving a no move area
    public void Lerp()
    {

        LerpElapsedTimeUpAndDown = 0;
       LerpElapsedTime += Time.deltaTime;

        float percentageComplete = LerpElapsedTime / DesireLerpDuration;

        transform.position = Vector3.Lerp(transform.position, PlayerTransform.position, curve.Evaluate(percentageComplete));

        if (LerpElapsedTime >= 5f)
        {
            FollowPlayer = true;
            LookedUpOrDown = false;
        }
        
    }
}
