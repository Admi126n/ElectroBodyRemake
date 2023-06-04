using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

public class GameController : MonoBehaviour
{
    private readonly Dictionary<int, TeleporterData> _Teleporters = new();
    private List<ExitTeleporter> _exitTeleporters;

    private void Start()
    {
        _exitTeleporters = new(FindObjectsOfType<ExitTeleporter>());

        FillTeleportersDict();
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

    public void ActivateExitTeleporter()
    {
        foreach (ExitTeleporter exitTeleporter in _exitTeleporters)
        {
            exitTeleporter.ActivateTeleporter();
        }
    }

    public Dictionary<int, TeleporterData> GetTeleporters()
    {
        return _Teleporters;
    }
}
