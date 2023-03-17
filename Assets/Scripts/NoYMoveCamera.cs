using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoYMoveCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public CameraControlTrigger2 CameraControlTrigger2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            CameraControlTrigger2.NoYMovement();
        }
    }
}
