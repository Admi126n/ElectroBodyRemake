using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerTeleportingController : MonoBehaviour, IPlayerTeleporting
{
    private GameManager _gameController;
    private PlayerController _playerController;
    private PlayerAnimator _playerAnimator;
    private AudioPlayer _audioPlayer;

    private bool _teleportToAnotherScene = false;
    private Vector3 _teleportingDestination;
    private int _destinationScene;
    private bool _teleportPressed;
    private int _chipCounter = 0;

    private void Start()
    {
        _gameController = FindObjectOfType<GameManager>();
        _playerController = GetComponent<PlayerController>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    private void Update()
    {
        if (_teleportPressed && _playerController.gameObject.GetComponent<Rigidbody2D>().IsTouchingLayers(LayerMask.GetMask("Teleporters")))
        {
            _playerController.CanMove = false;
            _teleportPressed = false;
            _playerAnimator.TriggerTeleportation();
            _audioPlayer.PlayTeleportingClip(_playerController.transform.position);
        }
    }

    /// <summary>
    /// Method triggered by input system.
    /// </summary>
    void OnTeleport(InputValue value)
    {
        _teleportPressed = value.Get<Vector2>().y == 1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _teleportToAnotherScene = collision.CompareTag(K.T.ExitTeleporter);

        if (collision.CompareTag(K.T.Chip))
        {
            Destroy(collision.gameObject);
            _chipCounter++;
            _audioPlayer.PlayChipPickedUpClip(_playerController.transform.position);

            if (_chipCounter == 3)
            {
                _gameController.ActivateExitTeleporter();
            }
        }
    }

    /// <summary>
    /// Method is called from player's teleporting animations.
    /// </summary>
    public void TeleportPlayer()
    {
        if (_teleportToAnotherScene)
        {
            SceneManager.LoadScene(_destinationScene);
        } else
        {
            _playerController.gameObject.transform.position = _teleportingDestination;
        }
        // TODO: should be set at the end of exit teleporter animations
        _playerController.CanMove = true;
    }

    public void SetTeleportingDestination(Vector3 destination)
    {
        _teleportingDestination = new (destination.x, destination.y + 0.5f, destination.y);
    }

    public void SetDestinationScene(int index)
    {
        _destinationScene = index;
    }
}
