using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] bool isDestroyableByTriggers;

    private Rigidbody2D _bulletRigidbody;
    private Animator _bulletAnimator;
    private PlayerController _playerController;
    private AudioPlayer _audioPlayer;

    private readonly float _BulletBaseSpeed = 8;
    private float _bulletHorizontalSpeed;

    private void Start()
    {
        _bulletRigidbody = GetComponent<Rigidbody2D>();
        _bulletAnimator = GetComponent<Animator>();
        _playerController = FindObjectOfType<PlayerController>();
        _audioPlayer = FindObjectOfType<AudioPlayer>();

        // set bullet direction
        transform.localScale = new(-_playerController.transform.localScale.x, transform.localScale.y, transform.localScale.z);
        _bulletHorizontalSpeed = -_playerController.transform.localScale.x * _BulletBaseSpeed;
    }

    private void Update()
    {
        _bulletRigidbody.velocity = new(_bulletHorizontalSpeed, 0f);
    }

    private bool ShouldNotBeDestroyedOnTrigger(Collider2D collision)
    {
        string collisionTag = collision.tag;

        return (collisionTag == K.T.Ammo || collisionTag == K.T.Chip || collisionTag == K.T.Teleporter 
            || collisionTag == K.T.InactiveTeleporter || collisionTag == K.T.ExitTeleporter);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDestroyableByTriggers || ShouldNotBeDestroyedOnTrigger(collision)) return;

        _bulletHorizontalSpeed = 0f;
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _bulletAnimator.SetTrigger(K.ACP.Explode);
        _audioPlayer.PlayExplosionClip(transform.position);

        // FIXME: 5 weapon's bullet should have separate explode animation
    }

    /// <summary>
    /// Method is called in SmallExplosion animation.
    /// </summary>
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
