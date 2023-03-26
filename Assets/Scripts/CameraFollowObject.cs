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

    //method to make the object follow player after leaving a no move area
    public void Lerp()
    {
        
        LerpElapsedTime += Time.deltaTime;

        float percentageComplete = LerpElapsedTime / DesireLerpDuration;

        transform.position = Vector3.Lerp(transform.position, PlayerTransform.position, curve.Evaluate(percentageComplete));

        if (LerpElapsedTime >= 5f)
        {
            FollowPlayer = true;
        }
        
    }
}
