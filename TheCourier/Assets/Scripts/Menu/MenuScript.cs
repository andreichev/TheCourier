using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {
    public Button[] buttonPlayEasy;
    public Button[] buttonPlayMedium;
    public Button[] buttonPlayHard;
    public Button buttonExit;   

    void Start() {
        for (int i = 0; i < buttonPlayEasy.Length; i++) {
            buttonPlayEasy[i].onClick.AddListener(buttonPlayEasyClick);
        }
        for (int i = 0; i < buttonPlayMedium.Length; i++) {
            buttonPlayMedium[i].onClick.AddListener(buttonPlayMediumClick);
        }
        for (int i = 0; i < buttonPlayHard.Length; i++) {
            buttonPlayHard[i].onClick.AddListener(buttonPlayHardClick);
        }

        buttonExit.onClick.AddListener(buttonExitClick);
    }

    void buttonPlayEasyClick() {
        LevelParams.countOfTurns = 4;
        SceneManager.LoadScene(1);
    }

    void buttonPlayMediumClick() {
        LevelParams.countOfTurns = 9;
        SceneManager.LoadScene(1);
    }

    void buttonPlayHardClick() {
        LevelParams.countOfTurns = 15;
        SceneManager.LoadScene(1);
    }

    void buttonExitClick() {
        Application.Quit();
    }
}
