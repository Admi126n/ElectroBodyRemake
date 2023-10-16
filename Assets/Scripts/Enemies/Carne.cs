using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carne : MonoBehaviour
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

    [Header("Other")]
    [SerializeField] Explosion explosion;

    private AudioPlayer _audioPlayer;
    private Coroutine _fireingCoroutine;
    private Rigidbody2D _enemyRigidbody;
    private bool _canMove = true;

    private void Start()
    {
        _audioPlayer = FindObjectOfType<AudioPlayer>();
        _enemyRigidbody = GetComponent<Rigidbody2D>();

        if (hasRandomSpeed)
        {
            movementSpeed = Random.Range(movementSpeed - 0.5f, movementSpeed + 0.2f);
        }
    }

    private void Update()
    {
        if (_canMove)
        {
            _enemyRigidbody.velocity = new Vector2(movementSpeed * Mathf.Sign(transform.localScale.x), 0);
        }

        if (_fireingCoroutine == null)
        {
            _fireingCoroutine = StartCoroutine(FireContinuosly());
        }
        
    }

    private IEnumerator FireContinuosly()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 2f));

        while (true)
        {
            _canMove = false;
            _enemyRigidbody.velocity = new Vector2(0, 0);
            _audioPlayer.PlayCannonShootingClip(warningClip, transform.position);

            if (hasRandomCooldowns)
            {
                warningCooldown = Random.Range(warningCooldown - 0.5f, warningCooldown + 0.5f);
            }
            yield return new WaitForSeconds(warningCooldown);

            EnemyBullet newBullet = Instantiate(bullet, transform.position, transform.rotation);
            newBullet.SetBuletDirection(transform.localScale, true);

            _audioPlayer.PlayCannonShootingClip(shootingClip, transform.position);

            yield return new WaitForSeconds(0.4f);
            _canMove = true;

            if (hasRandomCooldowns)
            {
                cooldown = Random.Range(cooldown - 0.5f, cooldown + 0.5f);
            }
            yield return new WaitForSeconds(cooldown);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(K.T.EnemyWall))
        {
            movementSpeed *= -1;
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.CompareTag(K.T.PlayerBullet) || collision.collider.CompareTag(K.T.Player))
    //    {
    //        Instantiate(explosion, transform.position, transform.rotation);

    //        GetComponent<BoxCollider2D>().enabled = false;
    //        GetComponent<SpriteRenderer>().enabled = false;
    //        StopAllCoroutines();

    //        Destroy(gameObject);
    //    }
    //}
}
