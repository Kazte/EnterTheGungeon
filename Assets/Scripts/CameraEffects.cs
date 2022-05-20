using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraEffects : Singleton<CameraEffects>
{
    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private void Awake()
    {
        _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        FindPlayer();
    }

    public void ShakeCamera(float intensity, float time)
    {
        var basicMultiChannelPerlin =
            _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        basicMultiChannelPerlin.m_AmplitudeGain = intensity;
        Invoke("StopShakeCamera", time);
    }

    private void StopShakeCamera()
    {
        var basicMultiChannelPerlin =
            _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        basicMultiChannelPerlin.m_AmplitudeGain = 0;
    }


    /// FIND PLAYER \\\

    private void FindPlayer()
    {
        _cinemachineVirtualCamera.m_Follow = GameObject.FindWithTag("Player").transform;
    }
}
