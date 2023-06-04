using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTeleporter : MonoBehaviour
{
    [SerializeField] private int destinationScene;

    private SpriteRenderer _teleporterRenderer;
    private Animator _teleporterAnimator;
    private IPlayerTeleporting _player;

    private bool _isActive = false;

    private void Start()
    {
        _teleporterRenderer = GetComponent<SpriteRenderer>();
        _teleporterAnimator = GetComponent<Animator>();
        _player = FindObjectOfType<PlayerTeleportingController>();
    }

    public void ActivateTeleporter()
    {
        _teleporterRenderer.enabled = true;
        _teleporterAnimator.enabled = true;
        gameObject.tag = K.T.ExitTeleporter;
        gameObject.layer = LayerMask.NameToLayer(K.L.Teleporters);
        _isActive = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isActive && collision.CompareTag(K.T.Player))
        {
            _player.SetDestinationScene(destinationScene);
        }
    }
}
