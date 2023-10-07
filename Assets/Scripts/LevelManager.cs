using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene(K.LevelName.level1);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(K.LevelName.mainMenu);
    }

    //public void QuitGame()
    //{
    //    Application.Quit();
    //}
}
