using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelGenerator : MonoBehaviour {
    public Area[] prefabsLeft;
    public Area[] prefabsRight;
    public Area[] prefabsForward;

    public GameObject[] carPrefab;

    public RawImage mapUI;
    public RawImage originalRawImage;
    public Button buttonPlay;
    public Button buttonPause;

    public Transform startPoint;

    public GameObject playerFinishedPrefab;

    private Vector3 currentPostion = Vector3.zero; //позиция, в которой спавн
    private Vector3 rotation = Vector3.zero;
    private int balance = 0;

    void Start() {
        Time.timeScale = 0;
        buttonPlay.onClick.AddListener(playClick);
        var currentDirection = AreaDirection.FORWARD;
        Vector3 directionVector = Vector3.zero; //вектор направления
        Vector3 uiPosition = Vector3.zero;
        Vector3 uiDirectionVector = Vector3.zero;
        Vector3 uiRotation = Vector3.zero;
        int index;
        int turnsCounter = LevelParams.countOfTurns;

        while (turnsCounter > 0) {
            if(turnsCounter == 1) {
                Instantiate(playerFinishedPrefab, currentPostion, Quaternion.Euler(rotation));
            }

            if (currentDirection == AreaDirection.FORWARD) {
                index = Random.Range(0, prefabsForward.Length);
                Area areaToGenerate = prefabsForward[index];

                Instantiate(areaToGenerate.gameObject, currentPostion, Quaternion.Euler(rotation)); //создает
                var map = Instantiate(originalRawImage, uiPosition, Quaternion.Euler(uiRotation), mapUI.transform);
                map.texture = areaToGenerate.map;
                map.SetNativeSize();
                map.name = "F";

                directionVector = Quaternion.Euler(rotation) * Vector3.forward;
                currentPostion += directionVector * areaToGenerate.length;

                uiDirectionVector = Quaternion.Euler(uiRotation) * Vector3.up;
                uiPosition += uiDirectionVector * 300;
            } else if (currentDirection == AreaDirection.RIGHT) {
                index = Random.Range(0, prefabsRight.Length);
                Area areaToGenerate = prefabsRight[index];

                Instantiate(areaToGenerate.gameObject, currentPostion, Quaternion.Euler(rotation));

                var map = Instantiate(originalRawImage, uiPosition, Quaternion.Euler(uiRotation), mapUI.transform);
                map.texture = areaToGenerate.map;
                map.SetNativeSize();
                map.name = "R";

                directionVector = Quaternion.Euler(rotation) * Vector3.forward;
                currentPostion += directionVector * areaToGenerate.length;

                uiDirectionVector = Quaternion.Euler(uiRotation) * Vector3.up;
                uiPosition += uiDirectionVector * 300;

                rotation.y += 90;
                uiRotation.z -= 90;

                directionVector = Quaternion.Euler(rotation) * Vector3.forward;
                currentPostion += directionVector * areaToGenerate.length;

                uiDirectionVector = Quaternion.Euler(uiRotation) * Vector3.up;
                uiPosition += uiDirectionVector * 300;
            } else if (currentDirection == AreaDirection.LEFT) {
                index = Random.Range(0, prefabsLeft.Length);
                Area areaToGenerate = prefabsLeft[index];

                Instantiate(areaToGenerate.gameObject, currentPostion, Quaternion.Euler(rotation));
                var map = Instantiate(originalRawImage, uiPosition, Quaternion.Euler(uiRotation), mapUI.transform);
                map.texture = areaToGenerate.map;
                map.SetNativeSize();
                map.name = "L";

                rotation.y -= 90;
                uiRotation.z += 90;
            }

            turnsCounter--;
            currentDirection = getRandomDirection();
        }

        mapUI.transform.localScale = new Vector3(
            (LevelParams.countOfTurns - 40) / -100f,
            (LevelParams.countOfTurns - 40) / -100f, 
            1f);
		Debug.Log((LevelParams.countOfTurns - 40) / -100f);

        Instantiate(carPrefab[LevelParams.carIndex], startPoint.position, Quaternion.identity);
        FindObjectOfType<CameraFollow>().myAwake();

        buttonPause.gameObject.SetActive(false);
    }

    public AreaDirection getRandomDirection() {
        int index = Random.Range(0, 3);
        if (index == 0) {
            return AreaDirection.FORWARD;
        } else if (index == 1) {
            balance++;

            if (balance == 2) {
                balance -= 2;
                return AreaDirection.LEFT;
            }
            return AreaDirection.RIGHT;
        } else {
            balance--;

            if (balance == -2) {
                balance += 2;
                return AreaDirection.RIGHT;
            }
            return AreaDirection.LEFT;
        }
    }

    void playClick() {
        mapUI.gameObject.SetActive(false);
        buttonPlay.gameObject.SetActive(false);
        originalRawImage.gameObject.SetActive(false);
        Time.timeScale = 1;
        buttonPause.gameObject.SetActive(true);
    }
}
