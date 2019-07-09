using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{

    CinemachineVirtualCamera vcam;
    CinemachineBasicMultiChannelPerlin noise;

    void Start()
    {
        vcam = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Noise(float amplitudeGain, float frequencyGain)
    {
        noise.m_AmplitudeGain = amplitudeGain;
        noise.m_FrequencyGain = frequencyGain;
    }

    public void Shake(float shakeIntensity = 5f, float shakeAmplitude = 1f, float shakeTiming = 0.5f)
    {
        StartCoroutine(ShakeCamera(shakeIntensity, shakeAmplitude, shakeTiming));
    }

    private IEnumerator ShakeCamera(float shakeIntensity = 5f, float shakeAmplitude = 1f, float shakeTiming = 0.5f)
    {
        Noise(shakeAmplitude, shakeIntensity);
        yield return new WaitForSeconds(shakeTiming);
        Noise(0, 0);
    }

}
