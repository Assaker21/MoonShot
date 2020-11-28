using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;
    public float minimumX = -360F;
    public float maximumX = 360F;
    public float minimumY = -60F;
    public float maximumY = 60F;
    public float SmoothnessOfRotation = 1f;
    public float BackSpeed = 1f;
    public float SmoothnessOfAcceleration = 0.5f;
    float rotationX = 0F;
    float rotationY = 0F;
    Quaternion xQuaternion;
    Quaternion yQuaternion;
    Quaternion originalRotation;
    Vector3 velocity = Vector3.zero;
    Vector3 previous = Vector3.zero;
    public GameObject Player;

    void LateUpdate()
    {
        velocity = (transform.position - previous) / Time.deltaTime;
        previous = transform.position;
        transform.position = Vector3.SmoothDamp(transform.position, Player.transform.position, ref velocity, SmoothnessOfAcceleration * Time.smoothDeltaTime);
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            if (axes == RotationAxes.MouseXAndY)
            {
                // Read the mouse input axis
                rotationX += Input.GetAxis("Mouse X") * sensitivityX;
                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                //rotationX = ClampAngle(rotationX, minimumX, maximumX);
                //rotationY = ClampAngle(rotationY, minimumY, maximumY);
                xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
                yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, originalRotation * xQuaternion * yQuaternion, SmoothnessOfRotation * Time.smoothDeltaTime);
            }
            else if (axes == RotationAxes.MouseX)
            {
                rotationX += Input.GetAxis("Mouse X") * sensitivityX;
                rotationX = ClampAngle(rotationX, minimumX, maximumX);
                xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
                transform.localRotation = originalRotation * xQuaternion;
            }
            else
            {
                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationY = ClampAngle(rotationY, minimumY, maximumY);
                yQuaternion = Quaternion.AngleAxis(-rotationY, Vector3.right);
                transform.localRotation = originalRotation * yQuaternion;
            }
        }
        else
        {        
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    void Start()
    {
        originalRotation = transform.localRotation;
    }
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);

    }
}
