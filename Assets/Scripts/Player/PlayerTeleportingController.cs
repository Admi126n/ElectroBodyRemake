using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleportingController : MonoBehaviour, PlayerTeleporting
{
    private PlayerController playerController;
    private AudioPlayer audioPlayer;

    private bool canTeleport = false;
    private Vector3 teleportingDestination;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void OnTeleport()
    {
        if (canTeleport)
        {
            Debug.Log("Teleporting...");
            audioPlayer.PlayTeleportingClip(playerController.transform.position);
            playerController.gameObject.transform.position = teleportingDestination;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(K.T.teleporter))
        {
            canTeleport = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(K.T.teleporter))
        {
            canTeleport = false;
        }
    }

    public void SetTeleportingDestination(Vector3 destination)
    {
        teleportingDestination = new (destination.x, destination.y + 0.5f, destination.y);
    }
}
