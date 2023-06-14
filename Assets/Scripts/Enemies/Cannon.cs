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

    private AudioPlayer _audioPlayer;
    private bool _isActive = true;
    private Coroutine _fireingCoroutine;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;

    void Start()
    {
        _audioPlayer = FindObjectOfType<AudioPlayer>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (!_isActive)
        {
            StopCoroutine(_fireingCoroutine);
            return;
        }

        if (_fireingCoroutine == null)
        {
            _fireingCoroutine = StartCoroutine(FireContinuosly());
        }
        
    }

    private IEnumerator FireContinuosly()
    {
        while (true)
        {
            Instantiate(bullet, transform.position, transform.rotation, transform);
            _audioPlayer.PlayCannonShootingClip(shootingClip, transform.position);

            yield return new WaitForSeconds(cooldown);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDestroyable && (collision.CompareTag(K.T.Bullet)))
        {
            Instantiate(explosion, transform.position, transform.rotation);
            _boxCollider.enabled = false;
            _spriteRenderer.sprite = destroyedCannon;
            _isActive = false;
        }
    }
}
