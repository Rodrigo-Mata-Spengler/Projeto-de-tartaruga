using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform PlayerTransform;

    [Header("Flip rotation stats")]
    [SerializeField] private float FlipYRotationTime = -0.5f;

    private CharacterController2D PlayerCharacterController;

    private bool _isFacingRight;


    private void Awake()
    {
        PlayerCharacterController = PlayerTransform.GetComponent<CharacterController2D>();

        _isFacingRight = PlayerCharacterController.m_FacingRight;
    }

    private void Update()
    {
        transform.position = PlayerTransform.position;
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
            return 100f;
        }
        else
        {
            return 0f;
        }
    }
}
