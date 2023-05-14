using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D bulletRigidbody;
    Animator bulletAnimator;
    PlayerController playerController;
    AudioPlayer audioPlayer;

    readonly float bulletSpeed = 8;
    float bulletDirection;

    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>();
        bulletAnimator = GetComponent<Animator>();
        playerController = FindObjectOfType<PlayerController>();
        audioPlayer = FindObjectOfType<AudioPlayer>();

        transform.localScale = new(-playerController.transform.localScale.x, transform.localScale.y, transform.localScale.z);
        bulletDirection = -playerController.transform.localScale.x * bulletSpeed;
    }

    void Update()
    {
        bulletRigidbody.velocity = new(bulletDirection, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bulletAnimator.SetTrigger("Explode");
        audioPlayer.PlayExplosionClip(transform.position);

        // FIXME: 4 weapon's bullet should explode only on collision with walls
        // FIXME: 5 weapon's bullet should have separate explode animation
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
