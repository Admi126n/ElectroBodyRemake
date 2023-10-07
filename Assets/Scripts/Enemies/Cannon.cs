using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

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
    private ShadowCaster2D _shadow;


    private void Start()
    {
        _audioPlayer = FindObjectOfType<AudioPlayer>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _shadow = GetComponent<ShadowCaster2D>();
    }

    private void Update()
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
            EnemyBullet newBullet = Instantiate(bullet, transform.position, transform.rotation);
            newBullet.SetBuletDirection(transform.localScale);

            _audioPlayer.PlayCannonShootingClip(shootingClip, transform.position);

            yield return new WaitForSeconds(cooldown);
        }
    }

    private void DestroyCannon()
    {
        _boxCollider.enabled = false;
        _shadow.enabled = false;
        _spriteRenderer.sprite = destroyedCannon;
        _isActive = false;
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
