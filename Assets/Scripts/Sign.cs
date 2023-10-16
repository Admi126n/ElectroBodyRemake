using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    [SerializeField] Explosion explosion;
    [SerializeField] Sprite arrow;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private BoxCollider2D _boxCollider;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(K.T.PlayerBullet))
        {
            _boxCollider.enabled = false;
            _animator.enabled = false;
            Instantiate(explosion, transform.position, transform.rotation);
            _spriteRenderer.sprite = arrow;
        }
    }
}
