using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Threading;

public class CameraControlTrigger : MonoBehaviour
{
    public CustomInspectorObjects customInspectorObjects;
    private Collider2D _coll;

    //Edge detection
    [Header("BoxCast, Edge detection")]
    public Vector2 Area;//trigger area
    public bool Detected = false;// detecte if a enemy was inside
    Vector2 direction;
    public Transform Target;// Player transform


    [HideInInspector]public bool Down = false; // the direction the camera should go

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

        //creates the box cast(trigger)
        RaycastHit2D BoxInfo = Physics2D.BoxCast(transform.position, Area, 0f, direction);

        //checks if the player is inside the area
        if (BoxInfo.collider.gameObject.tag == "Player")
        {
            Detected = true;
            Debug.DrawLine(transform.position, targetPos);

            if (customInspectorObjects.panCameraOnContact )
            {
                //pan the camera to the desire direction
                CameraManager.Instance.PanCameraOnContact(customInspectorObjects.panDistance, customInspectorObjects.panTime, customInspectorObjects.panDirection, false);   
            }

        }
        //checks if the player is out of the area
        if (BoxInfo.collider.gameObject.tag != "Player" && Detected == true)
        {
            //return the camera to the normal position 
            if (customInspectorObjects.panCameraOnContact)
            {
               CameraManager.Instance.PanCameraOnContact(customInspectorObjects.panDistance, customInspectorObjects.panTime, customInspectorObjects.panDirection, true);

                StartCoroutine(UnlerEdge(customInspectorObjects.panTime));
            }

        }
    
            //pan the camera

    }

    //desenha o quadrado
    public void OnDrawGizmosSelected()
    {
        //Draw the box on unity
        Gizmos.DrawWireCube(transform.position, Area);
    }

    IEnumerator UnlerEdge(float customInspectorObjectspanTime)
    {
        yield return new WaitForSeconds(customInspectorObjectspanTime);
        Detected = false;
    }

    /*
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
    */
}
    
[System.Serializable]

public class CustomInspectorObjects
{
    //Lerp the camera values
 
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