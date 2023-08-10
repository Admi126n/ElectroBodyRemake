using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

interface IPlayerTeleporting
{
    void SetTeleportingDestination(Vector3 destination);
    void SetDestinationScene(int index);
}

/// <summary>
/// Teleporter script. Setting destinationId to -1 makes teleporter inactive (it is only destination teleporter).
/// </summary>
public class Teleporter : MonoBehaviour
{
    [Header("Teleporter IDs")]
    [SerializeField] private int id;
    [SerializeField] private int destinationId;

    [Header("Sprites")]
    [SerializeField] private Sprite inactiveBase;

    private Dictionary<int, TeleporterData> _teleporters = new();
    private GameManager _gameController;
    private SpriteRenderer _teleporterBaseRenderer;
    private SpriteRenderer _teleporterRenderer;
    private Animator _teleporterAnimator;
    private Light2D _lightSource;
    private IPlayerTeleporting _player;

    private bool _isActive = true;

    private void Start()
    {
        _gameController = FindObjectOfType<GameManager>();
        _teleporterBaseRenderer = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        _teleporterRenderer = GetComponent<SpriteRenderer>();
        _teleporterAnimator = GetComponent<Animator>();
        _player = FindObjectOfType<PlayerTeleportingController>();
        _lightSource = gameObject.transform.GetComponentInChildren<Light2D>();

        if (destinationId == -1)
        {
            DeactivateTeleporter();
        }
    }

    private Vector3 GetDestinationPosition()
    {
        _teleporters = _gameController.GetTeleporters();

        return _teleporters[destinationId].TeleporterPosition;
    }

    private void DeactivateTeleporter()
    {
        _lightSource.enabled = false;
        _teleporterRenderer.enabled = false;
        _teleporterAnimator.enabled = false;
        _teleporterBaseRenderer.sprite = inactiveBase;
        gameObject.layer = LayerMask.NameToLayer(K.L.InactiveTeleporter);
        _isActive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isActive && collision.CompareTag(K.T.Player))
        {
            _player.SetTeleportingDestination(GetDestinationPosition());
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
