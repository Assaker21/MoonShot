using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collTest : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Coll");
    }
}
