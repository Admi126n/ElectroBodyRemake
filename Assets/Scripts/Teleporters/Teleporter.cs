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

        if (destinationId == -1)
        {
            DeactivateTeleporter();
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

    private void DeactivateTeleporter()
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
            // To dziala git, po teleportacji metoda sie triggeruje
            Debug.Log("Teleporter.OnTriggerEnter2D()");

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
