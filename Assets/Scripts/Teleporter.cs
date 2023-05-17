using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

interface PlayerTeleporting
{
    void SetTeleportingDestination(Vector3 destination);
}

public class Teleporter : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private int destination_id;

    List<Teleporter> teleporters = new();

    PlayerTeleporting player;

    void Start()
    {
        teleporters = new (FindObjectsOfType<Teleporter>());
        player = FindObjectOfType<PlayerTeleportingController>();

        // TODO: check if exist two teleporters with same id
    }

    private Vector3 GetDestinationTransform()
    {
        foreach (Teleporter teleporter in teleporters)
        {
            if (teleporter.GetId() == destination_id)
            {
                return teleporter.gameObject.transform.position;
            }
        }

        return new Vector3();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.SetTeleportingDestination(GetDestinationTransform());
        }
    }

    public int GetId()
    {
        return id;
    }

    public int GetDestinationId()
    {
        return destination_id;
    }
}
