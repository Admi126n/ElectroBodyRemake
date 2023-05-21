using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTeleportingController : MonoBehaviour, IPlayerTeleporting
{
    private PlayerController playerController;
    private PlayerAnimator playerAnimator;
    private AudioPlayer audioPlayer;

    private bool canTeleport = false;
    private Vector3 teleportingDestination;
    private bool teleportPressed;
    private int chipCounter = 0;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerAnimator = GetComponent<PlayerAnimator>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    private void Update()
    {
        if (canTeleport && teleportPressed)
        {
            teleportPressed = false;
            playerAnimator.TriggerTeleportation();
            audioPlayer.PlayTeleportingClip(playerController.transform.position);
        }
    }

    void OnTeleport(InputValue value)
    {
        teleportPressed = value.Get<Vector2>().y == 1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(K.T.teleporter))
        {
            canTeleport = true;
        } else if (collision.CompareTag(K.T.chip))
        {
            Destroy(collision.gameObject);
            chipCounter++;
            audioPlayer.PlayChipPickedUpClip(playerController.transform.position);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(K.T.teleporter))
        {
            canTeleport = false;
        }
    }

    public void TeleportPlayer()
    {
        playerController.gameObject.transform.position = teleportingDestination;
    }

    public void TeleportPlayer(int sceneIndex)
    {
        Debug.Log("Teleporting to " + sceneIndex.ToString() + " level");
    }

    public int GetChipCounter()
    {
        return chipCounter;
    }

    public void SetTeleportingDestination(Vector3 destination)
    {
        teleportingDestination = new (destination.x, destination.y + 0.5f, destination.y);
    }
}
