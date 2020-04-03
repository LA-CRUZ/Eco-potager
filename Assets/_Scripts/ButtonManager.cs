using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    [SerializeField] private GameObject menuStart;
    [SerializeField] private GameObject menuLevelSelection;
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
}
