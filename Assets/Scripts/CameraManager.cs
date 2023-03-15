using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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

    private CinemachineVirtualCamera _currentCamera;
    private CinemachineFramingTransposer _framingTransposer;

    private float _normYpanAmount;

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

        //Se the YDamping amount so it's based on the inspector value
        _normYpanAmount = _framingTransposer.m_YDamping;
    }


    //region Lerp the Y Damping

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
}