using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoYTigger : MonoBehaviour
{
    public bool Detected = false;// detecte if a enemy was inside


    [Header("render camera")]
    public CinemachineVirtualCamera CinemachineCamera; // cinemachine camera
    private CinemachineFramingTransposer FramingTransposer; 

    [Header("distancia que a camera vai se mover, para cima ou baixo")]
    public float CameraYMov; // distance that the camera go up or down


    [Header("normal camera values")] // the normal values of the camera
    private float NormSoftZoneHeight = 0.35f;
    private float NormDeadZoneHeight = 0;
    private float NormScreenY = 0.5f;

    
    [Header("No y camera values")]// the values when enter the no Y area
    private float YSoftZoneHeight = 1.5f;
    private float YDeadZoneHeight = 1.5f;
    private float YScreenY;


    public float DesireLerpDuration = 3f;//the duration of the lerp
    public float LerpElapsedTime; // the current duration of the lerp
    public float UnlerpElapsedTime; // the current duration of the Unlerp

    [SerializeField]
    private AnimationCurve curve;

    private void Start()
    {
        YScreenY += CameraYMov;
        FramingTransposer = CinemachineCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }
    private void Update()
    {
        if(Detected)
        {
            //do the lerp
            if (LerpElapsedTime < DesireLerpDuration)
            {
                Lerp();
            }

        }
        if(!Detected)
        {
            if (UnlerpElapsedTime < DesireLerpDuration)
            {
                UnLerp();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            Detected = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Detected = true;
        }
    }

    private void Lerp()
    {
        //Lerp the values from normal to no Y movment values
        UnlerpElapsedTime = 0;
        
            LerpElapsedTime += Time.deltaTime;

            float percentageComplete = LerpElapsedTime / DesireLerpDuration;

            FramingTransposer.m_SoftZoneHeight = Mathf.Lerp(NormSoftZoneHeight, YSoftZoneHeight, percentageComplete);

            FramingTransposer.m_DeadZoneHeight = Mathf.Lerp(NormDeadZoneHeight, YDeadZoneHeight, percentageComplete);

            FramingTransposer.m_ScreenY = Mathf.Lerp(NormScreenY, YScreenY, percentageComplete);
        
    }

    private void UnLerp()
    {
        //Lerp the values from no Y movment values to normal values
        LerpElapsedTime = 0f;

        UnlerpElapsedTime += Time.deltaTime;

        float percentageComplete = UnlerpElapsedTime / DesireLerpDuration;

        FramingTransposer.m_SoftZoneHeight = Mathf.Lerp(YSoftZoneHeight, NormSoftZoneHeight, percentageComplete);

        FramingTransposer.m_DeadZoneHeight = Mathf.Lerp(YDeadZoneHeight, NormDeadZoneHeight, percentageComplete);

        FramingTransposer.m_ScreenY = Mathf.Lerp(YScreenY, NormScreenY, percentageComplete);

    }
}





