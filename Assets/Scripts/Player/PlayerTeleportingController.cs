using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerTeleportingController : MonoBehaviour, IPlayerTeleporting
{
    private GameController gameController;
    private PlayerController playerController;
    private PlayerAnimator playerAnimator;
    private AudioPlayer audioPlayer;

    private bool canTeleport = false;
    private bool teleportToAnotherScene = false;
    private Vector3 teleportingDestination;
    private int destinationScene;
    private bool teleportPressed;
    private int chipCounter = 0;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        playerController = GetComponent<PlayerController>();
        playerAnimator = GetComponent<PlayerAnimator>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    private void Update()
    {
        if (canTeleport && teleportPressed)
        {
            playerController.SetCanMove(false);
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
            teleportToAnotherScene = false;
        } else if (collision.CompareTag(K.T.exitTeleporter))
        {
            canTeleport = true;
            teleportToAnotherScene = true;
        }  else if (collision.CompareTag(K.T.chip))
        {
            Destroy(collision.gameObject);
            chipCounter++;
            audioPlayer.PlayChipPickedUpClip(playerController.transform.position);

            if (chipCounter == 3)
            {
                gameController.ActivateExitTeleporter();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(K.T.teleporter))
        {
            canTeleport = false;
            teleportToAnotherScene = false;
        }
    }

    /// <summary>
    /// Method is called from player's teleporting animations.
    /// </summary>
    public void TeleportPlayer()
    {
        if (teleportToAnotherScene)
        {
            SceneManager.LoadScene(1);
        } else
        {
            playerController.gameObject.transform.position = teleportingDestination;
        }
        playerController.SetCanMove(true);
    }

    public int GetChipCounter()
    {
        return chipCounter;
    }

    public void SetTeleportingDestination(Vector3 destination)
    {
        teleportingDestination = new (destination.x, destination.y + 0.5f, destination.y);
    }

    public void SetDestinationScene(int index)
    {
        destinationScene = index;
    }
}
