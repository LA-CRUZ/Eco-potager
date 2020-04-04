using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    [SerializeField] private GameObject menuStart;
    [SerializeField] private GameObject menuLevelSelection;
    [SerializeField] private GameObject menuOptions;
    public void SwitchSceneBtn(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }

    public void ExitGameBtn()
    {
        Application.Quit();
    }

    public void ShowLevelSelection()
    {
        menuLevelSelection.SetActive(true);
        menuStart.SetActive(false);
    }

    public void HideLevelSelection()
    {
        menuLevelSelection.SetActive(false);
        menuStart.SetActive(true);
    }

    public void ShowOptions()
    {
        menuOptions.SetActive(true);
        menuStart.SetActive(false);
    }

    public void HideOptions()
    {
        menuOptions.SetActive(false);
        menuStart.SetActive(true);
    }

    public void changeDifficulty(int difficulty)
    {
        PlayerPrefs.SetInt("difficulty", difficulty);
    }
}
