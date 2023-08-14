using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TeleporterData
{
    public Vector3 TeleporterPosition { get; set; }
    public int TeleporterDestinationId { get; set; }

    public TeleporterData(Vector3 teleporterPosition, int teleporterDestinationId)
    {
        TeleporterPosition = teleporterPosition;
        TeleporterDestinationId = teleporterDestinationId;
    }

}

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    private readonly Dictionary<int, TeleporterData> _Teleporters = new();
    private List<ExitTeleporter> _exitTeleporters;
    private readonly Dictionary<int, Respawn> _Respawns = new();
    private PlayerController player;

    private bool _gamePaused = false;
    private bool _lightingEnabled = true;

    public bool LightingEnabled
    {
        set { _lightingEnabled = value; }
        get { return _lightingEnabled; }
    }

    private void Awake()
    {
        _exitTeleporters = new(FindObjectsOfType<ExitTeleporter>());

        FillTeleportersDict();
        FillRespawnsDict();
    }

    private void Start()
    {
        //Screen.SetResolution(1950, 1200, true);
        // TODO check if exit teleporter destination scene == current scene + 1
        player = FindObjectOfType<PlayerController>();
    }

    public void ResetGameSession()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void FillTeleportersDict()
    {
        List<Teleporter> teleportersObjects = new(FindObjectsOfType<Teleporter>());

        foreach (Teleporter teleporter in teleportersObjects)
        {
            try
            {
                TeleporterData teleporterData = new(teleporter.GetTeleporterPosition(), teleporter.GetDestinationId());

                _Teleporters.Add(teleporter.GetId(), teleporterData);
            }
            catch (System.ArgumentException)
            {
                Debug.Break();

                Debug.LogError("Found teleporters with duplicated ID, teleporters positions: "
                    + teleporter.GetTeleporterPosition().ToString()
                    + "; " + _Teleporters[teleporter.GetId()].TeleporterPosition.ToString());
            }
        }
    }

    private void FillRespawnsDict()
    {
        List<Respawn> respawns = new(FindObjectsOfType<Respawn>());

        foreach (Respawn respawn in respawns)
        {
            try
            {
                _Respawns.Add(respawn.GetId(), respawn);
            }
            catch (System.ArgumentException)
            {
                Debug.Break();

                Debug.LogError("Found respawns with duplicated ID, respawns positions: "
                    + respawn.GetPosition().ToString()
                    + "; " + _Respawns[respawn.GetId()].gameObject.transform.position.ToString());
            }
        }

    }

    public void ActivateExitTeleporter()
    {
        foreach (ExitTeleporter exitTeleporter in _exitTeleporters)
        {
            exitTeleporter.ActivateTeleporter();
        }
    }

    public void ResetRespawn(int id)
    {
        _Respawns[id].RestRespawn();
    }

    public Vector3 GetRespawnPosition(int id)
    {
        return _Respawns[id].GetPosition();
    }

    public Dictionary<int, TeleporterData> GetTeleporters()
    {
        return _Teleporters;
    }

    public void PauseResumeGame()
    {
        if (_gamePaused)
        {
            player.SetPlayerInput(true);
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        } else
        {
            player.SetPlayerInput(false);
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }

        _gamePaused = !_gamePaused;
    }
}
