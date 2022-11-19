using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCamera : MonoBehaviour {

    private Transform camera1Pos;
    private Transform camera2Pos;

    private int currentPos = 0; //initially at car 1
    private int carsCount = 2;

    private Button buttonNextCar;

    private Vector3 velocity;

    void Start() {
        camera1Pos = GameObject.Find("Car1CameraPos").transform;
        camera2Pos = GameObject.Find("Car2CameraPos").transform;

        buttonNextCar = GameObject.Find("ButtonChangeCar").GetComponent<Button>();
        buttonNextCar.onClick.AddListener(Right);
    }

    void Update() {
        if (currentPos == 0) {
            transform.position = Vector3.SmoothDamp(transform.position, camera1Pos.position, ref velocity, 0.3f);
        } else {
            transform.position = Vector3.SmoothDamp(transform.position, camera2Pos.position, ref velocity, 0.3f);
        }
    }

    void Right() {
        currentPos++;

        if (currentPos == carsCount)
            currentPos = 0;

        LevelParams.carIndex = currentPos;
    }

    void Left() {
        currentPos--;

        if (currentPos == -1)
            currentPos = carsCount - 1;

        LevelParams.carIndex = currentPos;
    }
}