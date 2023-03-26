using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoYTigger : MonoBehaviour
{

    [Header("Box Cast Variables")]
    public Vector3 Area;//trigger area
    public bool Detected = false;// detecte if a enemy was inside
    public Transform Target; // Player transform
     Vector2 direction;


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
    public void Update()
    {
        Vector2 targetPos = Target.position;

        direction = targetPos - (Vector2)transform.position;

        //creates the box cast(trigger)
        RaycastHit2D BoxInfo = Physics2D.BoxCast(gameObject.GetComponent<Renderer>().bounds.center,Area,0f, Area);

        //checks if the player is inside the area
        if (BoxInfo.collider.gameObject.tag == "Player")
        {
            Detected = true;
            Debug.DrawLine(transform.position, targetPos);

            //do the lerp
            if(LerpElapsedTime < DesireLerpDuration)
            {
                Lerp();
            }
            

        }
        //checks if the player is out of the area
        else if (BoxInfo.collider.gameObject.tag != "Player" && Detected == true)
        {
            //go back to the normal values of the camera
            if (UnlerpElapsedTime < DesireLerpDuration)
            {
                UnLerp();
            }

        }
    }

    public void OnDrawGizmosSelected()
    {
        //Draw the box on unity
        Gizmos.DrawWireCube(gameObject.GetComponent<Renderer>().bounds.center, Area);

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

        if (Detected == true)
        {
            UnlerpElapsedTime += Time.deltaTime;

            float percentageComplete = UnlerpElapsedTime / DesireLerpDuration;

            FramingTransposer.m_SoftZoneHeight = Mathf.Lerp(YSoftZoneHeight, NormSoftZoneHeight, percentageComplete);

            FramingTransposer.m_DeadZoneHeight = Mathf.Lerp(YDeadZoneHeight, NormDeadZoneHeight, percentageComplete);

            FramingTransposer.m_ScreenY = Mathf.Lerp(YScreenY, NormScreenY, percentageComplete);

        }

    }
}





