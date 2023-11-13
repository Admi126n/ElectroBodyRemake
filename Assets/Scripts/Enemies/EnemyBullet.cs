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

    private float _direction = 0;

    private void Start()
    {
        _bulletRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
        if (bulletDirection == BulletDirection.Horizontal)
        {
            transform.localScale = new(_direction, 1);
            _bulletRigidbody.velocity = new(bulletBaseSpeed * _direction, 0f);
        }
        else
        {
            transform.localScale = new(_direction, 1);
            _bulletRigidbody.velocity = new(0f, bulletBaseSpeed * _direction);
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
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void SetBuletDirection(Vector3 value, bool reverse=false)
    {
        if (bulletDirection == BulletDirection.Horizontal)
        {
            _direction = Mathf.Sign(value.x);
        }
        else
        {
            _direction = Mathf.Sign(value.y);
        }

        if (reverse)
        {
            _direction *= -1;
        }
    }
}
