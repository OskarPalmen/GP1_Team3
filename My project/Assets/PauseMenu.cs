using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject headerUI;
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
                Resume();
            }
            else {
                Pause();
            }
        }
    }
    public void Resume() {
        pauseMenuUI.SetActive(false);
        headerUI.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void Pause() {
        pauseMenuUI.SetActive(true);
        headerUI.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }
    
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }
}
