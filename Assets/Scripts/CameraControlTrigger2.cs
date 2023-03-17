using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Device;

public class CameraControlTrigger2 : MonoBehaviour
{
    public CinemachineVirtualCamera CinemachineCamera;


    [Header("Witch tipe of camera is been used")]
    public bool NormalCamera;
    public bool NoYMove;
    public bool CenteredCamera;


    [Space]
    [Header("Cinemachine Variables values")]
    public float ScreenY;
    public float SoftZoneHeight;
    public Transform FollowTransform;
    public Vector3 TrackedObjectOffset;
    public bool TargetMovementOnly;




    private void Start()
    {
        ScreenY = CinemachineCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY;
        SoftZoneHeight = CinemachineCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_SoftZoneHeight;

        FollowTransform = CinemachineCamera.Follow;

        TrackedObjectOffset = CinemachineCamera.GetCinemachineComponent<CinemachineFramingTransposer>().TrackedPoint;

        TargetMovementOnly = CinemachineCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TargetMovementOnly;

        NormalCamera = true;
    }




    public void NormalCam(Transform ObjectFollow, float trackedObjectOffset, bool targetMovementOnly)
    {
        ScreenY = 0.5f;
        SoftZoneHeight = 0.35f;
        TrackedObjectOffset = new Vector3(trackedObjectOffset, 0f, 0f);

        NormalCamera = true;

        TargetMovementOnly = true;

        



    }


    public void NoYMovement()
    {
        ScreenY = 1.5f;
        SoftZoneHeight = 2;

        NoYMove = true;
        NormalCamera = false;


        PassTheValuesToCinemachineCamera(ScreenY, SoftZoneHeight);
    }


    public void PassTheValuesToCinemachineCamera( float ScreenY, float SoftZoneHeight )
    {
        CinemachineCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY = ScreenY;
        CinemachineCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_SoftZoneHeight = SoftZoneHeight;
    }



}
