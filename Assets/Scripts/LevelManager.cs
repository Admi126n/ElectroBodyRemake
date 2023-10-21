using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadGame()
    {
        SaveManager.saveManagerInstance.DeleteSave();
        SceneManager.LoadScene(K.LevelName.level1);
    }

    public void LoadSecondLevel()
    {
        SceneManager.LoadScene(K.LevelName.level2);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(K.LevelName.mainMenu);
    }

    public void LoadTestLevel()
    {
        SceneManager.LoadScene(K.LevelName.testLevel);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
