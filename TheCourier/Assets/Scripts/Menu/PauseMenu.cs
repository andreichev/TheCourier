using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseMenu;
    public Button buttonPause;
    public Button buttonContinue;
    public Button buttonMainMenu;
    public Button buttonMainMenu1;
    public Button buttonNextLevel;

    // Start is called before the first frame update
    void Start() {

        buttonPause.onClick.AddListener(pause);
        buttonContinue.onClick.AddListener(continueGame);
        buttonMainMenu.onClick.AddListener(mainMenu);
        buttonMainMenu1.onClick.AddListener(mainMenu);
        buttonNextLevel.onClick.AddListener(nextLevel);
    }

    void pause() {
        pauseMenu.SetActive(true);
        buttonPause.gameObject.SetActive(false);

        Time.timeScale = 0;
    }

    void continueGame() {
        pauseMenu.SetActive(false);
        buttonPause.gameObject.SetActive(true);

        Time.timeScale = 1;
    }

    void mainMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void playerFinished() {
        buttonPause.gameObject.SetActive(false);
    }

    public void nextLevel() {
        if (LevelParams.countOfTurns < 15) {
            LevelParams.countOfTurns++;
        }
        SceneManager.LoadScene(1);
    }
}
