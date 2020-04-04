using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{

    [SerializeField] private GameObject menuStart;
    [SerializeField] private GameObject menuLevelSelection;
    [SerializeField] private GameObject menuOptions;
    [SerializeField] private GameObject aideWindow;
    public GameObject volumeSlider;

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
        volumeSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("volume") * 5;
    }

    public void HideOptions()
    {
        menuOptions.SetActive(false);
        aideWindow.SetActive(false);
        menuStart.SetActive(true);
    }

    public void changeDifficulty(int difficulty)
    {
        PlayerPrefs.SetInt("difficulty", difficulty);
    }

    public void ToggleHelp()
    {
        aideWindow.SetActive(!aideWindow.activeSelf);
    }
}
