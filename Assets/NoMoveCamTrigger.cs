using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoMoveCamTrigger : MonoBehaviour
{
    [Header("Box Cast Variables")]
    public Vector3 Area;
    public bool Detected = false;
    Vector2 direction;
    public Transform Target;



    public Transform CameraFollowObject;
    private CameraFollowObject CameraFollowObjectScript;

    [Header("render camera")]
    public CinemachineVirtualCamera CinemachineCamera;
    private CinemachineFramingTransposer FramingTransposer;
    [SerializeField] private CinemachineConfiner2D CinemachineConfiner2D;

    [Header("distancia que a camera vai se mover, para cima ou baixo")]
    public float CameraYMov;




    public float DesireLerpDuration = 3f;
    public float LerpElapsedTime;
    public float UnlerpElapsedTime;

    [SerializeField]
    private AnimationCurve curve;




    private void Start()
    {
   
        FramingTransposer = CinemachineCamera.GetCinemachineComponent<CinemachineFramingTransposer>();


        CameraFollowObjectScript = CameraFollowObject.GetComponent<CameraFollowObject>();
    }
    public void Update()
    {
        Vector2 targetPos = Target.position;

        direction = targetPos - (Vector2)transform.position;

        RaycastHit2D BoxInfo = Physics2D.BoxCast(transform.position, Area, 0f, direction);

        //checa se o player entrou no quadrado 
        if (BoxInfo.collider.gameObject.tag == "Player")
        {
            Detected = true;
            Debug.DrawLine(transform.position, targetPos);

            if (LerpElapsedTime < DesireLerpDuration)
            {
                Lerp();
            }


        }
        else if (BoxInfo.collider.gameObject.tag != "Player" && Detected == true)
        {
            if (UnlerpElapsedTime < DesireLerpDuration)
            {
                UnLerp();
            }






        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, Area);

    }

    private void Lerp()
    {
        CameraFollowObjectScript.FollowPlayer = false;

        CameraFollowObjectScript.enabled = false;
        UnlerpElapsedTime = 0;


        LerpElapsedTime += Time.deltaTime;

        float percentageComplete = LerpElapsedTime / DesireLerpDuration;


        CameraFollowObject.position = Vector3.Lerp(CameraFollowObject.position, transform.position, curve.Evaluate(percentageComplete));



        FramingTransposer.m_TrackedObjectOffset =new Vector3 (0f, 0f, 0f);
        FramingTransposer.m_TargetMovementOnly = false;
        CinemachineConfiner2D.enabled = false;

        LerpElapsedTime += Time.deltaTime;


    }

    private void UnLerp()
    {
        LerpElapsedTime = 0f;
   

        if (Detected == true)
        {

            CameraFollowObjectScript.enabled = true;

            CameraFollowObjectScript.Lerp();

            UnlerpElapsedTime += Time.deltaTime;

            float percentageComplete = UnlerpElapsedTime / DesireLerpDuration;

            FramingTransposer.m_TrackedObjectOffset = new Vector3(1f, 0f, 0f);
            FramingTransposer.m_TargetMovementOnly = true;
            CinemachineConfiner2D.enabled = false;
        }



    }
}