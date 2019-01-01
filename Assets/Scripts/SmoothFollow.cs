using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour
{
    public float smoothTime = 0.3f;

    private Transform thisTransform;
    public Transform target;

    void Start()
    {
        thisTransform = transform;
    }
    void Update()
    {
        thisTransform.LookAt(target);
    }
}