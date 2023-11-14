using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] AudioClip shootingClip;
    [SerializeField] bool isDestroyable;
    [SerializeField] Sprite destroyedCannon;
    [SerializeField] Explosion explosion;
    [SerializeField] EnemyBullet bullet;
    [SerializeField] float cooldown;
    [SerializeField] bool hasRandomCooldown = true;
    [SerializeField] bool shootDown = true;

    private AudioPlayer _audioPlayer;
    private bool _isActive = true;
    private Coroutine _fireingCoroutine;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;


    private void Start()
    {
        _audioPlayer = FindObjectOfType<AudioPlayer>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();

        _fireingCoroutine = StartCoroutine(FireContinuosly());
    }

    private IEnumerator FireContinuosly()
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));

        while (true)
        {
            EnemyBullet newBullet = Instantiate(bullet, transform.position, transform.rotation);
            newBullet.SetBuletDirection(transform.localScale, shootDown);

            _audioPlayer.PlayCannonShootingClip(shootingClip, transform.position);

            if (hasRandomCooldown)
            {
                cooldown = Random.Range(cooldown - 0.5f, cooldown + 1);
            }
            yield return new WaitForSeconds(cooldown);
        }
    }

    private void DestroyCannon()
    {
        _boxCollider.enabled = false;
        _spriteRenderer.sprite = destroyedCannon;
        _isActive = false;
        StopCoroutine(_fireingCoroutine);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDestroyable && (collision.CompareTag(K.T.PlayerBullet)))
        {
            Instantiate(explosion, transform.position, transform.rotation);
            DestroyCannon();
        }
    }

    public bool IsActive()
    {
        return _isActive;
    }
}
