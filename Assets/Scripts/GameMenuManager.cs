using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour
{

    public static bool gamePaused = false;
    public GameObject pauseMenuUI;
    private bool paused = false;
    //public GameObject gameOverUI;


     void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                paused = true;
                pauseMenuUI.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                paused = false;
                pauseMenuUI.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Start()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Menu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // public void GameOver()
    // {
    //     gameOverUI.SetActive(true);
    //     Time.timeScale = 0f;
    // }
}
