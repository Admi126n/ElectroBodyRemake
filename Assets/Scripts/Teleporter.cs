using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

interface IPlayerTeleporting
{
    void SetTeleportingDestination(Vector3 destination);
}

/// <summary>
/// Teleporter script. Setting destinationId to -1 makes teleporter inactive (it is only destination teleporter)
/// </summary>
public class Teleporter : MonoBehaviour
{
    [Header("Teleporter IDs")]
    [SerializeField] private int id;
    [SerializeField] private int destinationId;

    [Header("Sprites")]
    [SerializeField] private Sprite inactiveBase;

    List<Teleporter> teleporters = new();

    SpriteRenderer teleporterBaseRenderer;
    SpriteRenderer teleporterRenderer;
    Animator teleporterAnimator;
    IPlayerTeleporting player;

    bool isActive = true;

    void Start()
    {
        teleporters = new (FindObjectsOfType<Teleporter>());
        teleporterBaseRenderer = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        teleporterRenderer = GetComponent<SpriteRenderer>();
        teleporterAnimator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerTeleportingController>();

        DetectTeleporterDuplicates();

        if (destinationId == -1)
        {
            SetInactiveTeleporter();
        }
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

    private Vector3 GetDestinationPosition()
    {
        foreach (Teleporter teleporter in teleporters)
        {
            if (teleporter.GetId() == destinationId)
            {
                return teleporter.gameObject.transform.position;
            }
        }

        return new Vector3();
    }

    private void SetInactiveTeleporter()
    {
        teleporterRenderer.enabled = false;
        teleporterAnimator.enabled = false;
        teleporterBaseRenderer.sprite = inactiveBase;
        teleporterRenderer.tag = K.T.inactiveTeleporter;
        isActive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive && collision.CompareTag(K.T.player))
        {
            player.SetTeleportingDestination(GetDestinationPosition());
        }
    }

    public Vector3 GetTeleporterPosition()
    {
        return transform.position;
    }

    public int GetId()
    {
        return id;
    }

    public int GetDestinationId()
    {
        return destinationId;
    }
}
