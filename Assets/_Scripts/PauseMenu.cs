using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gamePaused = false;
    public GameObject pauseMenu;
    public GameObject menuOptions;
    public GameObject aideWindow;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void ShowOptions()
    {
        menuOptions.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void HideOptions()
    {
        menuOptions.SetActive(false);
        aideWindow.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void ToggleHelp()
    {
        aideWindow.SetActive(!aideWindow.activeSelf);
    }

    public void changeDifficulty(int difficulty)
    {
        PlayerPrefs.SetInt("difficulty", difficulty);
    }
}
