using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private class TeleporterData
    {
        public readonly Vector3 teleporterPosition;
        public readonly int teleporterDestinationId;

        public TeleporterData(Vector3 teleporterPosition, int teleporterDestinationId)
        {
            this.teleporterPosition = teleporterPosition;
            this.teleporterDestinationId = teleporterDestinationId;
        }
    }

    readonly Dictionary<int, TeleporterData> teleporters = new();
    List<ExitTeleporter> exitTeleporters;

    void Start()
    {
        exitTeleporters = new(FindObjectsOfType<ExitTeleporter>());

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

                teleporters.Add(teleporter.GetId(), teleporterData);
            }
            catch (System.ArgumentException)
            {
                Debug.Break();

                Debug.LogError("Found teleporters with duplicated ID, teleporters positions: "
                    + teleporter.GetTeleporterPosition().ToString()
                    + "; " + teleporters[teleporter.GetId()].teleporterPosition.ToString());
            }
        }
    }

    public void ActivateExitTeleporter()
    {
        foreach (ExitTeleporter exitTeleporter in exitTeleporters)
        {
            exitTeleporter.ActivateTeleporter();
        }
    }
}
