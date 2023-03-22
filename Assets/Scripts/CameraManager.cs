using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [SerializeField] private CinemachineVirtualCamera[] _allVirtualCameras;

    [Header("Controls for lerping the Y damping during play jump/fall")]
    [SerializeField] private float _fallPanAmount = 0.25f;
    [SerializeField] private float _fallYPanTime = 0.35f;
    public float _fallSpeedYDapingChangeThreshold = -15f;

    public bool IsLerpingYDamping { get; private set; }

    public bool LerpedFromPlayerFalling { get; set; }

    private Coroutine _lerpYPanCoroutine;
    private Coroutine _panCameraCoroutine;

    private CinemachineVirtualCamera _currentCamera;
    private CinemachineFramingTransposer _framingTransposer;

    private float _normYpanAmount;

    private Vector2 _startingTrackedObjectOffset;

    private CameraControlTrigger CameraControlTrigger;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        for(int i = 0; i< _allVirtualCameras.Length; i++)
        {
            if (_allVirtualCameras[i].enabled)
            {
                //set the curent active camera
                _currentCamera = _allVirtualCameras[i];

                //set the framing transposer
                _framingTransposer = _currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            }
        }

        //Set the YDamping amount so it's based on the inspector value
        _normYpanAmount = _framingTransposer.m_YDamping;

        

        //Set the starting position of the trackedobject offset
        _startingTrackedObjectOffset = _framingTransposer.m_TrackedObjectOffset;
    }

    private void Update()
    {
        //Debug.Log(_normYpanAmount);
    }

    #region Lerp the Y Damping

    public void LerpYDamping(bool IsPlayerFalling)
    {
        _lerpYPanCoroutine = StartCoroutine(LerpYAction(IsPlayerFalling));
    }

    private IEnumerator LerpYAction(bool IsPlayerFalling)
    {
        IsLerpingYDamping = true;

        //grab the starting damping amount
        float startDampAmount = _framingTransposer.m_YDamping;
        float endDampAmount = 0f;

        //determine the ending damping amount
        if(IsPlayerFalling)
        {
            endDampAmount = _fallPanAmount;
            LerpedFromPlayerFalling= true;

        }
        else
        {
            endDampAmount = _normYpanAmount;
           // Debug.Log("voltei");
        }

        //lerp the pan amount
        float elapsedTime = 0f;
        while(elapsedTime < _fallPanAmount)
        {
            elapsedTime += Time.deltaTime;

            float lerpedPanAmount = Mathf.Lerp(startDampAmount, endDampAmount, (elapsedTime/ _fallYPanTime));
            _framingTransposer.m_YDamping= lerpedPanAmount;

            yield return null;
        }

        IsLerpingYDamping = false;
    }
    #endregion Lerp the Y Damping;

    #region Pan Camera

    public void PanCameraOnContact(float panDistance, float panTime, PanDirection panDirection, bool panToStartingPos)
    {
        _panCameraCoroutine = StartCoroutine(PanCamera(panDistance,panTime,panDirection,panToStartingPos));
    }

    private IEnumerator PanCamera(float panDistance, float panTime, PanDirection panDirection, bool panToStartingPos)
    {
        Vector2 endPos = Vector2.zero;
        Vector2 startingPos= Vector2.zero;
      
        //handle pan fromm trigger
        if(!panToStartingPos)
        {
            //set the direction and distance
            switch(panDirection)
            {
                case PanDirection.Up:
                    endPos = Vector2.up; break;

                case PanDirection.Down:
                    endPos = Vector2.down;break;

                case PanDirection.Right:
                    endPos = Vector2.left; break;

                case PanDirection.Left:
                    endPos = Vector2.right; break;
            }
            endPos *= panDistance;

            startingPos = _startingTrackedObjectOffset;

            endPos += startingPos;

            

        }
        //handle the pan back to starting position
        else
        {
            startingPos = _framingTransposer.m_TrackedObjectOffset;
            endPos = _startingTrackedObjectOffset;
        }
        //handle the actual panning of the camera
        float elapsedTime = 0f;
        while(elapsedTime < panTime)
        {
            elapsedTime += Time.deltaTime;

            Vector3 panLerp = Vector3.Lerp(startingPos,endPos, (elapsedTime/ panTime));
            _framingTransposer.m_TrackedObjectOffset = panLerp;

            yield return null;

        }
    }

    #endregion
}