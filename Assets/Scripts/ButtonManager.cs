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
    [SerializeField] private GameObject menuCredits;
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
        menuCredits.SetActive(false);
        //Level 1
        if (PlayerPrefs.GetInt("Tutoriel", -1) == -1)
        {
            GameObject.Find("Level 1").GetComponent<Button>().interactable = false;
            GameObject.Find("Level 1").transform.GetChild(1).GetComponent<Text>().enabled = true;
            GameObject.Find("Level 1").transform.GetChild(1).GetComponent<Text>().text = PlayerPrefs.GetInt("Level 1", 0).ToString();
            GameObject.Find("Level 1").transform.GetChild(2).GetComponent<Text>().enabled = false;
            GameObject.Find("Level 1").transform.GetChild(3).GetComponent<Text>().enabled = false;
        }
        else
        {
            GameObject.Find("Level 1").GetComponent<Button>().interactable = true;
            GameObject.Find("Level 1").transform.GetChild(1).GetComponent<Text>().enabled = false;
            GameObject.Find("Level 1").transform.GetChild(2).GetComponent<Text>().enabled = true;
            GameObject.Find("Level 1").transform.GetChild(3).GetComponent<Text>().enabled = true;
        }
        
        //Level 2
        if (PlayerPrefs.GetInt("Level 1", -1) == -1)
        {
            GameObject.Find("Level 2").GetComponent<Button>().interactable = false;
            GameObject.Find("Level 2").transform.GetChild(1).GetComponent<Text>().enabled = true;
            GameObject.Find("Level 2").transform.GetChild(1).GetComponent<Text>().text = PlayerPrefs.GetInt("Level 2", 0).ToString();
            GameObject.Find("Level 2").transform.GetChild(2).GetComponent<Text>().enabled = false;
            GameObject.Find("Level 2").transform.GetChild(3).GetComponent<Text>().enabled = false;
        }
        else
        {
            GameObject.Find("Level 2").GetComponent<Button>().interactable = true;
            GameObject.Find("Level 2").transform.GetChild(1).GetComponent<Text>().enabled = false;
            GameObject.Find("Level 2").transform.GetChild(2).GetComponent<Text>().enabled = true;
            GameObject.Find("Level 2").transform.GetChild(3).GetComponent<Text>().enabled = true;
        }
        //Level 3
        if (PlayerPrefs.GetInt("Level 2", -1) == -1)
        {
            GameObject.Find("Level 3").GetComponent<Button>().interactable = false;
            GameObject.Find("Level 3").transform.GetChild(1).GetComponent<Text>().enabled = true;
            GameObject.Find("Level 3").transform.GetChild(1).GetComponent<Text>().text = PlayerPrefs.GetInt("Level 3", 0).ToString();
            GameObject.Find("Level 3").transform.GetChild(2).GetComponent<Text>().enabled = false;
            GameObject.Find("Level 2").transform.GetChild(3).GetComponent<Text>().enabled = false;
        }
        else
        {
            GameObject.Find("Level 3").GetComponent<Button>().interactable = true;
            GameObject.Find("Level 3").transform.GetChild(1).GetComponent<Text>().enabled = false;
            GameObject.Find("Level 3").transform.GetChild(2).GetComponent<Text>().enabled = true;
            GameObject.Find("Level 2").transform.GetChild(3).GetComponent<Text>().enabled = true;
        }
        //Level 4
        if (PlayerPrefs.GetInt("Level 3", -1) == -1)
        {
            GameObject.Find("Level 4").GetComponent<Button>().interactable = false;
            GameObject.Find("Level 4").transform.GetChild(1).GetComponent<Text>().enabled = true;
            GameObject.Find("Level 4").transform.GetChild(1).GetComponent<Text>().text = PlayerPrefs.GetInt("Level 4", 0).ToString();
            GameObject.Find("Level 4").transform.GetChild(2).GetComponent<Text>().enabled = false;
            GameObject.Find("Level 2").transform.GetChild(3).GetComponent<Text>().enabled = false;
        }
        else
        {
            GameObject.Find("Level 4").GetComponent<Button>().interactable = true;
            GameObject.Find("Level 4").transform.GetChild(1).GetComponent<Text>().enabled = false;
            GameObject.Find("Level 4").transform.GetChild(2).GetComponent<Text>().enabled = true;
            GameObject.Find("Level 2").transform.GetChild(23).GetComponent<Text>().enabled = true;
        }
    }

    public void HideLevelSelection()
    {
        menuLevelSelection.SetActive(false);
        menuStart.SetActive(true);
        menuCredits.SetActive(false);
    }

    public void ShowCredit()
    {
        menuCredits.SetActive(true);
        menuStart.SetActive(false);
    }

    public void HideCredit()
    {
        menuCredits.SetActive(false);
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
