using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy movement")]
    [SerializeField] float movementSpeed = 2;
    
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] AudioClip warningClip;
    [SerializeField] EnemyBullet bullet;
    [SerializeField] float cooldown;
    [SerializeField] float warningCooldown;

    [Header("Other")]
    [SerializeField] Explosion explosion;

    private Animator _animator;
    private AudioPlayer _audioPlayer;
    private Coroutine _fireingCoroutine;
    private Rigidbody2D _enemyRigidbody;
    private bool _canMove = true;
    private bool _isAlive = true;

    private void Start()
    {
        _audioPlayer = FindObjectOfType<AudioPlayer>();
        _enemyRigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_isAlive)
        {
            if (_canMove)
            {
                _enemyRigidbody.velocity = new Vector2(movementSpeed, 0);
            }

            if (_fireingCoroutine == null)
            {
                _fireingCoroutine = StartCoroutine(FireContinuosly());
            }
        } else if (transform.childCount == 0)
        {
            // Destroy enemy if there are no bullets of this enemy
            Destroy(gameObject);
        }
    }

    private IEnumerator FireContinuosly()
    {
        while (true)
        {
            _canMove = false;
            _animator.StartPlayback();
            _audioPlayer.PlayCannonShootingClip(warningClip, transform.position);

            yield return new WaitForSeconds(warningCooldown);

            Instantiate(bullet, transform.position, transform.rotation, transform);
            _audioPlayer.PlayCannonShootingClip(shootingClip, transform.position);

            yield return new WaitForSeconds(0.4f);
            _canMove = true;
            _animator.StopPlayback();

            yield return new WaitForSeconds(cooldown);
        }
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-Mathf.Sign(_enemyRigidbody.velocity.x), 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(K.T.EnemyWall))
        {
            movementSpeed *= -1;
            FlipEnemyFacing();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(K.T.PlayerBullet) || collision.collider.CompareTag(K.T.Player))
        {
            _isAlive = false;

            Instantiate(explosion, transform.position, transform.rotation);

            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            StopAllCoroutines();
        }
    }
}
