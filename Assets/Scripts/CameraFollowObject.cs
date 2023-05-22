using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public GameObject PlayerObj;
    [HideInInspector] public PlayerMovement PlayerMovmentScript;
    public bool PlayerOnAir;
    public bool PlayerMoving;

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
    public bool CanLookUporDown = false;
    public float LerpTimeDurationUpAndDown = 3f;
    public float LerpElapsedTimeUpAndDown = 0f;
    public bool StopLerp = false;
    [SerializeField]
    private AnimationCurve curveUpAndDown;

    private float currentY;

    private CharacterController2D characterController2D;
    private void Start()
    {
        PlayerObj = GameObject.FindGameObjectWithTag("Player");

        PlayerMovmentScript = PlayerObj.GetComponent<PlayerMovement>();
        characterController2D = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>();
    }
    private void Awake()
    {
        PlayerCharacterController = PlayerObj.GetComponent<CharacterController2D>();

        _isFacingRight = PlayerCharacterController.m_FacingRight;
        transform.position = PlayerObj.transform.position;
    }

    private void Update()
    {
        PlayerOnAir = PlayerMovmentScript.OnAir;
        PlayerMoving = PlayerMovmentScript.moving;

        /*
        CanLookUporDown = true;
        if (Input.GetAxis("Vertical") > 0.9f && CanLookUporDown && Input.GetAxis("Horizontal") * 40 == 0f )
        {
            LerpUp(currentY);
            PlayerMovmentScript.enabled = false;

        }
        if (Input.GetAxis("Vertical") < -0.9f && CanLookUporDown && Input.GetAxis("Horizontal") * 40 == 0f)
        {

            LerpDown(currentY);
            PlayerMovmentScript.enabled = false;

        }
        if (Input.GetAxis("Vertical") == 0 && LookedUpOrDown)
        {

            Lerp();

        }
        else
        {
            CanLookUporDown = false;
        }
        */
        //make the object follow the player if he's not on a no move area
        if(FollowPlayer)
        {
            transform.position = PlayerObj.transform.position;
            currentY = transform.position.y;
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

        float Up = Mathf.Lerp(transform.position.y,(currentY + heightAmount), percentageComplete);

        transform.position = new Vector3(transform.position.x, Up);

        LookedUpOrDown = true;

    }
    public void LerpDown(float currentY)
    {
        LerpElapsedTime = 0f;
        FollowPlayer = false;
        LerpElapsedTimeUpAndDown += Time.deltaTime;

        float percentageComplete = LerpElapsedTimeUpAndDown / LerpTimeDurationUpAndDown;

        float Up = Mathf.Lerp(transform.position.y, (currentY - heightAmount), percentageComplete);

        transform.position = new Vector3(transform.position.x, Up);

        LookedUpOrDown = true;

    }

    //method to make the object follow player after leaving a no move area
    public void Lerp()
    {

       LerpElapsedTimeUpAndDown = 0;
       LerpElapsedTime += Time.deltaTime;

        float percentageComplete = LerpElapsedTime / DesireLerpDuration;

        transform.position = Vector3.Lerp(transform.position, PlayerObj.transform.position, percentageComplete);

        if (LerpElapsedTime >1.5f)
        {
            this.GetComponent<CameraFollowObject>().enabled = true;
            StopLerp = true;
            FollowPlayer = true;
            LookedUpOrDown = false;
            PlayerMovmentScript.enabled = true;
            //enables the camera to lerp in a forwarder distance in the horizontal  
            characterController2D.turn = true;
        }
        
    }
}
