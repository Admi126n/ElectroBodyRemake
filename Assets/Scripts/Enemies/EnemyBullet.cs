using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private enum BulletDirection
    {
        Vertical,
        Horizontal
    };

    [SerializeField] BulletDirection bulletDirection;
    [SerializeField] float bulletBaseSpeed = 2;

    private Rigidbody2D _bulletRigidbody;
    private float _bulletSpeed;

    private void Start()
    {
        _bulletRigidbody = GetComponent<Rigidbody2D>();

        // set bullet direction
        if (bulletDirection == BulletDirection.Horizontal)
        {
            _bulletSpeed = transform.parent.localScale.x * bulletBaseSpeed;
        }
        else
        {
            _bulletSpeed = transform.parent.localScale.y * bulletBaseSpeed;
        }
    }

    private void Update()
    {
        if (bulletDirection == BulletDirection.Horizontal)
        {
            _bulletRigidbody.velocity = new(_bulletSpeed, 0f);
        }
        else
        {
            _bulletRigidbody.velocity = new(0f, _bulletSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(K.T.Ground) || collision.CompareTag(K.T.Player))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(K.T.Room))
        {
            gameObject.layer = LayerMask.NameToLayer(K.L.IgnoreRaycast);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
