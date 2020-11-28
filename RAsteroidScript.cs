using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RAsteroidScript : MonoBehaviour
{
    public bool Hit = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Shot")
        {
            Hit = true;
        }
    }
}
