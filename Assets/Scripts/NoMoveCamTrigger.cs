using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NoMoveCamTrigger : MonoBehaviour
{
    public bool Detected = false;// detecte if playe was inside

    public Transform CameraFollowObject;
    private CameraFollowObject CameraFollowObjectScript;

    [Header("render camera")]
    public CinemachineVirtualCamera CinemachineCamera;
    private CinemachineFramingTransposer FramingTransposer;
    [SerializeField] private CinemachineConfiner2D CinemachineConfiner2D;

    [Header("distancia que a camera vai se mover, para cima ou baixo")]
    public float CameraYMov;// distance that the camera go up or down


    public float DesireLerpDuration;//the duration of the lerp
    public float LerpElapsedTime;// the current duration of the lerp
    public float UnlerpElapsedTime;// the current duration of the Unlerp

    [SerializeField]
    private AnimationCurve curve;

    private void Start()
    {
   
        FramingTransposer = CinemachineCamera.GetCinemachineComponent<CinemachineFramingTransposer>();


        CameraFollowObjectScript = CameraFollowObject.GetComponent<CameraFollowObject>();
    }
    public void Update()
    {

        if(Detected)
        {
            if (LerpElapsedTime <1.5f)
            {
                Lerp();
            }

        }
        if (!Detected)
        {
            if (CameraFollowObjectScript.LerpElapsedTime <= CameraFollowObjectScript.DesireLerpDuration && CameraFollowObjectScript.StopLerp == false)
            {
                UnLerp();
                //makes the camera follow object start to follow the player again
                CameraFollowObjectScript.Lerp();


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
        CameraFollowObjectScript.StopLerp = false;
        //makes the camera follow object go to the center of the area and changes some values
        CameraFollowObjectScript.FollowPlayer = false;

        CameraFollowObjectScript.enabled = false;
        UnlerpElapsedTime = 0;
        CameraFollowObjectScript.LerpElapsedTime = 0f;


        LerpElapsedTime += Time.deltaTime;

        float percentageComplete = LerpElapsedTime / DesireLerpDuration;


        CameraFollowObject.position = Vector3.Lerp(CameraFollowObject.position, transform.position, percentageComplete);

        FramingTransposer.m_TrackedObjectOffset =new Vector3 (0f, 0f, 0f);
        FramingTransposer.m_TargetMovementOnly = false;
        //CinemachineConfiner2D.enabled = false;

        LerpElapsedTime += Time.deltaTime;


    }

    private void UnLerp()
    {
        LerpElapsedTime = 0f;
   
        //change back the values to normal
        if (Detected == true)
        {

            CameraFollowObjectScript.enabled = true;

            UnlerpElapsedTime += Time.deltaTime;

            float percentageComplete = UnlerpElapsedTime / DesireLerpDuration;

            FramingTransposer.m_TrackedObjectOffset = new Vector3(1f, 0f, 0f);
            FramingTransposer.m_TargetMovementOnly = true;
            //CinemachineConfiner2D.enabled = true;

            //StartCoroutine(DetectedFalse(CameraFollowObjectScript.DesireLerpDuration));
            //StopCoroutine(DetectedFalse(0));
        }

    }

}
