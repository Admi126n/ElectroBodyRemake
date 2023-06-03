using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D bulletRigidbody;
    private Animator bulletAnimator;
    private PlayerController playerController;
    private AudioPlayer audioPlayer;

    private readonly float bulletBaseSpeed = 8;
    private float bulletHorizontalSpeed;

    private void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>();
        bulletAnimator = GetComponent<Animator>();
        playerController = FindObjectOfType<PlayerController>();
        audioPlayer = FindObjectOfType<AudioPlayer>();

        // set bullet direction
        transform.localScale = new(-playerController.transform.localScale.x, transform.localScale.y, transform.localScale.z);
        bulletHorizontalSpeed = -playerController.transform.localScale.x * bulletBaseSpeed;
    }

    private void Update()
    {
        bulletRigidbody.velocity = new(bulletHorizontalSpeed, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bulletAnimator.SetTrigger(K.ACP.Explode);
        audioPlayer.PlayExplosionClip(transform.position);

        // FIXME: 4 weapon's bullet should explode only on collision with walls
        // FIXME: 5 weapon's bullet should have separate explode animation
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
