using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePresist : MonoBehaviour
{
    public static ScenePresist scenePersistInstance;

    private static int _chipCounter = 0;
    private static int _respawnId;

    private void Awake()
    {
        ManageSingleton();
    }

    private void ManageSingleton()
    {
        if (scenePersistInstance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            scenePersistInstance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetScenePersist()
    {
        _chipCounter = 0;
        Destroy(gameObject);
        _respawnId = 0;
    }

    public static int GetChipCounter()
    {
        return _chipCounter;
    }

    public static void IncreaseChipCounter()
    {
        _chipCounter++;
        FindObjectOfType<UIManager>().UpdateChipIndicator();
    }

    public static void ResetChipCounter()
    {
        _chipCounter = 0;
    }

    public static void SetRespawnId(int value)
    {
        _respawnId = value;
    }

    public static int GetRespawnId()
    {
        return _respawnId;
    }

}
