using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    public static SaveManager saveManagerInstance;

    private void Awake()
    {
        ManageSingleton();
    }

    private void ManageSingleton()
    {
        if (saveManagerInstance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            saveManagerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    [SerializeField] Button loadButton;

    void Start()
    {
        loadButton.interactable = LoadGame();
    }

    public bool LoadGame()
    {
        string path = Application.persistentDataPath;
        return File.Exists(path + "/save.data");
    }

    public void SaveGame()
    {
        string path = Application.persistentDataPath;
        var stream = new FileStream(path + "/save.data", FileMode.Create);
        stream.Close();
    }

    public void DeleteSave()
    {
        string path = Application.persistentDataPath;
        File.Delete(path + "/save.data");
    }
}
