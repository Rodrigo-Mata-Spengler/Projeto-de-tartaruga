using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoYTigger : MonoBehaviour
{

    [Header("Box Cast Variables")]
    public Vector3 Area;
    public bool Detected = false;
    Vector2 direction;
    public Transform Target;


    [Header("render camera")]
    public CinemachineVirtualCamera CinemachineCamera;
    private CinemachineFramingTransposer FramingTransposer;

    [Header("distancia que a camera vai se mover, para cima ou baixo")]
    public float CameraYMov;


    [Header("normal camera values")]
    private float NormSoftZoneHeight = 0.35f;
    private float NormDeadZoneHeight = 0;
    private float NormScreenY = 0.5f;

    
    [Header("No y camera values")]
    private float YSoftZoneHeight = 2f;
    private float YDeadZoneHeight = 2f;
    private float YScreenY;

    public float DesireLerpDuration = 3f;
    public float LerpElapsedTime;
    public float UnlerpElapsedTime;

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

        RaycastHit2D BoxInfo = Physics2D.BoxCast(transform.position,Area,0f, direction);

        //checa se o player entrou no quadrado 
        if (BoxInfo.collider.gameObject.tag == "Player")
        {
            Detected = true;
            Debug.DrawLine(transform.position, targetPos);

            if(LerpElapsedTime < DesireLerpDuration)
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
        UnlerpElapsedTime = 0;

       
        
            LerpElapsedTime += Time.deltaTime;

            float percentageComplete = LerpElapsedTime / DesireLerpDuration;

            FramingTransposer.m_SoftZoneHeight = Mathf.Lerp(NormSoftZoneHeight, YSoftZoneHeight, percentageComplete);

            FramingTransposer.m_DeadZoneHeight = Mathf.Lerp(NormDeadZoneHeight, YDeadZoneHeight, percentageComplete);

            FramingTransposer.m_ScreenY = Mathf.Lerp(NormScreenY, YScreenY, percentageComplete);
        

     
    }

    private void UnLerp()
    {
        LerpElapsedTime = 0f;
        Debug.Log("un lerp");

        if (Detected == true)
        {
            UnlerpElapsedTime += Time.deltaTime;

            float percentageComplete = UnlerpElapsedTime / DesireLerpDuration;

            FramingTransposer.m_SoftZoneHeight = Mathf.Lerp(YSoftZoneHeight, NormSoftZoneHeight, percentageComplete);

            FramingTransposer.m_DeadZoneHeight = Mathf.Lerp(YDeadZoneHeight, NormDeadZoneHeight, percentageComplete);

            FramingTransposer.m_ScreenY = Mathf.Lerp(YScreenY, NormScreenY, percentageComplete);

            
            Debug.Log("To saindo");
        }
        
        

    }
}





