using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour
{
    public Transform target;
    public Transform lookAtTarget;
    public float smoothTime = 0.3F;

    private Transform thisTransform;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        thisTransform = transform;
    }

    void FixedUpdate()
    {
        // Smoothly move the camera towards that target position
        thisTransform.position = Vector3.SmoothDamp(thisTransform.position, target.position, ref velocity, smoothTime);
        thisTransform.LookAt(lookAtTarget);
    }
}