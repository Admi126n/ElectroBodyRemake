using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] bool isDestroyableByTriggers;
    [SerializeField] Explosion explosion;

    private Rigidbody2D _bulletRigidbody;
    private PlayerController _playerController;

    private readonly float _BulletBaseSpeed = 8;
    private float _bulletHorizontalSpeed;

    private void Start()
    {
        _bulletRigidbody = GetComponent<Rigidbody2D>();
        _playerController = FindObjectOfType<PlayerController>();

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
            || collisionTag == K.T.InactiveTeleporter || collisionTag == K.T.ExitTeleporter
            || collisionTag == K.T.EnemyBullet);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDestroyableByTriggers || ShouldNotBeDestroyedOnTrigger(collision)) return;

        _bulletHorizontalSpeed = 0f;
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(K.T.Ground))
        {
            InstantiateExplosion();
        } else
        {
            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        _bulletHorizontalSpeed = 0f;
        Destroy(gameObject);
    }

    private void InstantiateExplosion()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        DestroyBullet();
    }
}
