using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour
{
    public Transform camera3D;
    public GameObject CameraIsHere;
    public GameObject Objective2;
    public Rigidbody ShipRb;
    public PlayerController PlayerCont;
    public GameObject RAS;

    public float speed = 1f;

    public bool Pan = false;
    public bool Panned = false;
    private void LateUpdate()
    {
        if (PlayerCont.FireRate >= 4)
        {
            Pan = true;
            Panned = true;
        }
        if (Pan)
        {
            transform.position = Vector3.Slerp(transform.position, camera3D.position, speed * Time.deltaTime);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, camera3D.localRotation, speed * Time.deltaTime);
            ShipRb.constraints = RigidbodyConstraints.None;
            ShipRb.constraints = RigidbodyConstraints.FreezeRotation;
            RAS.SetActive(true);
            
            if (Mathf.Abs(transform.position.y - camera3D.position.y) <= 0.01f)
            {
                transform.position = camera3D.position;
                transform.localRotation = camera3D.localRotation;
                CameraIsHere.SetActive(true);
                Pan = false;
                Objective2.SetActive(true);
                gameObject.SetActive(false);

            }
        }
    }
}
