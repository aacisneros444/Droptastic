using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    [Header("References")]
    public CinemachineVirtualCamera virtualCamera;

    [Header("Shake Parameters")]
    private float shakeTimer;
    private float shakeTimeTotal;
    private float startingIntensity;
    private const float intensity = 0.4f;
    private const float shakeTime = 0.25f;

    [Header("Lerp To Origin Paramters")]
    private Vector3 origin;
    private float resetTimer;
    private const float resetTimerTotal = 0.5f;

    private void Awake()
    {
        Instance = this;
        origin = transform.position;
    }

    public void ShakeCamera()
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        shakeTimer = shakeTime;
        shakeTimeTotal = shakeTime;
        startingIntensity = intensity;
    }

    private void Update()
    {
        if (shakeTimer > 0f)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                    virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain =
                    Mathf.Lerp(startingIntensity, 0f, 1 - (shakeTimer / shakeTimeTotal));

                resetTimer = resetTimerTotal;
            }
        }

        if (resetTimer > 0f)
        {
            resetTimer -= Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, origin, 1 - (resetTimer / resetTimerTotal));
        }
    }

}
