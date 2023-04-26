using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleButtomScript : MonoBehaviour
{
    public bool PlayerDetected = false;

    [SerializeField] private Transform CircleToMove;
    [SerializeField] private Transform CircleToMove1;
    [SerializeField] private float AmountToRotate;
    [Space]
    float speed = 2f;
    [SerializeField]float timeCount = 0.0f;

    public bool rotates = false;

    public Transform TargetRotation;
    public Transform TargetRotation1;

    private void Update()
    {
        if(PlayerDetected && Input.GetButtonDown("Interacao") && !rotates)
        {
            //CircleToMove.Rotate(0f, 0f, AmountToRotate, Space.Self);
            rotates= true;
            
        }
        if (rotates)
        {
            CircleToMove.rotation = Quaternion.SlerpUnclamped(CircleToMove.rotation, TargetRotation.rotation, 0.013f);
            if(CircleToMove1 != null)
            {
                CircleToMove1.rotation = Quaternion.SlerpUnclamped(CircleToMove1.rotation, TargetRotation1.rotation, 0.013f);
            }
            

            timeCount += Time.deltaTime;

            if(timeCount > 4)
            {
                TargetRotation.Rotate(0f, 0f, AmountToRotate, Space.Self);
                if(TargetRotation1 != null)
                {
                    TargetRotation1.Rotate(0f, 0f, AmountToRotate, Space.Self);
                }
                
                timeCount = 0;
                rotates = false;
            }
        }
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerDetected= true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerDetected= false;
        }
    }
}
