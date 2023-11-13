using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableDecoration : MonoBehaviour
{
    [SerializeField] Sprite destroyedSprite;
    [SerializeField] Explosion explosion;
    [SerializeField] bool isDestroyed;

    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();

        if (isDestroyed)
        {
            _boxCollider.enabled = false;
            _spriteRenderer.sprite = destroyedSprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(K.T.PlayerBullet))
        {
            _boxCollider.enabled = false;
            Instantiate(explosion, transform.position, transform.rotation);
            _spriteRenderer.sprite = destroyedSprite;
        }
    }
}
