using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower: MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] EnemyBullet bullet;
    [SerializeField] float cooldown;
    [SerializeField] bool hasRandomCooldowns = true;
    [SerializeField] bool shootRight = true;

    [Header("Other")]
    [SerializeField] Explosion explosion;

    private AudioPlayer _audioPlayer;
    private Coroutine _fireingCoroutine;

    private void Start()
    {
        _audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    private void Update()
    {
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
            EnemyBullet newBullet = Instantiate(bullet, transform.position, transform.rotation);

            if (shootRight)
            {
                newBullet.SetBuletDirection(transform.localScale);
            } else
            {
                newBullet.SetBuletDirection(new(-1, 1, 1));
            }
            

            _audioPlayer.PlayCannonShootingClip(shootingClip, transform.position);

            if (hasRandomCooldowns)
            {
                cooldown = Random.Range(cooldown - 0.5f, cooldown + 0.5f);
            }
            yield return new WaitForSeconds(cooldown);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(K.T.PlayerBullet) || collision.collider.CompareTag(K.T.Player))
        {
            Instantiate(explosion, transform.position, transform.rotation);
            StopAllCoroutines();

            Destroy(gameObject);
        }
    }
}
