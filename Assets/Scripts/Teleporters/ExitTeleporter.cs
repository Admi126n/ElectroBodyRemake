using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTeleporter : MonoBehaviour
{
    [SerializeField] private int destinationScene;

    private SpriteRenderer teleporterRenderer;
    private Animator teleporterAnimator;
    private PlayerTeleportingController player;

    private bool isActive = false;

    void Start()
    {
        teleporterRenderer = GetComponent<SpriteRenderer>();
        teleporterAnimator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerTeleportingController>();
    }

    public void ActivateTeleporter()
    {
        teleporterRenderer.enabled = true;
        teleporterAnimator.enabled = true;
        gameObject.tag = K.T.exitTeleporter;
        isActive = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive && collision.CompareTag(K.T.player))
        {
            player.SetDestinationScene(destinationScene);
        }
    }
}
