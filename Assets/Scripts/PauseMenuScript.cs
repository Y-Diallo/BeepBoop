using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public GameObject gameOverText;

    public void PauseButton()
    {
        pauseMenu.SetActive(true);
        gameOverText.SetActive(false);
        Time.timeScale = 0;
    }

    public void ResumeButton()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void HomeButton()
    {
        SceneManager.LoadScene("WelcomeScene");
        Time.timeScale = 1;
    }
}
