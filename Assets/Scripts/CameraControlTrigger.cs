using System.Collections;
using UnityEngine;


public class CameraControlTrigger : MonoBehaviour
{
    public CustomInspectorObjects customInspectorObjects;
    private Collider2D _coll;

    public bool Detected = false;// detecte if a enemy was inside

    public bool Down = false; // the direction the camera should go

    private void Start()
    {
        _coll = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Detected = true;

            if (customInspectorObjects.panCameraOnContact)
            {
                //pan the camera to the desire direction
                CameraManager.Instance.PanCameraOnContact(customInspectorObjects.panDistance, customInspectorObjects.panTime, customInspectorObjects.panDirection, false);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (customInspectorObjects.panCameraOnContact)
            {
                CameraManager.Instance.PanCameraOnContact(customInspectorObjects.panDistance, customInspectorObjects.panTime, customInspectorObjects.panDirection, true);

                StartCoroutine(UnlerEdge(customInspectorObjects.panTime));
            }
        }
    }

    IEnumerator UnlerEdge(float customInspectorObjectspanTime)
    {
        yield return new WaitForSeconds(customInspectorObjectspanTime);
        Detected = false;
    }
}

[System.Serializable]
public class CustomInspectorObjects
{
    //Lerp the camera values

    public bool panCameraOnContact = false;

     public PanDirection panDirection;
     public float panDistance = 3f;
     public float panTime = 0.35f;
}

public enum PanDirection
{
    Up,
    Down,
    Left,
    Right
}