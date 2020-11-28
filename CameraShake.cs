using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class CameraShake : MonoBehaviour
{
    public float magnitude = 1f;
    public float roughness = 1f;
    public float fadeInTime = 0.1f;
    public float fadeOutTime = 1f;

    public CameraShaker camShaker;
    /*void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            camShaker.ShakeOnce(magnitude, roughness, fadeInTime, fadeOutTime);
        }
    }*/
    public void Shake()
    {
        camShaker.ShakeOnce(magnitude, roughness, fadeInTime, fadeOutTime);
    }
}
