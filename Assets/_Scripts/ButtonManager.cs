using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void SwitchSceneBtn(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }

    public void ExitGameBtn()
    {
        Application.Quit();
    }
}
