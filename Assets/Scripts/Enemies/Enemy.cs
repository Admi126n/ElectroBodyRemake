using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy movement")]
    [SerializeField] float movementSpeed = 1.5f;
    [SerializeField] bool hasRandomSpeed = true;
    
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] AudioClip warningClip;
    [SerializeField] EnemyBullet bullet;
    [SerializeField] float cooldown;
    [SerializeField] float warningCooldown;
    [SerializeField] bool hasRandomCooldowns = true;
    [SerializeField] bool canShoot = true;

    [Header("Other")]
    [SerializeField] Explosion explosion;

    private Animator _animator;
    private AudioPlayer _audioPlayer;
    private Rigidbody2D _enemyRigidbody;
    private bool _canMove = true;

    private void Start()
    {
        _audioPlayer = FindObjectOfType<AudioPlayer>();
        _enemyRigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        if (hasRandomSpeed)
        {
            movementSpeed = Random.Range(movementSpeed - 0.5f, movementSpeed + 0.5f);
        }

        if (canShoot)
        {
            StartCoroutine(FireContinuosly());
        }
    }

    private void Update()
    {
        if (_canMove)
        {
            _enemyRigidbody.velocity = new Vector2(movementSpeed * Mathf.Sign(transform.localScale.x), 0);
        }
    }

    private IEnumerator FireContinuosly()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 2f));

        while (true)
        {
            _canMove = false;
            _enemyRigidbody.velocity = new Vector2(0, 0);
            _animator.StartPlayback();
            _audioPlayer.PlayCannonShootingClip(warningClip, transform.position);

            if (hasRandomCooldowns)
            {
                warningCooldown = Random.Range(warningCooldown - 0.5f, warningCooldown + 0.5f);
            }
            yield return new WaitForSeconds(warningCooldown);

            EnemyBullet newBullet = Instantiate(bullet, transform.position, transform.rotation);
            newBullet.SetBuletDirection(transform.localScale);

            _audioPlayer.PlayCannonShootingClip(shootingClip, transform.position);

            yield return new WaitForSeconds(0.4f);
            _canMove = true;
            _animator.StopPlayback();

            if (hasRandomCooldowns)
            {
                cooldown = Random.Range(cooldown - 0.5f, cooldown + 0.5f);
            }
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
            FlipEnemyFacing();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(K.T.PlayerBullet) || collision.collider.CompareTag(K.T.Player))
        {
            Instantiate(explosion, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }
}
