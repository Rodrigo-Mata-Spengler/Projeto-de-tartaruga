using System.Collections;
using UnityEngine;

public enum PanDirection
{
    Up,
    Down,
    Left,
    Right
}
public class CameraControlTrigger : MonoBehaviour
{
    private Collider2D _coll;

    public bool Detected = false;// detecte if a enemy was inside
    public bool panCameraOnContact = false;

    public PanDirection panDirection;
    public float panDistance = 3f;
    public float panTime = 0.35f;
    private void Start()
    {
        _coll = GetComponent<Collider2D>();
    }
    private void FixedUpdate()
    {

        if (panCameraOnContact && Detected)
        {
            //pan the camera to the desire direction
            CameraManager.Instance.PanCameraOnContact(panDistance, panTime, panDirection, false);
        }

        if (panCameraOnContact && !Detected)
        {
            CameraManager.Instance.PanCameraOnContact(panDistance, panTime, panDirection, true);

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Detected = true;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Detected = false;
        }
    }
}
