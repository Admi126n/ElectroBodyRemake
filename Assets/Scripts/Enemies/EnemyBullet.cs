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

    private Rigidbody2D _bulletRigidbody;
    private readonly float _BulletBaseSpeed = 2;
    private float _bulletSpeed;

    private void Start()
    {
        _bulletRigidbody = GetComponent<Rigidbody2D>();

        // set bullet direction
        if (bulletDirection == BulletDirection.Horizontal)
        {
            _bulletSpeed = transform.parent.localScale.x * _BulletBaseSpeed;
        }
        else
        {
            _bulletSpeed = transform.parent.localScale.y * _BulletBaseSpeed;
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
}
