using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor;
using System.Threading;

public class CameraControlTrigger : MonoBehaviour
{
    public CustomInspectorObjects customInspectorObjects;

    private Collider2D _coll;


    [Header("BoxCast")]
    public Vector3 Area;
    public bool Detected = false;
    Vector2 direction;
    public Transform Target;


    public bool Down = false;

    private void Start()
    {
        _coll = GetComponent<Collider2D>();
    }

    private void Update()
    {
        BoxCast();
    }

    //Edge detection box cast
    public void BoxCast()
    {
        Vector2 targetPos = Target.position;

        direction = targetPos - (Vector2)transform.position;

        RaycastHit2D BoxInfo = Physics2D.BoxCast(transform.position, Area, 0f, direction);

        //checa se o player entrou no quadrado e leva a camera para baixo
        if (BoxInfo.collider.gameObject.tag == "Player")
        {
            Detected = true;
            Debug.DrawLine(transform.position, targetPos);

            if (customInspectorObjects.panCameraOnContact )
            {
                //pan the camera
                CameraManager.Instance.PanCameraOnContact(customInspectorObjects.panDistance, customInspectorObjects.panTime, customInspectorObjects.panDirection, false);

                
            }

        }
        //volta a camera para a posição inicial
        else if(Detected == true)
        {

            if (customInspectorObjects.panCameraOnContact)
            {
                //pan the camera
                CameraManager.Instance.PanCameraOnContact(customInspectorObjects.panDistance, customInspectorObjects.panTime, customInspectorObjects.panDirection, true);
                Detected = false;
            }
        }
    }

    //desenha o quadrado
    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, Area);
    
    }

    #region old detection
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(customInspectorObjects.panCameraOnContact)
            {
                //pan the camera
                CameraManager.Instance.PanCameraOnContact(customInspectorObjects.panDistance, customInspectorObjects.panTime, customInspectorObjects.panDirection, false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            

            if (customInspectorObjects.panCameraOnContact)
            {
                //pan the camera
                CameraManager.Instance.PanCameraOnContact(customInspectorObjects.panDistance, customInspectorObjects.panTime, customInspectorObjects.panDirection, true);
            }
        }
    }
    #endregion
}

[System.Serializable]

public class CustomInspectorObjects
{
 
    public bool panCameraOnContact = false;

    [HideInInspector] public PanDirection panDirection;
    [HideInInspector] public float panDistance = 3f;
    [HideInInspector] public float panTime = 0.35f;
}

public enum PanDirection
{
    Up,
    Down,
    Left,
    Right
}

[CustomEditor(typeof(CameraControlTrigger))]

public class MyScriptEditor : Editor
{
    CameraControlTrigger cameraControlTrigger;

    private void OnEnable()
    {
        cameraControlTrigger = (CameraControlTrigger)target;

    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (cameraControlTrigger.customInspectorObjects.panCameraOnContact)
        {
            cameraControlTrigger.customInspectorObjects.panDirection = (PanDirection)EditorGUILayout.EnumPopup("Camera Pan Direction",cameraControlTrigger.customInspectorObjects.panDirection);

            cameraControlTrigger.customInspectorObjects.panDistance = EditorGUILayout.FloatField("Pan Distance", cameraControlTrigger.customInspectorObjects.panDistance);
            cameraControlTrigger.customInspectorObjects.panTime = EditorGUILayout.FloatField("Pan Time", cameraControlTrigger.customInspectorObjects.panTime);

        }
        
        if (GUI.changed)
        {
            EditorUtility.SetDirty(cameraControlTrigger);
        }

    }
}