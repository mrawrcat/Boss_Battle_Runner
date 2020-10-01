using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class CamShake : MonoBehaviour
{

    public float shakeDuration;
    public float shakeAmplitude;
    public float shakeFrequency;

    private float shakeElapsed = 0;
    
    public CinemachineVirtualCamera vCam;
    private CinemachineBasicMultiChannelPerlin vCamNoise;
    //private GameHelper helper;
    // Start is called before the first frame update
    void Start()
    {
        //helper = FindObjectOfType<GameHelper>();
        //vCam = helper.vCams.GetComponent<CinemachineVirtualCamera>();
        vCamNoise = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

   
}

    // Update is called once per frame
    void Update()
    {
        
        //vCam = GetComponent<CinemachineVirtualCamera>();
        //vCamNoise = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        //if(vCam != null || vCamNoise != null)
        //{
        if (shakeElapsed > 0)
        {
            vCamNoise.m_AmplitudeGain = shakeAmplitude;
            vCamNoise.m_FrequencyGain = shakeFrequency;

            shakeElapsed -= Time.deltaTime;
        }
        else
        {
            vCamNoise.m_AmplitudeGain = 0;
        }
        //}
    }

    public void Shake()
    {
        shakeElapsed = shakeDuration;
    }
}
