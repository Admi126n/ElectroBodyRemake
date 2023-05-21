using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    List<Teleporter> teleporters;
    List<ExitTeleporter> exitTeleporters;

    void Start()
    {
        exitTeleporters = new(FindObjectsOfType<ExitTeleporter>());
        teleporters = new(FindObjectsOfType<Teleporter>());

        DetectTeleporterDuplicates();
    }

    private void DetectTeleporterDuplicates()
    {
        List<int> teleportersIds = new();

        foreach (Teleporter teleporter in teleporters)
        {
            teleportersIds.Add(teleporter.GetId());
        }

        List<int> duplicates = teleportersIds.GroupBy(x => x)
              .Where(g => g.Count() > 1)
              .Select(y => y.Key)
              .ToList();

        if (duplicates.Count() > 0)
        {
            Debug.Break();
            foreach (int el in duplicates)
            {
                foreach (Teleporter teleporter in teleporters)
                {
                    if (el == teleporter.GetId())
                    {
                        Debug.LogError("Found teleporter with duplicated ID, teleporter position: " + teleporter.GetTeleporterPosition().ToString());
                    }
                }
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
