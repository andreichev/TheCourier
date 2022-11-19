using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    private Transform target;
    public Transform finishTarget;
    private Transform lookAtTarget;
    public float followTime;

    [System.NonSerialized]
    public bool playerFinished = false;

    private Vector3 velocity = Vector3.zero;

    private bool initalized = false;

    public void myAwake() {
        target = GameObject.Find("CameraFollow").transform;
        lookAtTarget = GameObject.Find("LookAt").transform;
        initalized = true;
    }

    void FixedUpdate() {
        if (!initalized) return;

        Vector3 targetPos = playerFinished ? finishTarget.position : target.position;
        this.transform.position = Vector3.SmoothDamp(this.transform.position, targetPos, ref velocity, followTime);

        this.transform.LookAt(lookAtTarget);

        if (!playerFinished) {
            Vector3 currentRotation = this.transform.localRotation.eulerAngles;
            currentRotation.x = 40;
            this.transform.localRotation = Quaternion.Euler(currentRotation);
        }
    }
}
