using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPhysics : MonoBehaviour
{
    [System.NonSerialized]
    public bool isGrounded = true;

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        { 
            isGrounded = hit.distance < 1.5f;
        }
        else
        {
            isGrounded = false;
        }
    }
}
