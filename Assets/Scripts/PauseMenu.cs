using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused = false;
    private bool gameOver = false;

    public GameObject pauseMenu;
    public GameObject gameOverScreen;

    private void Start()
    {
        pauseMenu.SetActive(false);
        gameOverScreen.SetActive(false);
    }

    private void Update()
    {
        if (gameOver == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("Pause Game");

                if (isPaused == false)
                {
                    isPaused = true;
                    Time.timeScale = 0f;
                    pauseMenu.SetActive(true);
                }

                else
                {
                    isPaused = false;
                    Time.timeScale = 1f;
                    pauseMenu.SetActive(false);
                }
            }
        }

        else
        {
            return;
        }
    }

    public void QuitButton()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        //Debug.Log("Game Over");
        gameOver = true;
        Time.timeScale = 0f;
        gameOverScreen.SetActive(true);
    }
}
