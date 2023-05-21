using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTeleporter : MonoBehaviour
{
    [SerializeField] private int destinationScene;

    private SpriteRenderer teleporterRenderer;
    private Animator teleporterAnimator;

    private bool isActive = false;

    void Start()
    {
        teleporterRenderer = GetComponent<SpriteRenderer>();
        teleporterAnimator = GetComponent<Animator>();
    }

    public void ActivateTeleporter()
    {
        teleporterRenderer.enabled = true;
        teleporterAnimator.enabled = true;
        isActive = false;
    }
}
