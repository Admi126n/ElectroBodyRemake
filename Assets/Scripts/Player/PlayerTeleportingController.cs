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
        if (teleportPressed && playerController.gameObject.GetComponent<Rigidbody2D>().IsTouchingLayers(LayerMask.GetMask("Teleporters")))
        {
            playerController.CanMove = false;
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
        if (collision.CompareTag(K.T.Teleporter))
        {
            teleportToAnotherScene = false;
        } else if (collision.CompareTag(K.T.ExitTeleporter))
        {
            teleportToAnotherScene = true;
        }  else if (collision.CompareTag(K.T.Chip))
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

    /// <summary>
    /// Method is called from player's teleporting animations.
    /// </summary>
    public void TeleportPlayer()
    {
        if (teleportToAnotherScene)
        {
            SceneManager.LoadScene(destinationScene);
        } else
        {
            playerController.gameObject.transform.position = teleportingDestination;
        }
        playerController.CanMove = true;
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
