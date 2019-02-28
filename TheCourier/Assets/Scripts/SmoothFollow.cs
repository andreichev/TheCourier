using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour
{
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