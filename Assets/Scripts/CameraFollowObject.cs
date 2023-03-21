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



    private void Start()
    {
        
    }
    private void Awake()
    {
        PlayerCharacterController = PlayerTransform.GetComponent<CharacterController2D>();

        _isFacingRight = PlayerCharacterController.m_FacingRight;
        transform.position = PlayerTransform.position;
    }

    private void Update()
    {
        if(FollowPlayer)
        {
            transform.position = PlayerTransform.position;
        }
        
        

    }
    public void CallTurn()
    {
        
        LeanTween.rotateY(gameObject, DetermineEndRotation(), FlipYRotationTime).setEaseInOutSine();
    }

    private float DetermineEndRotation()
    {
        _isFacingRight = !_isFacingRight;

        if(_isFacingRight)
        {
            return 0f;
        }
        else
        {
            return 180f;
        }
    }


    public void Lerp()
    {
        
        LerpElapsedTime += Time.deltaTime;

        float percentageComplete = LerpElapsedTime / DesireLerpDuration;

        transform.position = Vector3.Lerp(transform.position, PlayerTransform.position, curve.Evaluate(percentageComplete));

        if (percentageComplete >= DesireLerpDuration)
        {
            FollowPlayer = true;
        }
        
    }
}
