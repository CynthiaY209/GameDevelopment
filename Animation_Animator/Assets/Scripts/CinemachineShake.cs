using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; }

    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float shakeTimer;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        Instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMutiChannelPerlin =
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMutiChannelPerlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
       // Debug.Log("c");
       // Debug.Log(cinemachineBasicMutiChannelPerlin.m_AmplitudeGain);
    }

    // Update is called once per frame
    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                Debug.Log('b');
                CinemachineBasicMultiChannelPerlin cinemachineBasicMutiChannelPerlin =
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cinemachineBasicMutiChannelPerlin.m_AmplitudeGain = 0f;
            }
        }
    }
}
