using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostRotation : MonoBehaviour
{
    public float rotationSpeed = 1f;
    private void Update()
    {
        transform.Rotate(new Vector3(rotationSpeed, rotationSpeed, 0));
    }
}
