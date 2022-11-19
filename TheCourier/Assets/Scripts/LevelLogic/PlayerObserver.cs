using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObserver : MonoBehaviour
{
    private GameObject finishedGreeting;
    private Transform startPoint;
    private CarConroller carConroller;
    private CameraFollow cameraFollow;

    private bool finished = false;

    private void Start() {
        startPoint = GameObject.Find("StartPoint").transform;
        finishedGreeting = GameObject.Find("MessagePlayerFinished");
        carConroller = this.GetComponent<CarConroller>();
        cameraFollow = GameObject.FindObjectOfType<CameraFollow>();

        finishedGreeting.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) {
        if(finished) {
            return;
        }

        if (other.tag == "WrongWay") {
            moveToStartPoint();
        } else if (other.name == "Finish") {
            cameraFollow.playerFinished = true;
            finishedGreeting.SetActive(true);
            GameObject.FindObjectOfType<PauseMenu>().playerFinished();
            finished = true;
        }
    }

    public void moveToStartPoint() {
        this.transform.position = startPoint.position;
        this.transform.rotation = startPoint.rotation;
        carConroller.resetValues();
    }
}
